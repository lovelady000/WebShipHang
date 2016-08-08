using AutoMapper;
using ShipShop.Model.Models;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipShop.Web.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure(){
            Mapper.CreateMap<Menu, MenuViewModel>();
            Mapper.CreateMap<MenuGroup, MenuGroupViewModel>();
            Mapper.CreateMap<DonViTieuBieu, DonViTieuBieuViewModel>();
            Mapper.CreateMap<News, NewsViewModel>();
        }
    }
}