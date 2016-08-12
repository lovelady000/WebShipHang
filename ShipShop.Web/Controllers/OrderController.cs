using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.Infrastructure.Extensions;
using ShipShop.Web.Models;
using System;
using System.Web.Mvc;

namespace ShipShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;

        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            this._orderService = orderService;
            this._orderDetailService = orderDetailService;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
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
            if(listOrderDetailVM != null)
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