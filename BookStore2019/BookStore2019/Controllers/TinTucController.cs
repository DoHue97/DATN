using BookStore2019.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;

namespace BookStore2019.Controllers
{
    public class TinTucController : Controller
    {
        LoaiTinService loaiTinService = new LoaiTinService();
        // GET: TinTuc
        public ActionResult SearchCate()
        {
            var listCate = loaiTinService.GetAll();
            return View(listCate);
        }
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
            return View("Update", model);
        }
        //[Authorize]
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
    }
}