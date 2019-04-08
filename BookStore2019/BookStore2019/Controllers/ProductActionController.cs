using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Models;
using ValuesObject;
using BookStore2019.Services;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace BookStore2019.Controllers
{
    public class ProductActionController : Controller
    {
        SanPhamService sachService = new SanPhamService();
        HDBService hdbService = new HDBService();
        CTHDBService cTHDBService = new CTHDBService();
        AccountService accountService = new AccountService();
        // GET: ProductAction

        public ActionResult Order()
        {
            ShoppingCartModels model = new ShoppingCartModels();
            model.Cart = (Carts)Session["Cart"];
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateCart(int proid, int quanlity)
        {
            try
            {
                Carts objCart = (Carts)Session["Cart"];
                var product = sachService.Get(new OSanPham { MaSanPham = proid });
                if (product.SoLuong >= quanlity)
                {
                    if(product.Sale>0)
                    {
                        product.GiaBan = product.GiaBan - product.GiaBan * product.Sale / 100;
                    }
                    if (objCart != null)
                    {
                        objCart.UpdateCart(proid, quanlity);
                        Session["Cart"] = objCart;
                    }

                    return Json(new { Success = true, Flag = "0", Cart = objCart });

                }
                else
                {
                    return Json(new { Success = false, Flag = "1", Message = "Sản phẩm " + product.TenSanPham + " còn  " + product.SoLuong + " sản phẩm" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.StackTrace.ToString() });
            }


        }
        [HttpPost]
        public ActionResult AddToCart(int proid, int quanlity)
        {
            try
            {
                Carts objCart = (Carts)Session["Cart"];
                var product = sachService.Get(new OSanPham { MaSanPham = proid });
                if (product != null && product.SoLuong >= quanlity)
                {
                    
                    if (objCart == null)
                    {
                        objCart = new Carts();
                    }
                    if (product.Sale > 0)
                    {
                        product.GiaBan = product.GiaBan - product.GiaBan * product.Sale / 100;
                    }
                    CartItem cartItem = new CartItem
                    {
                        ProductId = proid,
                        Quantity = quanlity,
                        Price = (decimal)product.GiaBan,
                        ProductImage = product.Anh,
                        ProductName = product.TenSanPham,
                        ShortName = product.TenVanTat,
                        Total = (decimal)product.GiaBan * quanlity
                    };
                    objCart.AddToCart(cartItem);
                    Session["Cart"] = objCart;
                    return Json(new { Success = true, Flag = "0", Cart = objCart });

                }
                else
                {
                    return Json(new { Success = false, Flag = "1", Message = "Sản phẩm " + product.TenSanPham + " còn  " + product.SoLuong + " sản phẩm" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.StackTrace.ToString() });
            }


        }
        public ActionResult Remove(int id)
        {
            Carts objCart = (Carts)Session["Cart"];
            if (objCart != null)
            {
                objCart.RemoveFromCart(id);
                Session["Cart"] = objCart;
            }
            return RedirectToAction("Order", "ProductAction");
        }
        //Đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            string strChuoi = "";
            decimal total = 0;
            
            ShoppingCartModels model = new ShoppingCartModels();
            model.Cart = (Carts)Session["Cart"];
            
            if (Session["Account"] == null || Session["Account"].ToString() == "")
            {
                //return RedirectToAction("Login", "Account");
                return Json(new { Success = false, Url=Url.Action("Login","Account")});
            }
           
            OAccount account = (OAccount)Session["Account"];
            OAccount kh = accountService.Get(account.UserName, account.PassWord);
            OHoaDonBan ddh = new OHoaDonBan
            {
                MaKhach =kh.MaKhach,
                TrangThai=false,
            };
            hdbService.Add(ddh);
            int idHdb = hdbService.GetLastId();
            foreach (var item in model.Cart.ListItem)
            {
                cTHDBService.Add(new OCTHDB {
                    MaHDB=idHdb,
                    MaSanPham=item.ProductId,
                    SoLuong=item.Quantity,
                    ThanhTien=item.Total,

                });

                strChuoi += "<li> Tên sản phẩm: " + item.ProductName + "</li> ";
                strChuoi += "<li> Giá: " + item.Price.ToString("#,##") + " đ</li> ";
                strChuoi += "<li> Số lượng: " + item.Quantity + "</li> ";
                total += item.Total;
            }
            strChuoi += "<li>Tổng tiền: " + total.ToString("#,##") + " đ </li>";
            //SendEmail(kh.Email,strChuoi);
            Session["Cart"] = null;
            
            return Json(new { Success = true,Message= "Đặt hàng thành công. Bạn hãy check email nhé !" });
        }

        [HttpPost]
        public bool SendEmail(string email, string strChuoi)
        {
            string emailsend = ConfigurationManager.AppSettings["email"];
            string pass = ConfigurationManager.AppSettings["password"];
            MailAddress mailCompany = new MailAddress("sachhayonline97yp1@gmail.com");
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.To.Add(mailCompany);
            mail.From = new MailAddress(emailsend, "Book store online");
            mail.Subject = "Thông tin đơn hàng đã đặt";
            mail.SubjectEncoding = Encoding.GetEncoding("utf-8");
            mail.BodyEncoding = Encoding.GetEncoding("utf-8");

            mail.IsBodyHtml = true;
            mail.Body = strChuoi;
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
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}