using BookStore2019.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValuesObject;

namespace BookStore2019.Controllers
{
    public class AccountController : Controller
    {
        AccountService accountService = new AccountService();
        // GET: Account
        //[Authorize]
        public ActionResult Admin()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string usename = f["UserName"].ToString();
            string pass = f["Password"].ToString();
            bool isRemember = Boolean.Parse(f["IsRemember"]);
            var account = accountService.Login(usename, pass);
            if (account != null)
            {
                Session["Account"] = account;
                if (account.MaQuyen == 1)
                {
                    return RedirectToAction("Admin", "Account");
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
        public ActionResult Register(OAccount account)
        {
            if (ModelState.IsValid)
            {
                if (accountService.Register(account))
                {
                    Session["Account"] = account;
                    return View("Index", "Home");
                }
                
            }
            return View(account);
        }
    }
}