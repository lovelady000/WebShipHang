using AutoMapper;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Infrastructure.Extensions;
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
    [Authorize]
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

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _regionService.GetAll(new string[] { "Areas" });
                total = model.Count();
                var query = model.OrderBy(x => x.AreaID).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<RegionViewModel>>(query);

                var paginationSet = new PaginationSet<RegionViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = total,
                    TotalPages = (int)Math.Ceiling((decimal)total / pageSize),
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }


        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, RegionViewModel regionVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, ModelState);
                }
                else
                {
                    var region = new Region();
                    region.UpdateRegion(regionVM);
                    _regionService.Add(region);
                    _regionService.Save();
                    var responseData = Mapper.Map<Region, RegionViewModel>(region);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var region = _regionService.GetById(id);
                var responseData = Mapper.Map<Region, RegionViewModel>(region);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);

                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, RegionViewModel regionVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, ModelState);
                }
                else
                {
                    var region = _regionService.GetById(regionVM.RegionID);
                    region.UpdateRegion(regionVM);
                    _regionService.Update(region);
                    _regionService.Save();
                    var responseData = Mapper.Map<Region, RegionViewModel>(region);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("Delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, ModelState);
                }
                else
                {
                    _regionService.Delete(id);
                    _regionService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, "");
                }

                return response;
            });
        }
    }
}
