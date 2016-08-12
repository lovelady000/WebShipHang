using AutoMapper;
using ShipShop.Service;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShipShop.Web.Api
{
    [RoutePrefix("api/region")]
    public class RegionController : ApiControllerBase
    {
        private IRegionService _regionService;

        public RegionController(IErrorService errorService, IRegionService regionService)
            : base(errorService)
        {
            this._regionService = regionService;
        }
        [Route("getallnopaging")]
        [HttpGet]
        public HttpResponseMessage GetAllNoPaging(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _regionService.GetAll(new string[] { "Areas" });
                var responseData = Mapper.Map<List<RegionViewModel>>(model);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
    }
}
