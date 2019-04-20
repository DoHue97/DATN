using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore2019.Areas.Area.Controllers
{
    //[Authorize]
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}