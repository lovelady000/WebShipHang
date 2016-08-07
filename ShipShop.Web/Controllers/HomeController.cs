using AutoMapper;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShipShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMenuService _menuService;
        private IDonViTieuBieuService _donViTieuBieuService;

        public HomeController(IMenuService menuService, IDonViTieuBieuService donViTieuBieuService)
        {
            this._menuService = menuService;
            this._donViTieuBieuService = donViTieuBieuService;
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
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var model = _menuService.GetAll();
            var listMenuViewModel = Mapper.Map<IEnumerable<Menu>, IEnumerable<MenuViewModel>>(model);
            ViewBag.listMenuVM = listMenuViewModel;
            return PartialView();
        }
    }
}