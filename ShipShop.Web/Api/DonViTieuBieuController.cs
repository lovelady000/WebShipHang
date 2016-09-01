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
    [RoutePrefix("api/dvtb")]
    [Authorize]
    public class DonViTieuBieuController : ApiControllerBase
    {
        #region Init Controller

        private IDonViTieuBieuService _dvtbService;

        public DonViTieuBieuController(IErrorService errorService, IDonViTieuBieuService dvtbService)
            : base(errorService)
        {
            this._dvtbService = dvtbService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_GET_LIST_DVTB + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _dvtbService.GetAll();
                total = model.Count();
                var query = model.OrderBy(x => x.Order).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<DonViTieuBieuViewModel>>(query);

                var paginationSet = new PaginationSet<DonViTieuBieuViewModel>()
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
        [Authorize(Roles = Common.RolesConstants.ROLES_ADD_DVTB + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Create(HttpRequestMessage request, DonViTieuBieuViewModel dvtbVM)
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
                    var dvtb = new DonViTieuBieu();
                    dvtb.UpdateDVTB(dvtbVM);
                    _dvtbService.Add(dvtb);
                    _dvtbService.Save();
                    var responseData = Mapper.Map<DonViTieuBieu, DonViTieuBieuViewModel>(dvtb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("getbyid")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_DVTB + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var dvtb = _dvtbService.GetByID(id);
                var responseData = Mapper.Map<DonViTieuBieu, DonViTieuBieuViewModel>(dvtb);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);

                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_DVTB + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Update(HttpRequestMessage request, DonViTieuBieuViewModel dvtbVM)
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
                    var dvtb = _dvtbService.GetByID(dvtbVM.ID);
                    dvtb.UpdateDVTB(dvtbVM);
                    _dvtbService.Update(dvtb);
                    _dvtbService.Save();
                    var responseData = Mapper.Map<DonViTieuBieu, DonViTieuBieuViewModel>(dvtb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("Delete")]
        [HttpDelete]
        [Authorize(Roles = Common.RolesConstants.ROLES_DELETE_DVTB + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
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
                    _dvtbService.Delete(id);
                    _dvtbService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, "");
                }
                return response;
            });
        }
    }
}
