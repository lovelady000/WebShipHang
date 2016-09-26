using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.App_Start;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Models;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ShipShop.Web.AppMobileApi
{
    [RoutePrefix("api/app/account")]
    [Authorize(Roles = Common.RolesConstants.ROLES_USER)]
    public class AccountController : ApiControllerBase
    {
        public AccountController(IErrorService errorService) : base(errorService)
        {
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorService errorService) : base(errorService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [HttpPost]
        [Route("changePass")]
        [Authorize(Roles = Common.RolesConstants.ROLES_USER)]
        public async Task<HttpResponseMessage> ChangePassword(HttpRequestMessage request, ChangePassViewModel model)
        {
            //ApplicationUser user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            var user = await UserManager.FindAsync(User.Identity.Name, model.OldPassword);
            if (user == null)
            {
                return request.CreateResponse(HttpStatusCode.OK, "Mật khẩu cũ không đúng!");
            }
            else
            {
                if (model.NewPassword != model.RePassword)
                {
                    return request.CreateResponse(HttpStatusCode.OK, "Mật khẩu không trùng khớp!");
                }
                else
                {
                    var result = await UserManager.RemovePasswordAsync(user.Id);
                    if (result.Succeeded)
                    {
                        result = await UserManager.AddPasswordAsync(user.Id, model.NewPassword);
                        if (result.Succeeded)
                        {
                            return request.CreateResponse(HttpStatusCode.OK, "Thay đổi mật khẩu thành công!");
                        }
                        else
                        {
                            return request.CreateResponse(HttpStatusCode.OK, "Thay đổi mật khẩu thất bại!");
                        }
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.OK, "Thay đổi mật khẩu thất bại!");
                    }
                }
            }
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Register(HttpRequestMessage request, RegisterViewModel register)
        {
            var userByUsserName = await _userManager.FindByNameAsync(register.UserName);
            if (userByUsserName != null)
            {
                var response = new { Code = 0, Msg = "Số điện thoại đã được đăng kí!" };
                return request.CreateResponse(HttpStatusCode.BadRequest, "Số điện thoại đã được đăng kí!");
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
            await UserManager.CreateAsync(user, register.Password);
            var userFindByName = await _userManager.FindByNameAsync(register.UserName);
            UserManager.AddToRoles(userFindByName.Id, new string[] { "User" });

            var responseSuccess = new { Code = 1, Msg = "" };
            return request.CreateResponse(HttpStatusCode.BadRequest, "Đăng kí thành công!");
        }


        [HttpGet]
        [Route("getCurrenAccount")]
        [Authorize(Roles = Common.RolesConstants.ROLES_USER)]
        public async Task<HttpResponseMessage> ChangePassword(HttpRequestMessage request)
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest,"Không thể lấy thông tin tài khoản");
            }
            else
            {
                ApplicationUserViewModel appVM = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);
                return request.CreateResponse(HttpStatusCode.OK, appVM);
            }
        }
    }
}