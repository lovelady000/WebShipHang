using System.Web.Mvc;
using System.Web.Routing;

namespace ShipShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Singer page",
                url: "trang/{alias}.html",
                defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
                namespaces: new string[] { "ShipShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "QuanTriTaiKhoan",
                url: "quan-tri-tai-khoan.html",
                defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ShipShop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Quan tri don hang",
                url: "chi-tiet-don-hang-{id}-o.html",
                defaults: new { controller = "Order", action = "OrderDetail", id = UrlParameter.Optional },
                namespaces: new string[] { "ShipShop.Web.Controllers" }
            );

            //routes.MapRoute(
            //    name: "Admin",
            //    url: "administrator.html",
            //    defaults: new { controller = "Admin", action = "Index" },
            //    namespaces: new string[] { "ShipShop.Web.Controllers" }
            //);

            routes.MapRoute(
                name: "Home page",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ShipShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ShipShop.Web.Controllers" }
            );
        }
    }
}