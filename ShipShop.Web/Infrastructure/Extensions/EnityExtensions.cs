using ShipShop.Model.Models;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipShop.Web.Infrastructure.Extensions
{
    public static class EnityExtensions
    {
        public static void UpdateMenu(this Menu menu, MenuViewModel menuVM)
        {
            menu.ID = menuVM.ID;
            menu.Name = menuVM.Name;
            menu.URL = menuVM.URL;
            menu.DisplayOrder = menuVM.DisplayOrder;
            menu.GroupID = menuVM.GroupID;
            menu.Target = menuVM.Target;
            menu.Status = menuVM.Status;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderVM)
        {
            order.SenderName = orderVM.SenderName;
            order.SenderAddress = orderVM.SenderAddress;
            order.SenderMobile = orderVM.SenderMobile;
            order.SenderRegionID = orderVM.SenderRegionID;
            order.ReceiverName = orderVM.ReceiverName;
            order.ReceiverAddress = orderVM.ReceiverAddress;
            order.ReceiverMobile = orderVM.ReceiverMobile;
            order.ReceiverRegionID = orderVM.ReceiverRegionID;
            order.PayCOD = orderVM.PayCOD;
        }

        public static void UpdateOrderDetail(this OrderDetail orderDetail, OrderDetailViewModel orderDetailVM)
        {
            orderDetail.NameProduct = orderDetailVM.NameProduct;
            orderDetail.UrlProductDetail = orderDetailVM.UrlProductDetail;
            orderDetail.Note = orderDetailVM.Note;
        }

    }
}