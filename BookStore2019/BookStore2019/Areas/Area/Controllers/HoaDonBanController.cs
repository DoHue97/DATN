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
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = hdbService.GetById((int)id);

                ViewBag.IsEdit = true;
                return View(obj);
            }


            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OHoaDonBan model)
        {
            if (ModelState.IsValid)
            {
                var pro = hdbService.GetById(model.MaHDB);
                if (pro != null)
                {
                    try
                    {

                        hdbService.Edit(model);
                        return RedirectToAction("Search", "HoaDonBan");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            ViewBag.IsEdit = true;
            return View(model);
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