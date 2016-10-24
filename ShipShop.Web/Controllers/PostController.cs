using AutoMapper;
using ShipShop.Common;
using ShipShop.Service;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShipShop.Web.Controllers
{
    public class PostController : Controller
    {
        private IPostService _postService;
        private IPostCategoryService _postCategoryService;
        private int _maxPage;
        private int _pageSize;

        public PostController(IPostService postService, IPostCategoryService postCategoryService)
        {
            this._postService = postService;
            this._postCategoryService = postCategoryService;
            this._maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            //this._pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            this._pageSize = 1;
        }
        // GET: Post
        public ActionResult Index(string alias, int page=0)
        {
            var PostCategory = _postCategoryService.GetByAlias(alias);
            ViewBag.Alias = alias;
            ViewBag.Title = PostCategory.Name;
            int totalRow = 0;
            var ListPost = _postService.GetAllByCategoryPaging(PostCategory.ID, page, _pageSize, out totalRow).OrderByDescending(x=>x.ID);
            var listResult = Mapper.Map<IEnumerable<PostViewModel>>(ListPost);
            int totalCount = totalRow;
            int totalPage = (int)Math.Ceiling((double)totalCount / _pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = listResult,
                MaxPage = _maxPage,
                Page = page,
                TotalCount = totalCount,
                TotalPages = totalPage,

            };
           
            return View(paginationSet);
        }

        public ActionResult ViewPost(string alias)
        {
            var post = _postService.GetByAlias(alias);
            var postVM = Mapper.Map<PostViewModel>(post);
            return View(postVM);
        }
    }
}