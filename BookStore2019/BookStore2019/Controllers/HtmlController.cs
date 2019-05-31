using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Services;
using ValuesObject;
namespace BookStore2019.Controllers
{
    [AllowAnonymous]
    public class HtmlController : Controller
    {
        HtmlPageService htmlPageService = new HtmlPageService();
        LoaiTrangTinhService loaiTTService = new LoaiTrangTinhService();
        // GET: Html
        public ActionResult Detail(string shortnamecate,string shortname)
        {
            var item = htmlPageService.GetByShortName(shortname);
            return View(item);
        }
    }
}