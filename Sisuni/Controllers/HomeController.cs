using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sisuni.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(){
            return View();
        }
    }
}