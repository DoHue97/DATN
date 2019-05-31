using BookStore2019.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;

namespace BookStore2019.Controllers
{
    [AllowAnonymous]
    public class TinTucController : Controller
    {
        LoaiTinService loaiTinService = new LoaiTinService();
        TinTucService tinTucService = new TinTucService();
        // GET: TinTuc
        #region user
        public ActionResult GetAll()
        {
            var list = tinTucService.GetAllActive();
            ViewBag.ListHot = tinTucService.GetHot();
            return View(list);
        }
        public ActionResult GetByCate(string shortname)
        {
            var list = tinTucService.GetAllByCateShortName(shortname);
            ViewBag.Cate = loaiTinService.GetShortName(shortname);
            ViewBag.ListHot = tinTucService.GetHot();
            return View(list);
        }
        public ActionResult Detail(string shortnamecate,string shortname)
        {
            var item = tinTucService.GetShortName(shortname);
            ViewBag.ListHot = tinTucService.GetHot();
            var itemCate = loaiTinService.GetShortName(shortnamecate);
            ViewBag.Category = itemCate;
            return View(item);
        }
        #endregion
        
    }
}