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
    public class PageController : Controller
    {
        private IPageService _pageService;
        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }
        //
        // GET: /Page/
        public ActionResult Index(string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var pageVM = Mapper.Map<PageViewModel>(page);
            return View(pageVM);
        }

        public ActionResult About(string alias)
        {
            return View();
        }
	}
}