using BookStore2019.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;

namespace BookStore2019.Areas.Area.Controllers
{
    public class QuanLyTacGiaController : Controller
    {

        TacGiaService tgService = new TacGiaService();
        // GET: Area/QuanLyTacGia
        public ActionResult Search()
        {
            var list = tgService.GetAll();
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            OTacGia data = new OTacGia();
            data.IsActive = false;

            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OTacGia model)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    tgService.Add(model);
                    return RedirectToAction("Search", "QuanLyTacGia");
                }
                catch (Exception e)
                {

                }
            }

            ViewBag.IsEdit = true;
            return View("Update", model);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = tgService.Get((int)id);

                ViewBag.IsEdit = true;
                return View(obj);
            }


            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OTacGia model)
        {
            if (ModelState.IsValid)
            {
                var pro = tgService.Get(model.MaTacGia);
                if (pro != null)
                {
                    try
                    {

                        tgService.Update(model);
                        return RedirectToAction("Search", "QuanLyTacGia");
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
            tgService.Delete(id);
            return RedirectToAction("Search", "QuanLyTacGia");
        }
    }
}