using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Models;
using ValuesObject;
using BookStore2019.Services;
namespace BookStore2019.Areas.Area.Controllers
{
    [Authorize]
    public class QuanLyNXBController : Controller
    {
        NhaXuatBanService nxbService = new NhaXuatBanService();
        // GET: Area/QuanLyNXB
        public ActionResult Search()
        {
            var list = nxbService.GetAll();
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ONhaXuatBan data = new ONhaXuatBan();
            data.TrangThai = false;
            
            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ONhaXuatBan model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    
                    nxbService.Add(model);
                    return RedirectToAction("Search", "QuanLyNXB");
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
                var obj = nxbService.Get((int)id);
                
                ViewBag.IsEdit = true;
                return View(obj);
            }


            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ONhaXuatBan model)
        {
            if (ModelState.IsValid)
            {
                var pro = nxbService.Get(model.MaNXB);
                if (pro != null)
                {
                    try
                    {
                        
                        nxbService.Update(model);
                        return RedirectToAction("Search", "QuanLyNXB");
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
            nxbService.Delete(id);
            return RedirectToAction("Search", "QuanLyNXB");
        }
    }
}