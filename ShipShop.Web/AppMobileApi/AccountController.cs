using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.App_Start;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ShipShop.Web.AppMobileApi
{
    [RoutePrefix("api/app/account")]
    [Authorize]
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
        [Authorize]
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

        [HttpGet]
        [Route("getCurrenAccount")]
        [Authorize]
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