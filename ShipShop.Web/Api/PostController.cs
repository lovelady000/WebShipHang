using ShipShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShipShop.Service;

namespace ShipShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    [Authorize]
    public class PostController : ApiControllerBase
    {
        public PostController(IErrorService errorService) : base(errorService)
        {

        }
    }
}
