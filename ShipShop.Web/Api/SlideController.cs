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
    [RoutePrefix("api/slide")]
    [Authorize]
    public class SlideController : ApiControllerBase
    {
        #region Init Controller

        private ISlideService _slideService;

        public SlideController(IErrorService errorService, ISlideService slideService)
            : base(errorService)
        {
            this._slideService = slideService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_GET_LIST_SLIDE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _slideService.GetAll();
                total = model.Count();
                var query = model.OrderBy(x => x.DisplayOrder).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<SlideViewModel>>(query);

                var paginationSet = new PaginationSet<SlideViewModel>()
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
        [Authorize(Roles = Common.RolesConstants.ROLES_ADD_SLIDE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Create(HttpRequestMessage request, SlideViewModel slideVM)
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
                    var slide = new Slide();
                    slide.UpdateSlide(slideVM);
                    _slideService.Add(slide);
                    _slideService.Save();
                    var responseData = Mapper.Map<Slide, SlideViewModel>(slide);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("getbyid")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_SLIDE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var slide = _slideService.GetById(id);
                var responseData = Mapper.Map<Slide, SlideViewModel>(slide);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);

                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_SLIDE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Update(HttpRequestMessage request, SlideViewModel slideVM)
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
                    var slide = _slideService.GetById(slideVM.ID);
                    slide.UpdateSlide(slideVM);
                    _slideService.Update(slide);
                    _slideService.Save();
                    var responseData = Mapper.Map<Slide, SlideViewModel>(slide);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("Delete")]
        [HttpDelete]
        [Authorize(Roles = Common.RolesConstants.ROLES_DELETE_SLIDE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
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
                    _slideService.Delete(id);
                    _slideService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, "");
                }

                return response;
            });
        }
    }
}
