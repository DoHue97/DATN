using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Services;
using ValuesObject;
using BookStore2019.Models;

namespace BookStore2019.Areas.Area.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        AccountService accountService = new AccountService();
        SanPhamService spService = new SanPhamService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Info(string matk)
        {
            var account = accountService.GetById(matk);
            return View(account);
        }
        [HttpPost]
        public ActionResult ChangePass(string oldpass, string newpass, string username, int makhach)
        {
            if (accountService.ChangePass(new OAccount { MatKhau = oldpass, TenDN = username, MaKhach = makhach }, newpass))
            {
                return Json(new { Success = true, Message = "Đổi mật khẩu thành công !" });
            }
            return Json(new { Success = false, Message = "Đổi mật khẩu không thành công !" });
        }
        [HttpPost]
        public ActionResult Update(string matk, string username,string email)
        {
            var account = new OAccount {
                TenDN = username,
                Email = email,
                MaTK = Guid.Parse(matk)
            };
            if (accountService.Update(account))
            {
                if (Session["Admin"] != null)
                {
                    Session["Admin"] = account;
                }
                return Json(new { Success = true, Message = "Sửa thông tin thành công!",Model=account });
            }
            return Json(new { Success = false, Message = "Sửa thông tin thất bại!" });
        }
        public ActionResult DoanhThu(int year)
        {
            var list = spService.GetByYear(year);
            return Json(new { Success = true, ListDoanhThus = list });
        }
    }
}