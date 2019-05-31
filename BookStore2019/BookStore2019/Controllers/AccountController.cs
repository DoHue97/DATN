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
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace BookStore2019.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        int pageSize = 5;
        AccountService accountService = new AccountService();
        KhachHangService khService = new KhachHangService();
        HDBService hdbService = new HDBService();
        CTHDBService cthdbService = new CTHDBService();
        SanPhamService sachService = new SanPhamService();
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
        public ActionResult Login(Login model,string ReturnUrl)
        {
            
            var account = accountService.Login(model.UserName, model.Password);
            if (account != null)
            {
                
                FormsAuthentication.SetAuthCookie(model.UserName, model.IsRemember);
                
                if (account.MaQuyen == 1)
                {
                    Session["Admin"] = account;
                    if (ReturnUrl == null)
                    {
                        return RedirectToAction("Index", "Default", new { area = "Area" });
                    }
                    return Redirect(ReturnUrl);
                }
                else if(account.MaQuyen==3)
                {
                    Session["Account"] = account;
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
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
                MatKhau=model.Password,
                TenDN=model.UserName,
                MaQuyen=3,
            };
            if (accountService.Register(account))
            {
                Session["Account"] = account;
                //Random rd = new Random();
                //string strRandom;
                //strRandom = rd.Next(1, 1000).ToString();
                //Session["RandomText"] = strRandom;
                //SendMailActive(account.Email, strRandom);
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }
        public ActionResult VerificationEmail(string strRandom)
        {
            if (Session["RandomText"] !=null && Session["Account"]!=null)
            {
                string code = Session["RandomText"].ToString();
                if (code.Equals(strRandom))
                {
                    var account = (OAccount)Session["Account"];
                    accountService.Active(account);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("~/Views/Shared/Error.cshtml");
        }
        public void SendMailActive(string email,string strRandom)
        {
            string emailsend = ConfigurationManager.AppSettings["email"];
            string pass = ConfigurationManager.AppSettings["password"];
            MailAddress mailCompany = new MailAddress("sachhayonline97yp1@gmail.com");
            MailMessage mail = new MailMessage();
            mail.To.Add(email);

            mail.From = new MailAddress(mailCompany.Address, "Sách hay");
            mail.Subject = "Xác thực tài khoản";
            mail.SubjectEncoding = Encoding.GetEncoding("utf-8");
            mail.BodyEncoding = Encoding.GetEncoding("utf-8");

            mail.Bcc.Add(emailsend);
            mail.IsBodyHtml = true;
            mail.Body = "<div><p>Click vào link sau để xác thực tài khoản: </p><br/>";
            mail.Body += "http://localhost:52191/Account/VerificationEmail?strRandom=" + strRandom;
            mail.Body += "</div>";
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            NetworkCredential mailAuthentication = new NetworkCredential();
            mailAuthentication.UserName = emailsend;
            mailAuthentication.Password = pass;

            client.Port = 587;
            client.Host = "smtp.gmail.com";

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = mailAuthentication;

            try
            {
                client.Send(mail);                
            }
            catch (Exception e)
            {
                
            }
        }
        [HttpGet]
        public ActionResult Info(string username, int? page = 1)
        {            
            var model = accountService.GetByUserName(username);
            
            

            int total = 0;
            int endAt = (int)page * pageSize;
            int fromAt = endAt - pageSize;
            var list = hdbService.GetByMaKhach((int)model.MaKhach,fromAt, pageSize, ref total);

            ViewBag.Pager = Pager.Items(total).PerPage(pageSize).Move((int)page).Segment(5).Center();
            ViewBag.ListOrders = list;
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePass(string oldpass, string newpass, string username,int makhach)
        {
            if (accountService.ChangePass(new OAccount { MatKhau = oldpass, TenDN = username,MaKhach=makhach }, newpass))
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