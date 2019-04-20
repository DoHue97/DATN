using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;
using BookStore2019.Services;

namespace BookStore2019.Areas.Area.Controllers
{
    [Authorize]
    public class HoaDonBanController : Controller
    {
        HDBService hdbService = new HDBService();
        CTHDBService cthdbService = new CTHDBService();
        // GET: Area/HoaDonBan
        public ActionResult Search()
        {
            var list = hdbService.GetAll();
            return View(list);
        }
        public ActionResult Details(int id)
        {
            var listDetails = cthdbService.GetAll(id);
            return View(listDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            return Json(new { Success = true, Message = "Xóa thành công!" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDetail(int orderid,int proid)
        {
            return Json(new { Success = true, Message = "Xóa thành công!" });
        }
    }
}