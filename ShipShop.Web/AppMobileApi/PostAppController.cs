using ShipShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShipShop.Service;
using ShipShop.Web.Models;
using AutoMapper;

namespace ShipShop.Web.AppMobileApi
{
    [Authorize]
    [RoutePrefix("api/app/post")]
    public class PostAppController : ApiControllerBase
    {
        private IPostService _postService;
        private IPostCategoryService _postCategoryService;
        private IPageService _pageService;
        public PostAppController(IErrorService errorService, IPostService postService, IPostCategoryService postCategoryService, IPageService pageService) : base(errorService)
        {
            this._postService = postService;
            this._postCategoryService = postCategoryService;
            this._pageService = pageService;
        }

        [Route("GetListPostByPostCate")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetListPost(HttpRequestMessage request, string alias)
        {
            var PostCategory = _postCategoryService.GetByAlias(alias);
            var ListPost = _postService.GetAllByCategory(PostCategory.ID).OrderByDescending(x => x.ID);
            var listResult = Mapper.Map<IEnumerable<PostViewModel>>(ListPost);
            var paginationSet = listResult;
            return request.CreateResponse(HttpStatusCode.OK, paginationSet);

        }

        [Route("GetPostContent")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetPostContent(HttpRequestMessage request, string alias)
        {
            var post = _postService.GetByAlias(alias);
            var postVM = Mapper.Map<PostViewModel>(post);

            return request.CreateResponse(HttpStatusCode.OK, postVM);

        }

        [Route("GetPageContent")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetPageContent(HttpRequestMessage request, string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var pageVM = Mapper.Map<PageViewModel>(page);

            return request.CreateResponse(HttpStatusCode.OK, pageVM);

        }
    }
}
