using ShipShop.Service;
using ShipShop.Web.Infrastructure.Core;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using System.Linq;

namespace ShipShop.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        private IErrorService _errorService;
        public HomeController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello ";
        }


        
    }
}