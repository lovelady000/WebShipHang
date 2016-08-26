using Microsoft.AspNet.Identity.Owin;
using ShipShop.Web.App_Start;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ShipShop.Web.Api
{

    [RoutePrefix("api/account")]
    [Authorize]
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        [AllowAnonymous]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, string userName, string password, bool rememberMe)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = await SignInManager.UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return request.CreateResponse(HttpStatusCode.OK, false);
            }
            else
            {
                var listRoles = await _userManager.GetRolesAsync(user.Id);
                if (!listRoles.Contains("Admin"))
                {
                    return request.CreateResponse(HttpStatusCode.OK, false);
                }
                else
                {
                    var result = await SignInManager.PasswordSignInAsync(userName, password, rememberMe, shouldLockout: false);
                    return request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
        }

        [HttpPost]
        [Route("changePass")]
        public async Task<HttpResponseMessage> ChangePassword(HttpRequestMessage request, ChangePassViewModel model)
        {
            //ApplicationUser user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            var user = await UserManager.FindAsync(User.Identity.Name, model.OldPassword);
            if (user == null)
            {
                return request.CreateResponse(HttpStatusCode.OK, "Mật khẩu cũ không đúng!" );
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


    }
}
