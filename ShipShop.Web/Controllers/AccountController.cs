using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.App_Start;
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
        public async Task<ActionResult> Register(RegisterViewModel register)
        {
            ViewBag.RegionID = new SelectList(_regionService.GetAll(), "RegionID", "Name");
            var userByUsserName = await _userManager.FindByNameAsync(register.UserName);
            if (userByUsserName != null)
            {
                ModelState.AddModelError("Username", "Tài khoản đã tồn tại!");
                return View(register);
            }

            var user = new ApplicationUser()
            {
                UserName = register.UserName,
                Address = register.Address,
                Vendee = register.Vendee,
                RegionID = register.RegionID,
                WebOrShopName = register.Vendee ? register.WebOrShopName : "",
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

            return Redirect("/");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            ApplicationUser user = await _userManager.FindAsync(model.UserName, model.Password);
            if (user != null)
            {
                IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationProperties props = new AuthenticationProperties();
                props.IsPersistent = model.RememberMe;
                authenticationManager.SignIn(props, identity);
            }

            //return RedirectToAction("Index","Home");
            return Redirect("/");
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
    }
}