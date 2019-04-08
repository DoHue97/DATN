using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;
using BookStore2019.Services;
namespace BookStore2019.Areas.Area.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        AccountService accountService = new AccountService();
        // GET: Area/QuanLyNguoiDung
        public ActionResult Search()
        {
            var list = accountService.GetAll();
            return View(list);
        }
        
    }
}