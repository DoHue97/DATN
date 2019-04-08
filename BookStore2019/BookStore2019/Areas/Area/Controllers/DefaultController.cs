using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore2019.Areas.Area.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Area/Default
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}