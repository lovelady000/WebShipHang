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
    [RoutePrefix("api/postcategory")]
    [Authorize]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService)
            : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        //public PostCategoryController()
        //{
        //}

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _postCategoryService.GetAll();
                total = model.Count();
                var query = model.OrderBy(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<PostCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<PostCategoryViewModel>()
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

        [Route("getallnopaging")]
        [HttpGet]
        public HttpResponseMessage GetAllNoPaging(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _postCategoryService.GetAll();
                var responseData = Mapper.Map<List<PostCategoryViewModel>>(model);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PostCategoryViewModel postCateVM)
        {
            if (ModelState.IsValid)
            {
                return CreateHttpResponse(request, () =>
                {
                    PostCategory postCate = new PostCategory();
                    postCate.UpdatePostCategory(postCateVM);
                    _postCategoryService.Add(postCate);
                    _postCategoryService.Save();
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, "Thêm thành công");

                    return response;
                });
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, "Thêm không thành công");
            }
        }

        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var reponseData = _postCategoryService.GetById(id);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, reponseData);

                return response;
            });
        }



        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, PostCategoryViewModel postCateVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory postCate = _postCategoryService.GetById(postCateVM.ID);
                    postCate.UpdatePostCategory(postCateVM);
                    _postCategoryService.Update(postCate);
                    _postCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, "Sửa thành công");
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

                _postCategoryService.Delete(id);
                _postCategoryService.Save();
                response = request.CreateResponse(HttpStatusCode.Created, "");

                return response;
            });
        }
    }
}