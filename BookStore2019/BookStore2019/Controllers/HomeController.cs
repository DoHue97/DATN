using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Services;
namespace BookStore2019.Controllers
{
    public class HomeController : Controller
    {
        SachService sachService = new SachService();
        SlideService slideService = new SlideService();
        public ActionResult Index()
        {
            int total = 0;
            var listHot = sachService.GetHot(true);
            ViewBag.ListHot = listHot;
            var listSlide = slideService.GetAllActive();
            ViewBag.ListSlide = listSlide;
            var listHotByTag = sachService.GetHotByTag("tieu-thuyet");
            ViewBag.ListHotByTag = listHotByTag;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Search(FormCollection f, int? page = 1)
        {
            string key = f["Search"].ToString();
            if (key == null || key == "")
            {
                return RedirectToAction("GetAll", "Sach");
            }
            int pageSize = 8;
            int total = 0;
            int endAt = (int)page * pageSize;
            int fromAt = endAt - pageSize;
            var list = sachService.Search((int)fromAt, pageSize, ref total, key);
            ViewBag.Page = page;
            ViewBag.PageCount = list.Count;
            ViewBag.PageSize = pageSize;
            ViewBag.FromAt = fromAt;
            ViewBag.Key = key;
            return View(list);
        }
        //[HttpPost]
        //public ActionResult Search(FormCollection f)
        //{
        //    string key = f["Search"].ToString();
        //    var totalCount = 0;
            
        //    var list = sachService.Search();
            
        //    return View(new StaticPagedList<OSach>(list, page, pageSize, totalCount).ToPagedList(page, pageSize));
        //}
    }
}