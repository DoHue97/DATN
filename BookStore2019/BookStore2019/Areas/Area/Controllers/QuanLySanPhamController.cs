using BookStore2019.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;

namespace BookStore2019.Areas.Area.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        ChuDeService chuDeService = new ChuDeService();
        SachService sachService = new SachService();
        NhaXuatBanService nxbService = new NhaXuatBanService();
        #region admin
        // GET: Default
        
        [HttpGet]
        public ActionResult SearchCate()
        {
            var listCate = chuDeService.GetAll();
            return View(listCate);
        }
        
        [HttpGet]
        public ActionResult CreateCate()
        {
            OChuDe data = new OChuDe();
            data.IsActive = false;

            List<OChuDe> listCate = chuDeService.GetByParentId();

            ViewBag.ListCate = new SelectList(listCate, "ParentId", "Ten"); ;

            return View("UpdateCate", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCate(OChuDe model)
        {

            try
            {
                chuDeService.Add(model);
                return RedirectToAction("SearchCate", "QuanLySanPham");
            }
            catch (Exception e)
            {

            }
            List<OChuDe> listCate = chuDeService.GetByParentId();
            ViewBag.ListCate = new SelectList(listCate, "ParentId", "Ten"); ;
            ViewBag.IsEdit = true;
            return View("UpdateCate", model);
        }
        
        [HttpGet]
        public ActionResult UpdateCate(int? id)
        {
            if (id.HasValue)
            {
                var obj = chuDeService.Get(new OChuDe { MaChuDe = (int)id });
                var categories = chuDeService.GetByParentId();
                var listCate = new SelectList(categories, "ParentId", "Ten");
                ViewBag.ListCate = listCate;
                ViewBag.IsEdit = true;
                return View(obj);
            }
            //var listCate = new SelectList(chuDeService.GetAll(), "ParentId", "Ten");
            //ViewBag.Categories = listCate;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCate(OChuDe model)
        {
            if (ModelState.IsValid)
            {
                var pro = chuDeService.Get(new OChuDe { MaChuDe = model.MaChuDe });
                if (pro != null)
                {
                    try
                    {

                        chuDeService.Update(model);
                        return RedirectToAction("SearchCate", "QuanLySanPham");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            var categories = chuDeService.GetByParentId();
            var listCate = new SelectList(categories, "ParentId", "Ten");
            ViewBag.ListCate = listCate;
            ViewBag.IsEdit = true;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCate(int id)
        {
            chuDeService.Delete(new OChuDe { MaChuDe = id });
            return RedirectToAction("SearchCate", "QuanLySanPham");
        }
        #region sach
        
        [HttpGet]
        public ActionResult Search(bool isSach)
        {
            var listSach = sachService.GetAll(isSach);
            return View(listSach);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            OSach data = new OSach();
            data.IsActive = false;

            data.IsHot = false;
            data.IsSach = false;
            List<OChuDe> listCate = chuDeService.GetAll();
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten"); ;
            List<ONhaXuatBan> listNXB = nxbService.GetAll();
            ViewBag.ListNXB = new SelectList(listNXB, "MaNXB", "TenNXB");
            return View("Update", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OSach model)
        {
            if (ModelState.IsValid)
            {
                // var pro = ServiceFactory.NewsgoryManager.Get(new Newsgories { NewsgoryId = model.NewsgoryId });
                try
                {
                    model.TenVanTat = Help.Helper.convertToUnSign3(model.TenSach);
                    sachService.Add(model);
                    return RedirectToAction("Search", "QuanLySanPham", new { isSach = model.IsSach });
                }
                catch (Exception e)
                {

                }
            }
            List<OChuDe> listCate = chuDeService.GetAll();
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten");
            List<ONhaXuatBan> listNXB = nxbService.GetAll();
            ViewBag.ListNXB = new SelectList(listNXB, "MaNXB", "TenNXB");
            ViewBag.IsEdit = true;
            return View("Update", model);
        }
        
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = sachService.Get(new OSach { MaSach = (int)id });
                List<OChuDe> listCate = chuDeService.GetAll();
                ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten");
                List<ONhaXuatBan> listNXB = nxbService.GetAll();
                ViewBag.ListNXB = new SelectList(listNXB, "MaNXB", "TenNXB");
                ViewBag.IsEdit = true;
                return View(obj);
            }

            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OSach model)
        {
            if (ModelState.IsValid)
            {
                var pro = sachService.Get(new OSach { MaSach = model.MaSach });
                if (pro != null)
                {
                    try
                    {
                        model.TenVanTat = Help.Helper.convertToUnSign3(model.TenSach);
                        //model.CreateBy = CurrentUser.Name;
                        sachService.Update(model);
                        return RedirectToAction("Search", "QuanLySanPham", new { isSach = model.IsSach });
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            List<OChuDe> listCate = chuDeService.GetAll();
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten");
            List<ONhaXuatBan> listNXB = nxbService.GetAll();
            ViewBag.ListNXB = new SelectList(listNXB, "MaNXB", "TenNXB");
            ViewBag.IsEdit = true;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var pro = sachService.Get(new OSach { MaSach = id });
            bool isSach = (bool)pro.IsSach;
            sachService.Delete(new OSach { MaSach = id });
            return RedirectToAction("Search", "QuanLySanPham", new { isSach = isSach });
        }
        #endregion
        #endregion
    }
}