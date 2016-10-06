using AutoMapper;
using ShipShop.Service;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Infrastructure.Extensions;
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
        [Authorize(Roles = Common.RolesConstants.ROLES_GET_LIST_ORDER + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int typeOrder,int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int type = typeOrder;
                int total = 0;
                var model = _orderService.GetAll(new string[] { "SenderRegion", "ReceiverRegion", "User" });
                var tlist = model.ToList();
                if(keyword != null && keyword!= "")
                {
                    model = model.Where(x => x.User.WebOrShopName != null && x.User.WebOrShopName.ToUnSign().IndexOf(keyword.ToUnSign()) != -1 || x.Username == keyword);
                }
                if(type == 1)
                {
                    model = model.Where(x => x.User.Vendee);
                }
                if (type == 2)
                {
                    model = model.Where(x => !x.User.Vendee);
                }
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

        [Route("getorderbyid")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_GET_LIST_ORDER + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage GetOrderByID(HttpRequestMessage request, int orderID)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _orderService.GetByID(orderID,new string[] { "SenderRegion", "ReceiverRegion" });
                var responseData = Mapper.Map<OrderViewModel>(model);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }


        [Route("getorderdetail")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_GET_LIST_ORDER + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage GetOrderDetailByOrderID(HttpRequestMessage request,int orderID, int page, int pageSize = 10)
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
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_ORDER + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
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
