using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Services;
using BookStore2019.Help;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Net;
using BookStore2019.Models;

namespace BookStore2019.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        SanPhamService sachService = new SanPhamService();
        SlideService slideService = new SlideService();
     
        public ActionResult Index()
        {
            int total = 0;
            var listHot = sachService.GetHot(true);
            ViewBag.ListHot = listHot;
            var listSlide = slideService.GetAllActive();
            ViewBag.ListSlide = listSlide;
            var listHotByTag = sachService.GetHotByTag("tieu-thuyet");
            ViewBag.ListHotByTag = listHotByTag;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //[HttpGet]
        public ActionResult Search(string key, int? page = 1)
        {
           if(key!=null && key != "")
            {
                key = Helper.convertLower(key);
                int pageSize = 20;
                int total = 0;
                int endAt = (int)page * pageSize;
                int fromAt = endAt - pageSize;

                var list = sachService.Search((int)fromAt, pageSize, ref total, key);
                ViewBag.Pager = Pager.Items(total).PerPage(pageSize).Move((int)page).Segment(5).Center();
                ViewBag.Key = key;
                ViewBag.ToTals = total;
                return View(list);
            }
           return RedirectToAction("GetAll","Sach");
        }
        [HttpPost]
        public ActionResult SendMail(string email)
        {
            string emailsend = ConfigurationManager.AppSettings["email"];
            string pass = ConfigurationManager.AppSettings["password"];
            MailAddress mailCompany = new MailAddress("sachhayonline97yp1@gmail.com");
            MailMessage mail = new MailMessage();
            mail.To.Add(mailCompany);

            mail.From = new MailAddress(email, "Sách hay");
            mail.Subject = "Nhận tin từ sách hay online";
            mail.SubjectEncoding = Encoding.GetEncoding("utf-8");
            mail.BodyEncoding = Encoding.GetEncoding("utf-8");

            mail.Bcc.Add(emailsend);
            mail.IsBodyHtml = true;
            mail.Body = "<div><p>Email: " + email + " đăng ký nhận tin !</p> <br></div>";
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
                
                return Json(new { Success = true });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Error = "Lỗi không gửi được mail!" });
            }
        }
        
    }
}