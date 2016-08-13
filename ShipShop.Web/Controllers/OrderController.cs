using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.App_Start;
using ShipShop.Web.Infrastructure.Extensions;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShipShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        private ApplicationUserManager _userManager;

        public OrderController(ApplicationUserManager userManager, IOrderService orderService, IOrderDetailService orderDetailService)
        {
            UserManager = userManager;
            this._orderService = orderService;
            this._orderDetailService = orderDetailService;
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
                var order = _orderService.GetByID(id);
                var orderVM = Mapper.Map<OrderViewModel>(order);
                ViewBag.Order = orderVM;
                return View(query);
            }
            else
            {
                return Redirect("/");
            }
        }
        // GET: Order
        public async  Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Title = "Quản trị tài khoản!";

                var model = _orderService.GetAllByUserName(User.Identity.Name,new string[] { "ReceiverRegion", "SenderRegion" });
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
        public JsonResult CreateOrder(OrderHomePageViewModel orderHomePage)
        {
            var orderVM = orderHomePage.Order;

            var order = new Order();
            order.UpdateOrder(orderVM);
            order.CreatedBy = User.Identity.Name;
            order.Username = User.Identity.Name;
            order.CreatedDate = DateTime.Now;
            order.Status = true;
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
    }
}