using BookStore2019.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;

namespace BookStore2019.Areas.Area.Controllers
{
    [Authorize]
    public class QuanLyNCCController : Controller
    {
        NhaCungCapService nccService = new NhaCungCapService();
        // GET: Area/QuanLyNCC
        public ActionResult Search()
        {
            var list = nccService.GetAll();
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ONhaCungCap data = new ONhaCungCap();
            data.TrangThai = false;

            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ONhaCungCap model)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    nccService.Add(model);
                    return RedirectToAction("Search", "QuanLyNCC");
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
                var obj = nccService.Get((int)id);

                ViewBag.IsEdit = true;
                return View(obj);
            }


            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ONhaCungCap model)
        {
            if (ModelState.IsValid)
            {
                var pro = nccService.Get(model.MaNCC);
                if (pro != null)
                {
                    try
                    {

                        nccService.Update(model);
                        return RedirectToAction("Search", "QuanLyNCC");
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
            nccService.Delete(id);
            return RedirectToAction("Search", "QuanLyNCC");
        }
    }
}