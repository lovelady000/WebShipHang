using AutoMapper;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShipShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMenuService _menuService;
        private IDonViTieuBieuService _donViTieuBieuService;
        private INewsService _newsService;
        private IWebInformationService _webInformationService;

        public HomeController(IMenuService menuService, IDonViTieuBieuService donViTieuBieuService, INewsService newsService, IWebInformationService webInformationService)
        {
            this._menuService = menuService;
            this._donViTieuBieuService = donViTieuBieuService;
            this._newsService = newsService;
            this._webInformationService = webInformationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = _donViTieuBieuService.GetAll();
            var listDonViTieuBieuVM = Mapper.Map<IEnumerable<DonViTieuBieu>, IEnumerable<DonViTieuBieuViewModel>>(model);
            ViewBag.listDonViTieuBieu = listDonViTieuBieuVM;

            var webInformation = _webInformationService.GetSingle();
            var webInformationVM = Mapper.Map<WebInformation, WebInformationViewModel>(webInformation);
            ViewBag.WebInfo = webInformationVM;
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var listNews = _newsService.GetAll().OrderBy(x => x.Order);
            var listNewsVM = Mapper.Map<IEnumerable<News>, IEnumerable<NewsViewModel>>(listNews);
            ViewBag.listNewsVM = listNewsVM;

            var model = _menuService.GetAll();
            var listMenuViewModel = Mapper.Map<IEnumerable<Menu>, IEnumerable<MenuViewModel>>(model);
            ViewBag.listMenuVM = listMenuViewModel;

            var webInformation = _webInformationService.GetSingle();
            var webInformationVM = Mapper.Map<WebInformation, WebInformationViewModel>(webInformation);
            ViewBag.webInformationVM = webInformationVM;

            return PartialView();
        }
 
    }
}