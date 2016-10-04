using AutoMapper;
using ShipShop.Service;
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
        public PostController(IPostService postService, IPostCategoryService postCategoryService)
        {
            this._postService = postService;
            this._postCategoryService = postCategoryService;
        }
        // GET: Post
        public ActionResult Index(string alias)
        {
            var PostCategory = _postCategoryService.GetByAlias(alias);
            ViewBag.Alias = alias;
            ViewBag.Title = PostCategory.Name;
            int totalRow = 0;
            var ListPost = _postService.GetAllByCategoryPaging(PostCategory.ID, 0, 10, out totalRow);
            var listResult = Mapper.Map<IEnumerable<PostViewModel>>(ListPost);
            return View(listResult);
        }

        public ActionResult ViewPost(string alias)
        {
            var post = _postService.GetByAlias(alias);
            var postVM = Mapper.Map<PostViewModel>(post);
            return View(postVM);
        }
    }
}