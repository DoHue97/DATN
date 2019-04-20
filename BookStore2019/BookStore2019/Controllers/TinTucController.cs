﻿using BookStore2019.Services;
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
        public ActionResult Detail(string shortname)
        {
            var item = tinTucService.GetShortName(shortname);
            ViewBag.ListHot = tinTucService.GetHot();
            return View(item);
        }
        #endregion
        #region Admin
        [Authorize]
        public ActionResult SearchCate()
        {
            var listCate = loaiTinService.GetAll();
            return View(listCate);
        }
        [Authorize]
        [HttpGet]
        public ActionResult CreateCate()
        {
            OLoaiTin data = new OLoaiTin();
            data.IsActive = false;

            return View("UpdateCate", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCate(OLoaiTin model)
        {
            if (ModelState.IsValid)
            {
                // var pro = ServiceFactory.NewsgoryManager.Get(new Newsgories { NewsgoryId = model.NewsgoryId });
                try
                {
                    model.ShortName = Help.Helper.convertToUnSign3(model.Ten);
                    loaiTinService.Add(model);
                    return RedirectToAction("SearchCate", "TinTuc");
                }
                catch (Exception e)
                {

                }
            }

            ViewBag.IsEdit = true;
            return View("UpdateCate", model);
        }
        [Authorize]
        [HttpGet]
        public ActionResult UpdateCate(int? id)
        {
            if (id.HasValue)
            {
                var obj = loaiTinService.Get((int)id);

                ViewBag.IsEdit = true;
                return View(obj);
            }
            //var listCate = new SelectList(chuDeService.GetAll(), "ParentId", "Ten");
            //ViewBag.Categories = listCate;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCate(OLoaiTin model)
        {
            if (ModelState.IsValid)
            {
                var pro = loaiTinService.Get(model.MaLoaiTin);
                if (pro != null)
                {
                    try
                    {

                        loaiTinService.Update(model);
                        return RedirectToAction("SearchCate", "TinTuc");
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
        public ActionResult DeleteCate(int id)
        {
            loaiTinService.Delete(new OLoaiTin { MaLoaiTin = id });
            return RedirectToAction("SearchCate", "TinTuc");
        }

        //tin tuc
        [Authorize]
        [HttpGet]
        public ActionResult Search()
        {
            var listCate = loaiTinService.GetAllActive();
            ViewBag.ListCates = new SelectList(listCate, "MaLoaiTin", "Ten");
            var list = tinTucService.GetAll();
            return View(list);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            OTinTuc data = new OTinTuc();
            data.IsActive = false;
            var listCate = loaiTinService.GetAllActive();
            ViewBag.ListCates = new SelectList(listCate, "MaLoaiTin", "Ten");
            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OTinTuc model)
        {
            if (ModelState.IsValid)
            {
                // var pro = ServiceFactory.NewsgoryManager.Get(new Newsgories { NewsgoryId = model.NewsgoryId });
                try
                {
                    model.ShortName = Help.Helper.convertToUnSign3(model.TieuDe);
                    tinTucService.Add(model);
                    return RedirectToAction("Search", "TinTuc");
                }
                catch (Exception e)
                {

                }
            }
            var listCate = loaiTinService.GetAllActive();
            ViewBag.ListCates = new SelectList(listCate, "MaLoaiTin", "Ten");
            ViewBag.IsEdit = true;
            return View("Update", model);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = tinTucService.Get((int)id);

                ViewBag.IsEdit = true;
                return View(obj);
            }
            var listCate = new SelectList(loaiTinService.GetAllActive(), "MaLoaiTin", "Ten");
            ViewBag.ListCates = listCate;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OTinTuc model)
        {
            if (ModelState.IsValid)
            {
                var pro = tinTucService.Get(model.MaTin);
                if (pro != null)
                {
                    try
                    {
                        model.ShortName = Help.Helper.convertToUnSign3(model.TieuDe);
                        tinTucService.Update(model);
                        return RedirectToAction("Search", "TinTuc");
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
            tinTucService.Delete(new OTinTuc { MaTin = id });
            return RedirectToAction("Search", "TinTuc");
        }
        #endregion


    }
}