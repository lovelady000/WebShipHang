using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.App_Start;
using ShipShop.Web.Infrastructure.Extensions;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace ShipShop.Web.Controllers
{
    public class OrderController : Controller
    {
        #region init
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        private IRegionService _regionService;
        private ApplicationUserManager _userManager;

        public OrderController(ApplicationUserManager userManager, IOrderService orderService, IOrderDetailService orderDetailService, IRegionService regionService)
        {
            UserManager = userManager;
            this._orderService = orderService;
            this._orderDetailService = orderDetailService;
            this._regionService = regionService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult OrderDetail(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Title = "Chi tiết đơn hàng";
                var modelOrderDetail = _orderDetailService.GetAllByOrder(id,new string[] { "Order" });
                var query = Mapper.Map<IEnumerable<OrderDetailViewModel>>(modelOrderDetail);
                var order = _orderService.GetByID(id,new string [] { "ReceiverRegion", "SenderRegion" });
                var orderVM = Mapper.Map<OrderViewModel>(order);
                if(order.Username != User.Identity.Name)
                {
                    return Redirect("/");
                }
                ViewBag.Order = orderVM;
                return View(query);
            }
            else
            {
                return Redirect("/");
            }
        }
        #endregion
        // GET: Order
        public async  Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                DateTime dtBeginDate = DateTime.MinValue;
                DateTime dtToDate = DateTime.MaxValue;
                if (Request["dtDenNgay"] != null)
                {
                    string denNgay = Request["dtDenNgay"].ToString();
                    ViewBag.dtDenNgay = denNgay;
                    if(denNgay != "")
                    dtToDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);
                }

                if (Request["dtTuNgay"] != null)
                {
                    string tuNgay = Request["dtTuNgay"].ToString();
                    ViewBag.dtTuNgay = tuNgay;
                    if (tuNgay != "")
                        dtBeginDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                if (Request["drdRegion"] != null)
                {
                    string drdRegion = Request["drdRegion"].ToString();
                    if (drdRegion != "")
                        ViewBag.drdRegion = new SelectList(_regionService.GetAll(), "RegionID", "Name", drdRegion);
                }
                else
                {
                    ViewBag.drdRegion = new SelectList(_regionService.GetAll(), "RegionID", "Name");
                }

                ViewBag.Title = "Quản trị tài khoản";
                var model = _orderService.GetAllByUserName(User.Identity.Name, dtBeginDate,dtToDate, new string[] { "ReceiverRegion", "SenderRegion" });
                var query = Mapper.Map<IEnumerable<OrderViewModel>>(model);
                var currenUser = await _userManager.FindByNameAsync(User.Identity.Name);
                //ViewBag.CurrenUser = currenUser;
                ViewBag.Address = currenUser.Address;
                ViewBag.Vendee = currenUser.Vendee;
                ViewBag.WebsiteOrShop = currenUser.WebOrShopName;
                
                return View(query);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrder(OrderHomePageViewModel orderHomePage)
        {
            if(User.Identity.IsAuthenticated)
            {
                var orderVM = orderHomePage.Order;
                var order = new Order();
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                order.UpdateOrder(orderVM);
                order.CreatedBy = User.Identity.Name;
                order.Username = User.Identity.Name;
                order.CreatedDate = DateTime.Now;
                order.Status = true;
                order.UserID = user.Id;

                _orderService.Add(order);
                _orderService.Save();

                var listOrderDetailVM = orderHomePage.listOrderDetail;
                if (listOrderDetailVM != null)
                {
                    OrderDetail orderDetail;
                    foreach (var item in listOrderDetailVM)
                    {
                        orderDetail = new OrderDetail();
                        orderDetail.UpdateOrderDetail(item);
                        orderDetail.OrderID = order.ID;
                        _orderDetailService.Add(orderDetail);
                    }
                    _orderDetailService.Save();
                }

                var response = new { Code = 1, Msg = "THành công" };
                return Json(response);
            }
            else
            {
                var response = new { Code = 0, Msg = "Thất bại" };
                return Json(response);
            }
        }
    }
}