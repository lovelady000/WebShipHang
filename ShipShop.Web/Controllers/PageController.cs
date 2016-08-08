using ShipShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShipShop.Web.Controllers
{
    public class PageController : Controller
    {
        private INewsService _newsService;
        public PageController(INewsService newsService)
        {
            this._newsService = newsService;
        }
        //
        // GET: /Page/
        public ActionResult Index(string alias)
        {

            return View();
        }

        public ActionResult About(string alias)
        {
            return View();
        }
	}
}