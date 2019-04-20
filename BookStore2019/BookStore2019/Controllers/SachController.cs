using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Help;
using BookStore2019.Models;
using BookStore2019.Services;
using PagedList;
using ValuesObject;

namespace BookStore2019.Controllers
{
    [AllowAnonymous]
    public class SachController : Controller
    {
        int pageSize = 20;
        SanPhamService sachService = new SanPhamService();
        ChuDeService chuDeService = new ChuDeService();
        NhaXuatBanService nxbService = new NhaXuatBanService();
        // GET: Sach
        
        public ActionResult GetAll(int? page=1)
        {
            
            int total = 0;
            int endAt = (int)page * pageSize;
            int fromAt = endAt - pageSize;
            var list = sachService.GetAllActive((int)fromAt, pageSize, ref total);

            ViewBag.Pager = Pager.Items(total).PerPage(pageSize).Move((int)page).Segment(5).Center();
            return View(list);
        }
        public ActionResult Detail(string shortname)
        {
            var pro = sachService.GetByShortName(shortname);
            ViewBag.ListImages = sachService.GetById(pro.MaSanPham);
            var listTacGia = sachService.GetNameTacgia(pro.MaSanPham);
            ViewBag.ListTacGia = listTacGia;
            ViewBag.ListOrther = sachService.GetOrther(pro);
            return View(pro);
        }
        public ActionResult GetByCate(int id, int? page=1)
        {
            
            var category = chuDeService.Get(new OChuDe { MaChuDe = id });
            
            int total = 0;
            int endAt = (int)page * pageSize;
            int fromAt = endAt - pageSize;
            var list = sachService.GetAllByCate((int)fromAt, pageSize, ref total,id);
            ViewBag.Category = category;
            ViewBag.Pager = Pager.Items(total).PerPage(pageSize).Move((int)page).Segment(5).Center();
            return View(list);
        }
       
        #region admin
        // GET: Default
        [Authorize]
        [HttpGet]
        public ActionResult SearchCate()
        {
            var listCate = chuDeService.GetAll();
            return View(listCate);
        }
        [Authorize]
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
                return RedirectToAction("SearchCate", "Sach");
            }
            catch (Exception e)
            {

            }
            List<OChuDe> listCate = chuDeService.GetByParentId();
            ViewBag.ListCate = new SelectList(listCate, "ParentId", "Ten"); ;
            ViewBag.IsEdit = true;
            return View("UpdateCate", model);
        }
        [Authorize]
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
                        return RedirectToAction("SearchCate", "Sach");
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
            return RedirectToAction("SearchCate", "Sach");
        }
        #region sach
        [Authorize]
        [HttpGet]
        public ActionResult Search(bool isSach)
        {
            var listSach = sachService.GetAll(isSach);
            return View(listSach);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            OSanPham data = new OSanPham();
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
        public ActionResult Create(OSanPham model)
        {
            if (ModelState.IsValid)
            {
                // var pro = ServiceFactory.NewsgoryManager.Get(new Newsgories { NewsgoryId = model.NewsgoryId });
                try
                {
                    model.TenVanTat = Help.Helper.convertToUnSign3(model.TenSanPham);
                    sachService.Add(model);
                    return RedirectToAction("Search", "Sach",new { isSach=model.IsSach});
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
        [Authorize]
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = sachService.Get(new OSanPham { MaSanPham = (int)id });
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
        public ActionResult Update(OSanPham model)
        {
            if (ModelState.IsValid)
            {
                var pro = sachService.Get(new OSanPham { MaSanPham = model.MaSanPham });
                if (pro != null)
                {
                    try
                    {
                        model.TenVanTat = Help.Helper.convertToUnSign3(model.TenSanPham);
                        //model.CreateBy = CurrentUser.Name;
                        sachService.Update(model);
                        return RedirectToAction("Search", "Sach", new { isSach = model.IsSach });
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
            var pro = sachService.Get(new OSanPham { MaSanPham = id });
            bool isSach = (bool)pro.IsSach;
            sachService.Delete(new OSanPham { MaSanPham = id });
            return RedirectToAction("Search", "Sach", new { isSach = isSach });
        }
        #endregion
        #endregion
    }
}