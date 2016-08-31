﻿using AutoMapper;
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
        private ISlideService _slideService;

        public HomeController(IMenuService menuService, IDonViTieuBieuService donViTieuBieuService, 
            INewsService newsService, IWebInformationService webInformationService , ISlideService slideService)
        {
            this._menuService = menuService;
            this._donViTieuBieuService = donViTieuBieuService;
            this._newsService = newsService;
            this._webInformationService = webInformationService;
            this._slideService = slideService;
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

            var slide = _slideService.GetAll();
            slide = slide.OrderBy(x => x.DisplayOrder);
            var listSlideVM = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slide);
            ViewBag.listSlideVM = listSlideVM;
            return PartialView();
        }
 
    }
}