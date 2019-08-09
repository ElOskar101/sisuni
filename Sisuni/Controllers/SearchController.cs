using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.ViewModels;

namespace Sisuni.Controllers{
    public class SearchController : Controller{
        // GET: Search
        public ActionResult Index(){
            return View();
        }

        [HttpPost]
        public ActionResult Index(string section) {

            if(section == null) {
                return View();
            } else {
                return View();
            }
            
            
        }
    }

    
}