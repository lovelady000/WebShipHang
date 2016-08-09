using ShipShop.Service;
using ShipShop.Web.Models;
using System.Web.Mvc;

namespace ShipShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private IRegionService _regionService;
        public AccountController(IRegionService regionService)
        {
            this._regionService = regionService;
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.RegionID = new SelectList(_regionService.GetAll(),"ID","Name");
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            var x = register;
            return View();
           
        }
    }
}