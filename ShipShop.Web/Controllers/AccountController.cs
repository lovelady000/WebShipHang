using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.App_Start;
using ShipShop.Web.Hubs;
using ShipShop.Web.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShipShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private IRegionService _regionService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IRegionService regionService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this._regionService = regionService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.RegionID = new SelectList(_regionService.GetAll(), "RegionID", "Name");
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Register(RegisterViewModel register)
        {
            var userByUsserName = await _userManager.FindByNameAsync(register.UserName);
            if (userByUsserName != null)
            {
                var response = new { Code = 0, Msg = "Số điện thoại đã được đăng kí!" };
                return Json(response);
            }
            var user = new ApplicationUser()
            {
                UserName = register.UserName,
                Address = register.Address,
                Vendee = register.Vendee,
                RegionID = register.RegionID,
                WebOrShopName = register.Vendee ? register.WebOrShopName : "",
                IsAdmin = false,
            };
            await _userManager.CreateAsync(user, register.Password);
            var userFindByName = await _userManager.FindByNameAsync(register.UserName);
            _userManager.AddToRoles(userFindByName.Id, new string[] { "User" });
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity = _userManager.CreateIdentity(userFindByName, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationProperties props = new AuthenticationProperties();
            props.IsPersistent = true;
            authenticationManager.SignIn(props, identity);

            var responseSuccess = new { Code = 1, Msg = "" };
            return Json(responseSuccess);
        }

        [HttpGet]
        public ActionResult Login()
        {
            //var context = GlobalHost.ConnectionManager.GetHubContext<OrderHub>();
            //context.Clients.All.broadcastMessage("Đơn hàng mới!", "Bạn có đơn hàng mới từ số điện thoại:");
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            ApplicationUser user = await _userManager.FindAsync(model.UserName, model.Password);
            if (user != null && !user.IsAdmin)
            {
                if(user.IsBanded.GetValueOrDefault(false))
                {
                    var responseError = new { Code = 0, Msg = "Tài khoản đang bị khóa !" };
                    return Json(responseError);
                }
                IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationProperties props = new AuthenticationProperties();
                props.IsPersistent = model.RememberMe;
                authenticationManager.SignIn(props, identity);
                var responseSuccess = new { Code = 1, Msg = "" };
                return Json(responseSuccess);
            }
            //return RedirectToAction("Index","Home");
            //return Redirect("/");
            var response = new { Code = 0, Msg = "Số điện thoại hoặc mật khẩu không chính xác!" };
            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            //return RedirectToAction("Index", "Home");
            return Redirect("/");
        }

        [HttpPost]
        public async Task<JsonResult> ChangePassword(ChangePassViewModel model)
        {
            //ApplicationUser user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            var user = await UserManager.FindAsync(User.Identity.Name, model.OldPassword);
            if (user == null)
            {
                return Json(new { code = 0, msg = "Mật khẩu cũ không đúng!" });
            }
            else
            {
                if (model.NewPassword != model.RePassword)
                {
                    return Json(new { code = 0, msg = "Mật khẩu không trùng khớp!" });
                }
                else
                {
                    var result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
                    if (result.Succeeded)
                    {
                        result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                        if (result.Succeeded)
                        {
                            return Json(new { code = 1, msg = "Thay đổi mật khẩu thành công!" });
                        }
                        else
                        {
                            return Json(new { code = 0, msg = "Thay đổi mật khẩu thất bại!" });
                        }
                    }
                    else
                    {
                        return Json(new { code = 0, msg = "Thay đổi mật khẩu thất bại!" });
                    }
                }
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCurrenAccount()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return Json(new { code = 0, msg = "Thất bại!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (user.IsBanded.GetValueOrDefault(false))
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut();
                    return Json(new { code = 2, msg = "Tài khoản đang bị khóa!" }, JsonRequestBehavior.AllowGet);
                }
                ApplicationUserViewModel appVM = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);
                return Json(new { code = 1, msg = appVM }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> ChangeProfileAccount(ApplicationUserViewModel appVM)
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return Json(new { code = 0, msg = "Thất bại!" });
            }
            else
            {
                user.Address = appVM.Address;
                await UserManager.UpdateAsync(user);
                return Json(new { code = 1, msg = "Sửa thông tin thành công!" });
            }
        }
    }
}