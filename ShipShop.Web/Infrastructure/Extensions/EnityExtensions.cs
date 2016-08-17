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
            order.Note = orderVM.Note;
        }

        public static void UpdateOrderDetail(this OrderDetail orderDetail, OrderDetailViewModel orderDetailVM)
        {
            orderDetail.NameProduct = orderDetailVM.NameProduct;
            orderDetail.UrlProductDetail = orderDetailVM.UrlProductDetail;
            orderDetail.Note = orderDetailVM.Note;
        }
        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }

        //public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        //{
        //    appUser.Id = appUserViewModel.Id;
        //    appUser.FullName = appUserViewModel.FullName;
        //    appUser.BirthDay = appUserViewModel.BirthDay;
        //    appUser.Email = appUserViewModel.Email;
        //    appUser.UserName = appUserViewModel.UserName;
        //    appUser.PhoneNumber = appUserViewModel.PhoneNumber;
        //}

        public static void UpdateNews(this News news, NewsViewModel newsViewModel)
        {
            news.Name = newsViewModel.Name;
            news.Order = newsViewModel.Order;
            news.Status = newsViewModel.Status;
            news.Url = newsViewModel.Url;
        }
        public static void UpdateWebInformation(this WebInformation webInformation, WebInformationViewModel webInformationVM)
        {
            webInformation.Slogan = webInformationVM.Slogan;
            webInformation.HotLine = webInformationVM.HotLine;
            webInformation.Skyper = webInformationVM.Skyper;
            webInformation.Facebook = webInformationVM.Facebook;
            webInformation.LinkAppIOS = webInformationVM.LinkAppIOS;
            webInformation.LinkAppAndroid = webInformationVM.LinkAppAndroid;
            webInformation.LinkAppWindowPhone = webInformationVM.LinkAppWindowPhone;
            webInformation.Latitude = webInformationVM.Latitude;
            webInformation.Longitude = webInformationVM.Longitude;
        }
    }
}