using AutoMapper;
using ShipShop.Service;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShipShop.Web.Api
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiControllerBase
    {
        #region Init Controller

        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;

        public OrderController(IErrorService errorService, IOrderService orderService, IOrderDetailService orderDetailService)
            : base(errorService)
        {
            this._orderService = orderService;
            this._orderDetailService = orderDetailService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _orderService.GetAll(new string[] { "SenderRegion", "ReceiverRegion", "User" });
                total = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<List<OrderViewModel>>(query);

                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = total,
                    TotalPages = (int)Math.Ceiling((decimal)total / pageSize),
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }


        [Route("getorderdetail")]
        [HttpGet]
        public HttpResponseMessage GetByOrderID(HttpRequestMessage request,int orderID, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _orderDetailService.GetAllByOrder(orderID);
                total = model.Count();
                var query = model.OrderByDescending(x => x.OrderDetailID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<List<OrderDetailViewModel>>(query);

                var paginationSet = new PaginationSet<OrderDetailViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = total,
                    TotalPages = (int)Math.Ceiling((decimal)total / pageSize),
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("changeOrderStatus")]
        [HttpPut]
        public HttpResponseMessage ChangeOrderStatus(HttpRequestMessage request, OrderViewModel orderVM)
        {
            return CreateHttpResponse(request, () =>
            {
                var order = _orderService.GetByID(orderVM.ID);
                order.Status = false;
                _orderService.Update(order);
                _orderService.Save();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, "");
                return response;
            });
        }
    }
}
