﻿using ShipShop.Model.Models;
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
            order.SenderView = orderVM.SenderView;
            order.ReceiverView = orderVM.ReceiverView;
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

        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {
            appUser.UserName = appUserViewModel.UserName;
        }

        public static void UpdateNews(this News news, NewsViewModel newsViewModel)
        {
            news.Name = newsViewModel.Name;
            news.Order = newsViewModel.Order;
            news.Status = newsViewModel.Status;
            news.Url = newsViewModel.Url;
        }
        public static void UpdateWebInformation(this WebInformation webInformation, WebInformationViewModel webInformationVM)
        {
            webInformation.Logo = webInformationVM.Logo;
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
        public static void UpdateDVTB(this DonViTieuBieu dvtb, DonViTieuBieuViewModel dvtbVM)
        {
            dvtb.Image = dvtbVM.Image;
            dvtb.Name = dvtbVM.Name;
            dvtb.Order = dvtbVM.Order;
            dvtb.Url = dvtbVM.Url;
        }

        public static void UpdatePage(this Page page, PageViewModel pageVM)
        {
            page.Name = pageVM.Name;
            page.Alias = pageVM.Alias;
            page.Content = pageVM.Content;
            page.CreatedDate = pageVM.CreatedDate;
            page.CreatedBy = pageVM.CreatedBy;
            page.UpdatedDate = pageVM.UpdatedDate;
            page.UpdatedBy = pageVM.UpdatedBy;
            page.MetaKeyword = pageVM.MetaKeyword;
            page.MetaDescription = pageVM.MetaDescription;
            page.Status = pageVM.Status;
        }

        public static void UpdateArea(this Area area, AreaViewModel areaViewModel)
        {
            area.Name = areaViewModel.Name;
        }

        public static void UpdateRegion(this Region region, RegionViewModel regionViewModel)
        {
            region.Name = regionViewModel.Name;
            region.AreaID = regionViewModel.AreaID;
        }
        public static void UpdateSlide(this Slide slide, SlideViewModel slideViewModel)
        {
            slide.Name = slideViewModel.Name;
            slide.Description = slideViewModel.Description;
            slide.Image = slideViewModel.Image;
            slide.Url = slideViewModel.Url;
            slide.DisplayOrder = slideViewModel.DisplayOrder;
            slide.Status = slide.Status;
        }

        public static void UpdatePostCategory(this PostCategory postCate, PostCategoryViewModel postCateVM)
        {
            postCate.Name = postCateVM.Name;
            postCate.Alias = postCateVM.Alias;
            postCate.Description = postCateVM.Description;
            postCate.ParentID = postCateVM.ParentID;
            postCate.DisplayOrder = postCateVM.DisplayOrder;
            postCate.Image = postCateVM.Image;
            postCate.HomeFlag = postCateVM.HomeFlag;
            postCate.CreatedDate = postCateVM.CreatedDate;
            postCate.CreatedBy = postCateVM.CreatedBy;
            postCate.UpdatedDate = postCateVM.UpdatedDate;
            postCate.UpdatedBy = postCateVM.UpdatedBy;
            postCate.MetaKeyword = postCateVM.MetaKeyword;
            postCate.MetaDescription = postCateVM.MetaDescription;
            postCate.Status = postCateVM.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postVM)
        {
            post.Name = postVM.Name;
            post.Alias = postVM.Alias;
            post.CategoryID = postVM.CategoryID;
            post.Image = postVM.Image;
            post.Description = postVM.Description;
            post.Content = postVM.Content;
            post.HomeFlag = postVM.HomeFlag;
            post.HotFlag = postVM.HotFlag;
            post.ViewCount = postVM.ViewCount;

            post.CreatedDate = postVM.CreatedDate;
            post.CreatedBy = postVM.CreatedBy;

            post.UpdatedDate = postVM.UpdatedDate;
            post.UpdatedBy = postVM.UpdatedBy;
            post.MetaKeyword = postVM.MetaKeyword;
            post.MetaDescription = postVM.MetaDescription;
            post.Status = postVM.Status;
        }
    }
}