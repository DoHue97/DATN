using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;
using BookStore2019.Services;
namespace BookStore2019.Areas.Area.Controllers
{
    public class QuanLySlideController : Controller
    {
        SlideService slideService = new SlideService();
        // GET: Area/QuanLySlide
        public ActionResult Search()
        {
            var list = slideService.GetAll();
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            OSlide data = new OSlide();
            data.IsActive = false;
            

            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OSlide model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    
                    slideService.Add(model);
                    return RedirectToAction("Search", "QuanLySlide");
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
                var obj = slideService.Get((int)id);
                
                ViewBag.IsEdit = true;
                return View(obj);
            }


            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OSlide model)
        {
            if (ModelState.IsValid)
            {
                var pro = slideService.Get(model.SlideId);
                if (pro != null)
                {
                    try
                    {
                        
                        slideService.Update(model);
                        return RedirectToAction("Search", "QuanLySlide");
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
            slideService.Delete(new OSlide { SlideId = id });
            return RedirectToAction("Search", "QuanLySlide");
        }
    }
}