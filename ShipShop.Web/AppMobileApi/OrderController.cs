using AutoMapper;
using Microsoft.AspNet.Identity;
using ShipShop.Common;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.App_Start;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Infrastructure.Extensions;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShipShop.Web.AppMobileApi
{
    [RoutePrefix("api/order")]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        #region Init Controller

        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        private IRegionService _regionService;
        private ApplicationUserManager _userManager;
        private int _pageSize;
        private int _maxPage;

        public OrderController(ApplicationUserManager userManager, IOrderService orderService, IOrderDetailService orderDetailService, IRegionService regionService, IErrorService errorService)
            : base(errorService)
        {
            _userManager = userManager;
            this._orderService = orderService;
            this._orderDetailService = orderDetailService;
            this._regionService = regionService;
            this._pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            this._maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage Get(HttpRequestMessage request, string typeOfOrder,string dtDenNgay,string dtTuNgay,int page = 1)
        {

            return CreateHttpResponse(request, () =>
            {
                int pageSize = _pageSize;
                int totalCount = 0;
                int maxPage = _maxPage;
                DateTime dtBeginDate = DateTime.MinValue;
                DateTime dtToDate = DateTime.MaxValue;

                if (!string.IsNullOrEmpty(dtDenNgay))
                {
                    string denNgay = dtDenNgay.ToString();
                    if (denNgay != "")
                        dtToDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);
                }

                if (!string.IsNullOrEmpty(dtTuNgay))
                {
                    string tuNgay = dtTuNgay.ToString();
                    if (tuNgay != "")
                        dtBeginDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                //if (Request["drdRegion"] != null)
                //{
                //    string drdRegion = Request["drdRegion"].ToString();
                //    if (drdRegion != "")
                //        ViewBag.drdRegion = new SelectList(_regionService.GetAll(), "RegionID", "Name", drdRegion);
                //}
                //else
                //{
                //    ViewBag.drdRegion = new SelectList(_regionService.GetAll(), "RegionID", "Name");
                //}

                var model = _orderService.GetAllByUserName(User.Identity.Name, dtBeginDate, dtToDate, page, pageSize, out totalCount, new string[] { "ReceiverRegion", "SenderRegion" });
                if (typeOfOrder == "2")
                {
                    var user = _userManager.FindByName(User.Identity.Name);
                    if (user.Vendee)
                    {

                        model = _orderService.GetAllBySenderMobile(User.Identity.Name, dtBeginDate, dtToDate, page, pageSize, out totalCount, new string[] { "ReceiverRegion", "SenderRegion" });
                    }
                    else
                    {
                        model = _orderService.GetAllByReceiverMobile(User.Identity.Name, dtBeginDate, dtToDate, page, pageSize, out totalCount, new string[] { "ReceiverRegion", "SenderRegion" });
                    }
                }
                var query = Mapper.Map<IEnumerable<OrderViewModel>>(model);

                var currenUser = _userManager.FindByName(User.Identity.Name);

                int totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = query,
                    MaxPage = maxPage,
                    Page = page,
                    TotalCount = totalCount,
                    TotalPages = totalPage,
                };
                return request.CreateResponse(HttpStatusCode.OK, paginationSet);
            });
        }

        [Route("getorderbyid")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetOrderByID(HttpRequestMessage request, int orderID)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _orderService.GetByID(orderID, new string[] { "SenderRegion", "ReceiverRegion" });
                var responseData = Mapper.Map<OrderViewModel>(model);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }


        [Route("getorderdetail")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetOrderDetailByOrderID(HttpRequestMessage request, int orderID)
        {
            return CreateHttpResponse(request, () =>
            {

                var modelOrderDetail = _orderDetailService.GetAllByOrder(orderID, new string[] { "Order" });
                var query = Mapper.Map<IEnumerable<OrderDetailViewModel>>(modelOrderDetail);
                var order = _orderService.GetByID(orderID, new string[] { "ReceiverRegion", "SenderRegion" });
                var orderVM = Mapper.Map<OrderViewModel>(order);
                var responseData = query;
                var total = responseData.Count();
                if (order.Username != User.Identity.Name || order.SenderMobile != User.Identity.Name || order.ReceiverMobile != User.Identity.Name)
                {
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                    return response;
                }
                return request.CreateResponse(HttpStatusCode.BadRequest, "Thao tác thất bại!");
            });
        }

        [Route("changeOrderStatus")]
        [HttpPut]
        [Authorize]
        public HttpResponseMessage ChangeOrderStatus(HttpRequestMessage request, OrderViewModel orderVM)
        {
            return CreateHttpResponse(request, () =>
            {
              
                var order = _orderService.GetByID(orderVM.ID);
                
                order.Status = !order.Status;
                _orderService.Update(order);
                _orderService.Save();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, "");
                return response;
            });
        }

        [Route("createOrder")]
        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateOrder(HttpRequestMessage request, OrderHomePageViewModel orderHomePage)
        {
            if (User.Identity.IsAuthenticated)
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

                //var response = new { Code = 1, Msg = "THành công" };
                return request.CreateResponse(HttpStatusCode.OK,"Tạo đơn hàng thành công");
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, "Tạo đơn hàng không thành công");
            }
        }
    }
}
