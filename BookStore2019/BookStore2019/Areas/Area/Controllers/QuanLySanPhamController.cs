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
    public class QuanLySanPhamController : Controller
    {
        ChuDeService chuDeService = new ChuDeService();
        SanPhamService sanphamService = new SanPhamService();
        NhaXuatBanService nxbService = new NhaXuatBanService();
        TacGiaService tacGiaService = new TacGiaService();
        Sach_TacGiaService sach_TacGiaService = new Sach_TacGiaService();
        NhaCungCapService nccService = new NhaCungCapService();
        ImagesService imgService = new ImagesService();
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
            data.TrangThai = false;

            List<OChuDe> listCate = chuDeService.GetByParentId();
            ViewBag.ListTacGia = new SelectList(tacGiaService.GetAll(), "MaTacGia", "Ten");
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten");

            return View("UpdateCate", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCate(OChuDe model)
        {

            try
            {

                chuDeService.Add(model);
                var idSach = sanphamService.GetLastId();

                return RedirectToAction("SearchCate", "QuanLySanPham");
            }
            catch (Exception e)
            {

            }
            List<OChuDe> listCate = chuDeService.GetByParentId();
            ViewBag.ListCate = new SelectList(listCate, "MaChuDeCha", "Ten");

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
                var listCate = new SelectList(categories, "MaChuDe", "Ten");
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
            var listCate = new SelectList(categories, "MaChuDeCha", "Ten");
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
            var listSach = sanphamService.GetAll(isSach);
            ViewBag.IsSach = isSach;
            return View(listSach);
        }

        [HttpGet]
        public ActionResult Create(bool isSach)
        {
            OSanPham data = new OSanPham();
            data.TrangThai = false;

            data.SanPhamHot = false;
            data.IsSach = isSach;
            List<OChuDe> listCate = chuDeService.GetAll();
            ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten");
            List<ONhaXuatBan> listNXB = nxbService.GetAll();
            ViewBag.ListNXB = new SelectList(listNXB, "MaNXB", "TenNXB");
            ViewBag.ListTacGia = new SelectList(tacGiaService.GetAll(), "MaTacGia", "Ten");
            ViewBag.ListNCC = new SelectList(nccService.GetAllActive(), "MaNCC", "TenNCC");



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
                    model.TuKhoa += Help.Helper.convertLower(model.TenSanPham + ", ");

                    var category = chuDeService.Get(new OChuDe { MaChuDe = (int)model.MaChuDe });
                    model.TuKhoa += Help.Helper.convertLower(category.Ten + ",");

                    var account = (OAccount)Session["Account"];
                    model.NguoiTao = (Guid)account.MaTK;
                    if(model.IsSach!=null && model.IsSach == true)
                    {
                        foreach (var item in model.MaTacGia)
                        {
                            var tacgia = tacGiaService.Get(item);
                            model.TuKhoa += Help.Helper.convertLower(tacgia.Ten + ",");
                        }
                    }
                    
                    //if (model.IsSach == true)
                    //{
                    //    System.IO.FileStream fs = new System.IO.FileStream("/sach/" + model.TenVanTat, System.IO.FileMode.Create);
                    //}
                    //else
                    //{
                    //    System.IO.FileStream fs = new System.IO.FileStream("/do-dung-hoc-tap/" + model.TenVanTat, System.IO.FileMode.Create);
                    //}

                    sanphamService.Add(model);
                    if (model.IsSach != null && model.IsSach == true)
                    {
                        var idSach = sanphamService.GetLastId();
                        if (model.MaTacGia.ToList() != null && model.MaTacGia.ToList().Count > 0)
                        {
                            foreach (int item in model.MaTacGia)
                            {

                                sach_TacGiaService.Add(new OSach_TacGia
                                {
                                    MaTacGia = item,
                                    MaSanPham = idSach,
                                });

                            }
                        }
                    }

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
            ViewBag.ListTacGia = new SelectList(tacGiaService.GetAll(), "MaTacGia", "Ten");
            ViewBag.ListNCC = new SelectList(nccService.GetAllActive(), "MaNCC", "TenNCC");
            ViewBag.IsEdit = true;
            return View("Update", model);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                var obj = sanphamService.Get(new OSanPham { MaSanPham = (int)id });
                List<OChuDe> listCate = chuDeService.GetAll();
                ViewBag.ListCate = new SelectList(listCate, "MaChuDe", "Ten");
                List<ONhaXuatBan> listNXB = nxbService.GetAll();
                ViewBag.ListNXB = new SelectList(listNXB, "MaNXB", "TenNXB");

                ViewBag.ListSelected = tacGiaService.GetByMaSanPham((int)id);

                ViewBag.ListTacGia = tacGiaService.GetAll();
                ViewBag.ListNCC = new SelectList(nccService.GetAllActive(), "MaNCC", "TenNCC");
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
                var pro = sanphamService.Get(new OSanPham { MaSanPham = model.MaSanPham });
                if (pro != null)
                {
                    try
                    {
                        model.TenVanTat = Help.Helper.convertToUnSign3(model.TenSanPham);
                        //model.CreateBy = CurrentUser.Name;
                        if (model.IsSach != null && model.IsSach == true)
                        {
                            foreach (var item in model.MaTacGia)
                            {
                                var tacgia = tacGiaService.Get(item);
                                model.TuKhoa += Help.Helper.convertLower(tacgia.Ten + ",");
                            }
                        }
                        sanphamService.Update(model);
                        sach_TacGiaService.Delete(model.MaSanPham);

                        
                        if (model.MaTacGia != null && model.MaTacGia.ToList().Count > 0)
                        {
                            foreach (int item in model.MaTacGia)
                            {
                                sach_TacGiaService.Add(new OSach_TacGia
                                {
                                    MaTacGia = item,
                                    MaSanPham = model.MaSanPham,
                                });
                            }

                        }

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
            ViewBag.ListTacGia = tacGiaService.GetAllActive();
            ViewBag.ListNCC = new SelectList(nccService.GetAllActive(), "MaNCC", "TenNCC");
            ViewBag.IsEdit = true;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var pro = sanphamService.Get(new OSanPham { MaSanPham = id });
            bool isSach = (bool)pro.IsSach;
            sanphamService.Delete(new OSanPham { MaSanPham = id });
            return RedirectToAction("Search", "QuanLySanPham", new { isSach = isSach });
        }

        [HttpGet]
        public ActionResult Images(int masp)
        {
            var list = imgService.GetAll(masp);
            var product = sanphamService.Get(new OSanPham { MaSanPham = masp });
            ViewBag.Product = product;
            return View(list);
        }
        [HttpGet]
        public ActionResult CreateImage(int masp)
        {
            OImageSach data = new OImageSach();
            data.TrangThai = false;
            data.MaSanPham = masp;
            var product = sanphamService.Get(new OSanPham { MaSanPham = masp });
            ViewBag.Product = product;
            return View("UpdateImage", data);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateImage(OImageSach model)
        {

            try
            {
                imgService.Add(model);

                return RedirectToAction("Images", "QuanLySanPham", new { masp = model.MaSanPham });
            }
            catch (Exception e)
            {

            }

            ViewBag.IsEdit = true;
            return View("UpdateImage", model);
        }

        [HttpGet]
        public ActionResult UpdateImage(int? id)
        {
            if (id.HasValue)
            {
                var obj = imgService.Get(new OImageSach { MaAnh = (int)id });
                var product = sanphamService.Get(new OSanPham { MaSanPham = obj.MaSanPham });
                ViewBag.Product = product;
                ViewBag.IsEdit = true;
                return View(obj);
            }

            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateImage(OImageSach model)
        {
            if (ModelState.IsValid)
            {
                var pro = imgService.Get(new OImageSach { MaAnh = model.MaAnh });
                if (pro != null)
                {
                    try
                    {

                        imgService.Update(model);
                        return RedirectToAction("Images", "QuanLySanPham", new { masp = model.MaSanPham });
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
        public ActionResult DeleteImage(int id, int masp)
        {
            imgService.Delete(id);
            return RedirectToAction("Images", "QuanLySanPham", new { masp = masp });
        }
        [HttpPost]
        public ActionResult AddAuthor(string tacgia)
        {
            try
            {
                var item = tacGiaService.GetByShortName(Help.Helper.convertToUnSign3(tacgia));
                if (item != null)
                {
                    return Json(new { Success = false, Flag = 1, Message = "Tác giả này đã tồn tại!" });
                }
                else
                {
                    tacGiaService.Add(new OTacGia
                    {
                        Ten = tacgia,
                        TenVanTat = Help.Helper.convertToUnSign3(tacgia),
                        TrangThai = true,
                    });
                    return Json(new { Success = true });
                }
            }
            catch (Exception e)
            {

            }
            return Json(new { Success = false, Flag = 0, Error = "Thêm thất bại" });
        }
        #endregion
        #endregion
    }
}