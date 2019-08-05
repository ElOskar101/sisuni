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

        public ActionResult Search(string section) {
            /*using (var db = new Context()) {

                var list = db.CourseStudent.ToList();
                return View(list);
            }*/
            return View();
        }
    }

    
}