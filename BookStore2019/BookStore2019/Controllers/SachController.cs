using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Help;
using BookStore2019.Services;
using PagedList;
using ValuesObject;

namespace BookStore2019.Controllers
{
    public class SachController : Controller
    {
        SachService sachService = new SachService();
        ChuDeService chuDeService = new ChuDeService();
        // GET: Sach
        public ActionResult GetAll(int? page=1)
         {
            int pageSize = 8;

            int total = 0;

            int endAt = (int)page * pageSize;
            int fromAt = endAt - pageSize;
            var list = sachService.GetAllActive((int)fromAt, pageSize, ref total);
            ViewBag.Page = page;
            //double pageCount = (int)Math.Ceiling(list.Count() / (double)pageSize);
            ViewBag.PageCount = total;
            ViewBag.PageSize = pageSize;
            ViewBag.FromAt = fromAt;

            return View(list);
        }
        public ActionResult Detail(string shortname)
        {
            var pro = sachService.GetByShortName(shortname);
            var listTacGia = sachService.GetNameTacgia(pro.MaSach);
            ViewBag.ListImages = sachService.GetById(pro.MaSach);
            ViewBag.ListTacGia = listTacGia;
            ViewBag.ListOrther = sachService.GetOrther(pro);
            return View(pro);
        }
        public ActionResult GetByCate(int id, int? page=1)
        {
            int pageSize = 8;

            int total = 0;

            int endAt = (int)page * pageSize;
            int fromAt = endAt - pageSize;
            var category = chuDeService.Get(new OChuDe { MaChuDe = id });
            var list = sachService.GetAllByCate((int)fromAt, pageSize, ref total, id);
            ViewBag.Page = page;
            //double pageCount = (int)Math.Ceiling(list.Count() / (double)pageSize);
            ViewBag.PageCount = list.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.FromAt = fromAt;
            ViewBag.Category = category;
            return View(list);
        }
        public ActionResult GetPaggedData(int pageNumber = 1, int pageSize = 20)
        {
            List<OSach> listData = sachService.GetAll();
            var pagedData = Pagination.PagedResult(listData, pageNumber, pageSize);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }
        #region admin
        // GET: Default
        public ActionResult SearchCate()
        {
            var listCate = chuDeService.GetAll();
            return View(listCate);
        }
        [HttpGet]
        public ActionResult CreateCate()
        {
            OSach data = new OSach();
            data.IsActive = false;
            data.IsFull = false;
            data.IsHot = false;
            List<OChuDe> listCate = chuDeService.GetByParentId();
            
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten"); ;

            return View("UpdateCate", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCate(OChuDe model)
        {
            if (ModelState.IsValid)
            {
                // var pro = ServiceFactory.NewsgoryManager.Get(new Newsgories { NewsgoryId = model.NewsgoryId });
                try
                {                    
                    chuDeService.Add(model);
                    return RedirectToAction("SearchCate", "Sach");
                }
                catch (Exception e)
                {

                }
            }
            List<OChuDe> listCate = chuDeService.GetByParentId();
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten"); ;
            ViewBag.IsEdit = true;
            return View("Update", model);
        }
        //[Authorize]
        [HttpGet]
        public ActionResult UpdateCate(int? id)
        {
            if (id.HasValue)
            {
                var obj = chuDeService.Get(new OChuDe { MaChuDe = (int)id });
                var categories = chuDeService.GetByParentId();
                var listCate = new SelectList(categories, "MaChuDe", "Ten");
                ViewBag.ListCate = chuDeService.GetByParentId();
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
        public ActionResult Search()
        {
            var listSach = sachService.GetAll();
            return View(listSach);
        }
        [HttpGet]
        public ActionResult Create()
        {
            OSach data = new OSach();
            data.IsActive = false;
            data.IsFull = false;
            data.IsHot = false;
            List<OChuDe> listCate = chuDeService.GetAll();
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten"); ;

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
                    return RedirectToAction("Search", "Sach");
                }
                catch (Exception e)
                {

                }
            }
            List<OChuDe> listCate = chuDeService.GetAll();
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten"); ;
            ViewBag.IsEdit = true;
            return View("Update", model);
        }
        //[Authorize]
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = sachService.Get(new OSach { MaSach = (int)id });
                List<OChuDe> listCate = chuDeService.GetAll();
                ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten");
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
                        return RedirectToAction("Search", "Sach");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            List<OChuDe> listCate = chuDeService.GetAll();
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten"); ;
            ViewBag.IsEdit = true;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            sachService.Delete(new OSach { MaSach = id });
            return RedirectToAction("Search", "Sach");
        }
        #endregion
        #endregion
    }
}