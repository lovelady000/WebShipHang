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
    [RoutePrefix("api/news")]
    [Authorize]
    public class NewsController : ApiControllerBase
    {
        #region Init Controller

        private INewsService _newsService;

        public NewsController(IErrorService errorService, INewsService newsService)
            : base(errorService)
        {
            this._newsService = newsService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_GET_LIST_NEWS + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _newsService.GetAll();
                total = model.Count();
                var query = model.OrderBy(x => x.Order).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<NewsViewModel>>(query);

                var paginationSet = new PaginationSet<NewsViewModel>()
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
        [Authorize(Roles = Common.RolesConstants.ROLES_ADD_NEWS + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Create(HttpRequestMessage request, NewsViewModel newsVM)
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
                    var news = new News();
                    news.UpdateNews(newsVM);
                    _newsService.Add(news);
                    _newsService.Save();
                    var responseData = Mapper.Map<News, NewsViewModel>(news);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("getbyid")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_NEWS + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var news = _newsService.GetById(id);
                var responseData = Mapper.Map<News, NewsViewModel>(news);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);

                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_NEWS + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
    
        public HttpResponseMessage Update(HttpRequestMessage request, NewsViewModel newsVM)
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
                    var newnews = _newsService.GetById(newsVM.ID);
                    newnews.UpdateNews(newsVM);
                    _newsService.Update(newnews);
                    _newsService.Save();
                    var responseData = Mapper.Map<News, NewsViewModel>(newnews);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("Delete")]
        [HttpDelete]
        [Authorize(Roles = Common.RolesConstants.ROLES_DELETE_NEWS + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
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
                    _newsService.Delete(id);
                    _newsService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, "");
                }

                return response;
            });
        }
    }
}
