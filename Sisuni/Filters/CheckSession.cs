using Sisuni.Controllers;
using Sisuni.Models;
using System.Web;
using System.Web.Mvc;

namespace Sisuni.Filters {
    public class CheckSession : ActionFilterAttribute{

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            var user = (string)HttpContext.Current.Session["Token"];

            if(user == null) {

                if (filterContext.Controller is LoginController == false &&
                 filterContext.Controller is HomeController == false)
                    filterContext.HttpContext.Response.Redirect("~/login/login");
            } else {

                if (filterContext.Controller is LoginController == true)
                    filterContext.HttpContext.Response.Redirect("~/home/index");
            }

            base.OnActionExecuting(filterContext);
        } 
    }
}