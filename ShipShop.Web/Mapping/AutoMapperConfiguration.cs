using AutoMapper;
using ShipShop.Model.Models;
using ShipShop.Web.Models;

namespace ShipShop.Web.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Menu, MenuViewModel>();
            Mapper.CreateMap<MenuGroup, MenuGroupViewModel>();
            Mapper.CreateMap<DonViTieuBieu, DonViTieuBieuViewModel>();
            Mapper.CreateMap<News, NewsViewModel>();
            Mapper.CreateMap<Region, RegionViewModel>();
            Mapper.CreateMap<Area, AreaViewModel>();

            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<OrderDetail, OrderDetailViewModel>();
            Mapper.CreateMap<WebInformation, WebInformationViewModel>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            Mapper.CreateMap<Page, PageViewModel>();
            Mapper.CreateMap<Slide, SlideViewModel>();

            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<PostTag, PostTagViewModel>();
        }
    }
}