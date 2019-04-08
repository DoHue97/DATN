using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2019.Services;
using ValuesObject;
namespace BookStore2019.Controllers
{
    public class HtmlController : Controller
    {
        HtmlPageService htmlPageService = new HtmlPageService();
        // GET: Html
        public ActionResult Detail(string shortname)
        {
            var item = htmlPageService.GetByShortName(shortname);

            return View(item);
        }
    }
}