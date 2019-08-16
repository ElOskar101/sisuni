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
                 filterContext.Controller is HomeController == false &&
                 filterContext.Controller is NotFoundController == false &&
                        filterContext.Controller is ServerErrorController == false)
                    filterContext.HttpContext.Response.Redirect("~/login/index", false);
            } else {

                if (filterContext.Controller is LoginController == true)
                    filterContext.HttpContext.Response.Redirect("~/dash/index", false);
            }

            base.OnActionExecuting(filterContext);
        } 
    }
}