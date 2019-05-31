using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore2019
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
               name: "login",
               url: "dang-nhap",
               defaults: new { controller = "Account", action = "Login" }
           );
            routes.MapRoute(
               name: "register",
               url: "dang-ky",
               defaults: new { controller = "Account", action = "Register" }
           );
            routes.MapRoute(
               name: "info",
               url: "thong-tin/{username}",
               defaults: new { controller = "Account", action = "Info", username=UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "search",
               url: "tim-kiem/{key}",
               defaults: new { controller = "Home", action = "Search", key=UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "contact",
               url: "lien-he",
               defaults: new { controller = "Home", action = "Contact" }
           );
            routes.MapRoute(
               name: "about",
               url: "gioi-thieu",
               defaults: new { controller = "Home", action = "About" }
           );
            #region san pham

            routes.MapRoute(
               name: "detail-product",
               url: "san-pham/{shortname}",
               defaults: new { controller = "SanPham", action = "Detail", shortname=UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "get-by-cate-product",
               url: "san-pham/{shortnamecate}/{isSach}",
               defaults: new { controller = "SanPham", action = "GetByCate", shortnamecate = UrlParameter.Optional, isSach = UrlParameter.Optional}
           );
            #endregion
            #region tin tuc
            routes.MapRoute(
               name: "getall",
               url: "tin-tuc",
               defaults: new { controller = "TinTuc", action = "GetAll" }
           );
            routes.MapRoute(
               name: "get-by-cate",
               url: "tin-tuc/{shortname}",
               defaults: new { controller = "TinTuc", action = "GetByCate", shortname=UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "detail-new",
               url: "tin-tuc/{shortnamecate}/{shortname}",
               defaults: new { controller = "TinTuc", action = "Detail", shortnamecate = UrlParameter.Optional,shortname=UrlParameter.Optional }
           );
            #endregion
            #region trang tinh
            routes.MapRoute(
               name: "detail-html",
               url: "trang-tinh/{shortnamecate}/{shortname}",
               defaults: new { controller = "Html", action = "Detail", shortnamecate = UrlParameter.Optional, shortname = UrlParameter.Optional }
           );
            #endregion
            #region dat hang
            routes.MapRoute(
               name: "order",
               url: "gio-hang",
               defaults: new { controller = "ProductAction", action = "Order" }
           );
            #endregion
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
