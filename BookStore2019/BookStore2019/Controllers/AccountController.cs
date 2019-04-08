using BookStore2019.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;
using System.Security;
using System.Web.Security;
using BookStore2019.Models;

namespace BookStore2019.Controllers
{
    public class AccountController : Controller
    {
        AccountService accountService = new AccountService();
        KhachHangService khService = new KhachHangService();
        // GET: Account
        public ActionResult Shipping()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model)
        {
            //string usename = f["UserName"].ToString();
            //string pass = f["Password"].ToString();
            //bool isRemember = Boolean.Parse(f["IsRemember"].ToString());

            var account = accountService.Login(model.UserName, model.Password);
            if (account != null)
            {
                Session["Account"] = account;
                //FormsAuthentication.SetAuthCookie(model.UserName, model.IsRemember);
                
                if (account.MaQuyen == 1)
                {
                    return RedirectToAction("Index", "Default",new { area="Area"});
                }
                else if(account.MaQuyen==3)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        public ActionResult LogOff()
        {
            Session["Account"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Register model)
        {
            
            OKhachHang khachHang = new OKhachHang
            {
                TenKhach=model.FullName,
                DienThoai=model.Phone,
                Email=model.Email,
            };
            khService.Add(khachHang);
            int idKhach = khService.GetLastId();
            OAccount account = new OAccount {
                Email=khachHang.Email,
                MaKhach=idKhach,
                PassWord=model.Password,
                UserName=model.UserName,
                MaQuyen=3,
            };
            if (ModelState.IsValid)
            {
                if (accountService.Register(account))
                {
                    Session["Account"] = account;
                    return View("Index", "Home");
                }
                
            }
            return View("Login");
        }
        [HttpGet]
        public ActionResult Info(string username)
        {
            
            var model = accountService.GetByUserName(username);
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePass(string oldpass, string newpass, string username,int makhach)
        {
            if (accountService.ChangePass(new OAccount { PassWord = oldpass, UserName = username,MaKhach=makhach }, newpass))
            {
                return Json(new { Success = true, Message = "Đổi mật khẩu thành công !" });
            }
            return Json(new { Success = false, Message = "Đổi mật khẩu không thành công !" });
        }
        [HttpPost]
        public ActionResult Update(int makhach, string fullname, string address, string phone, string email)
        {
            if (khService.Update(new OKhachHang
            {
                DiaChi = address,
                TenKhach = fullname,
                DienThoai = phone,
                Email = email,
                MaKhach = makhach
            }))
            {
                return Json(new { Success = true, Message = "Sửa thông tin thành công!" });
            }
            return Json(new { Success = false, Message = "Sửa thông tin thất bại!" });
        }
    }
}