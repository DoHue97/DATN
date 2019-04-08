using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Services;
using ValuesObject;

namespace BookStore2019.Areas.Area.Controllers
{
    public class QuanLyTrangTinhController : Controller
    {
        HtmlPageService htmlPageService = new HtmlPageService();
        LoaiTrangTinhService loaiTrangTinhService = new LoaiTrangTinhService();

        [HttpGet]
        public ActionResult SearchCate()
        {
            var list = loaiTrangTinhService.GetAll();
            return View(list);
        }
        [HttpGet]
        public ActionResult CreateCate()
        {
            OLoaiTrangTinh data = new OLoaiTrangTinh();
            data.IsActive = false;

            return View("UpdateCate", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCate(OLoaiTrangTinh model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    model.TenVanTat = Help.Helper.convertToUnSign3(model.TenLoai);
                    loaiTrangTinhService.Add(model);
                    return RedirectToAction("SearchCate", "QuanLyTrangTinh");
                }
                catch (Exception e)
                {

                }
            }

            ViewBag.IsEdit = true;
            return View("Update", model);
        }

        [HttpGet]
        public ActionResult UpdateCate(int? id)
        {
            if (id.HasValue)
            {
                var obj = htmlPageService.Get((int)id);

                ViewBag.IsEdit = true;
                return View(obj);
            }


            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCate(OLoaiTrangTinh model)
        {
            if (ModelState.IsValid)
            {
                var pro = loaiTrangTinhService.Get(model.MaLoai);
                if (pro != null)
                {
                    try
                    {
                        model.TenVanTat = Help.Helper.convertToUnSign3(model.TenLoai);
                        loaiTrangTinhService.Update(model);
                        return RedirectToAction("SearchCate", "QuanLyTrangTinh");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            ViewBag.IsEdit = true;
            return View(model);
        }

        [HttpGet]
        public ActionResult Search()
        {
            var list = htmlPageService.GetAll();
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            OTrangTinh data = new OTrangTinh();
            data.IsActive = false;
            ViewBag.LoaiTrangTinh = new SelectList(loaiTrangTinhService.GetAllActive(), "MaLoai", "TenLoai");

            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OTrangTinh model)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    model.TenVanTat = Help.Helper.convertToUnSign3(model.TenTrang);
                    htmlPageService.Add(model);
                    return RedirectToAction("Search", "QuanLyTrangTinh");
                }
                catch (Exception e)
                {

                }
            }
            ViewBag.LoaiTrangTinh = new SelectList(loaiTrangTinhService.GetAllActive(), "MaLoai", "TenLoai");
            ViewBag.IsEdit = true;
            return View("Update", model);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = htmlPageService.Get((int)id);
                ViewBag.LoaiTrangTinh = new SelectList(loaiTrangTinhService.GetAllActive(), "MaLoai", "TenLoai");
                ViewBag.IsEdit = true;
                return View(obj);
            }

            
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OTrangTinh model)
        {
            if (ModelState.IsValid)
            {
                var pro = htmlPageService.Get(model.MaTrangTinh);
                if (pro != null)
                {
                    try
                    {
                        model.TenVanTat = Help.Helper.convertToUnSign3(model.TenTrang);
                        htmlPageService.Update(model);
                        return RedirectToAction("Search", "QuanLyTrangTinh");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            ViewBag.LoaiTrangTinh = new SelectList(loaiTrangTinhService.GetAllActive(), "MaLoai", "TenLoai");
            ViewBag.IsEdit = true;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            htmlPageService.Delete(new OTrangTinh { MaTrangTinh = id });
            return RedirectToAction("Search", "QuanLyTrangTinh");
        }
    }
}