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
    [RoutePrefix("api/page")]
    [Authorize]
    public class PageController : ApiControllerBase
    {
        private IPageService _pageService;

        public PageController(IErrorService errorService, IPageService pageService) : base(errorService)
        {
            this._pageService = pageService;
        }

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_GET_LIST_PAGE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _pageService.GetAll();
                total = model.Count();
                var query = model.OrderBy(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<PageViewModel>>(query);

                var paginationSet = new PaginationSet<PageViewModel>()
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
        [Authorize(Roles = Common.RolesConstants.ROLES_ADD_PAGE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Create(HttpRequestMessage request, PageViewModel pageVM)
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
                    var page = new Page();
                    page.UpdatePage(pageVM);
                    _pageService.Add(page);
                    _pageService.Save();
                    var responseData = Mapper.Map<Page, PageViewModel>(page);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_PAGE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var page = _pageService.GetById(id);
                var responseData = Mapper.Map<Page, PageViewModel>(page);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);

                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_PAGE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Update(HttpRequestMessage request, PageViewModel pageVM)
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
                    var page = _pageService.GetById(pageVM.ID);
                    page.UpdatePage(pageVM);
                    _pageService.Update(page);
                    _pageService.Save();
                    var responseData = Mapper.Map<Page, PageViewModel>(page);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("Delete")]
        [HttpDelete]
        [Authorize(Roles = Common.RolesConstants.ROLES_DELETE_PAGE + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
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
                    _pageService.Delete(id);
                    _pageService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, "");
                }

                return response;
            });
        }
    }
}