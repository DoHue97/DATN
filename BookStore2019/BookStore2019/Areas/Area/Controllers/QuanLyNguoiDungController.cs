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
    public class QuanLyNguoiDungController : Controller
    {
        AccountService accountService = new AccountService();
        KhachHangService khService = new KhachHangService();
        
        // GET: Area/QuanLyNguoiDung
        public ActionResult Search()
        {
            var list = accountService.GetAll();
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            OAccount data = new OAccount();
            data.TrangThai = false;
            ViewBag.Customers = new SelectList(khService.GetAll(), "MaKhach", "TenKhach");

            ViewBag.ListRoles = new SelectList(accountService.GetAllRole(), "MaQuyen", "TenQuyen");
            return View(data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OAccount model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    accountService.Add(model);
                    return RedirectToAction("Search", "QuanLyNguoiDung");
                }
                catch (Exception e)
                {

                }
            }

            ViewBag.IsEdit = true;

            ViewBag.Customers = new SelectList(khService.GetAll(), "MaKhach", "TenKhach");

            ViewBag.ListRoles = new SelectList(accountService.GetAllRole(), "MaQuyen", "TenQuyen");
            return View(model);
        }

        //[HttpGet]
        //public ActionResult Update(string matk,string username)
        //{
        //    if (id.HasValue)
        //    {
        //        var obj = accountService.GetById();

        //        ViewBag.IsEdit = true;
        //        return View(obj);
        //    }


        //    return View();
        //}
        //[HttpPost, ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        //public ActionResult Update(OAccount model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var pro = accountService.Get(model.MaNXB);
        //        if (pro != null)
        //        {
        //            try
        //            {

        //                accountService.Update(model);
        //                return RedirectToAction("Search", "QuanLyNguoiDung");
        //            }
        //            catch (Exception e)
        //            {

        //            }
        //        }
        //    }

        //    ViewBag.IsEdit = true;
        //    return View(model);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Lock(string id)
        {
            accountService.Lock(new OAccount { MaTK = Guid.Parse(id) });
            return Json(new { Success = true });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnLock(string id)
        {
            accountService.UnLock(new OAccount { MaTK = Guid.Parse(id) });

            return Json(new { Success = true });
        }
    }
}