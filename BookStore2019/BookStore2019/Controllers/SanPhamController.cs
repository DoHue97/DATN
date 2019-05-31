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
    public class SanPhamController : Controller
    {
        int pageSize = 20;
        SanPhamService sachService = new SanPhamService();
        ChuDeService chuDeService = new ChuDeService();
        NhaXuatBanService nxbService = new NhaXuatBanService();
        // GET: Sach

        public ActionResult GetAllBook(int? page = 1)
        {

            int total = 0;
            int endAt = (int)page * pageSize;
            int fromAt = endAt - pageSize;
            var list = sachService.GetAllBook((int)fromAt, pageSize, ref total);

            ViewBag.Pager = Pager.Items(total).PerPage(pageSize).Move((int)page).Segment(5).Center();
            return View(list);
        }
        public ActionResult GetAllDoDung(int? page = 1)
        {

            int total = 0;
            int endAt = (int)page * pageSize;
            int fromAt = endAt - pageSize;
            var list = sachService.GetAllDoDung((int)fromAt, pageSize, ref total);

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
        public ActionResult GetByCate(string shortnamecate, bool isSach, int? page=1)
        {
            
            var category = chuDeService.GetByShortName(shortnamecate);
            
            int total = 0;
            int endAt = (int)page * pageSize;
            int fromAt = endAt - pageSize;
            var list = sachService.GetAllByCate((int)fromAt, pageSize, ref total,category.MaChuDe);
            ViewBag.Category = category;
            ViewBag.Pager = Pager.Items(total).PerPage(pageSize).Move((int)page).Segment(5).Center();
            ViewBag.IsSach = isSach;
            return View(list);
        }
       
        
    }
}