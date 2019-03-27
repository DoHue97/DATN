using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Models;
using ValuesObject;
using BookStore2019.Services;
namespace BookStore2019.Controllers
{
    public class ProductActionController : Controller
    {
        SachService sachService = new SachService();
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
                var product = sachService.Get(new OSach { MaSach = proid });
                if (product.SoLuong >= quanlity)
                {
                    if (objCart != null)
                    {
                        objCart.UpdateCart(proid, quanlity);
                        Session["Cart"] = objCart;
                    }
                    
                    return Json(new { Success = true, Flag = "0", Cart = objCart });
                    
                }
                else
                {                    
                    return Json(new { Success = false, Flag = "1", Message = "Sản phẩm " + product.TenSach + " còn  " + product.SoLuong + " sản phẩm" });
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
                var product = sachService.Get(new OSach { MaSach = proid });
                if (product!=null && product.SoLuong >= quanlity)
                {
                    if (objCart == null)
                    {
                        objCart = new Carts();
                    }                    
                    CartItem cartItem = new CartItem
                    {
                        ProductId = proid,
                        Quantity = quanlity,
                        Price = (decimal)product.GiaBan,
                        ProductImage = product.Anh,
                        ProductName = product.TenSach,
                        ShortName = product.TenVanTat,
                        Total = (decimal)product.GiaBan * quanlity
                    };
                    objCart.AddToCart(cartItem);
                    Session["Cart"] = objCart;
                    return Json(new { Success = true, Flag = "0", Cart = objCart });

                }
                else
                {
                    return Json(new { Success = false, Flag = "1", Message = "Sản phẩm " + product.TenSach + " còn  " + product.SoLuong + " sản phẩm" });
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
    }
}