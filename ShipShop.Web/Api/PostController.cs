using ShipShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShipShop.Service;
using AutoMapper;
using ShipShop.Web.Models;
using ShipShop.Model.Models;
using ShipShop.Web.Infrastructure.Extensions;

namespace ShipShop.Web.Api
{
    [RoutePrefix("api/post")]
    [Authorize]
    public class PostController : ApiControllerBase
    {
        private IPostService _postService;
        public PostController(IErrorService errorService, IPostService postService) : base(errorService)
        {
            this._postService = postService;
        }

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_GET_LIST_POST + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _postService.GetAll();
                total = model.Count();
                var query = model.OrderBy(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<PostViewModel>>(query);

                var paginationSet = new PaginationSet<PostViewModel>()
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

        //[Route("getallnopaging")]
        //[HttpGet]
        //public HttpResponseMessage GetAllNoPaging(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var model = _postCategoryService.GetAll();
        //        var responseData = Mapper.Map<List<PostCategoryViewModel>>(model);

        //        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //        return response;
        //    });
        //}

        [Route("create")]
        [HttpPost]
        [Authorize(Roles = Common.RolesConstants.ROLES_ADD_POST + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Create(HttpRequestMessage request, PostViewModel postVM)
        {
            if (ModelState.IsValid)
            {
                return CreateHttpResponse(request, () =>
                {
                    Post post = new Post();
                    post.UpdatePost(postVM);
                    _postService.Add(post);
                    _postService.Save();
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
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_POST + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var reponseData = _postService.GetById(id);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, reponseData);

                return response;
            });
        }



        [Route("update")]
        [HttpPut]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_POST + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Update(HttpRequestMessage request, PostViewModel postVM)
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
                    Post postCate = _postService.GetById(postVM.ID);
                    postCate.UpdatePost(postVM);
                    _postService.Update(postCate);
                    _postService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, "Sửa thành công");
                }
                return response;
            });
        }


        [Route("Delete")]
        [HttpDelete]
        [Authorize(Roles = Common.RolesConstants.ROLES_DELETE_POST + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _postService.Delete(id);
                _postService.Save();
                response = request.CreateResponse(HttpStatusCode.Created, "");

                return response;
            });
        }
    }
}
