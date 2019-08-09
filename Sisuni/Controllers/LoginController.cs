using Sisuni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sisuni.Controllers
{
    public class LoginController :  Controller {
        // GET: Login

        /*public ActionResult Index(Student student) {
            return View();
        }*/


        public ActionResult Login() {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Student student){


            string baseURL = "http://localhost:3030/api/";
            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync<Student>("login/authenticate", student);
                
                User accesToken = await response.Content.ReadAsAsync<User>();

                
                if (response.IsSuccessStatusCode) {
                    Session["Student"] = accesToken;

                    Session["Token"] = accesToken.Token;
                    return RedirectToAction("Index", "Dash");
                } else {
                    return View();
                }
            } 
          }
    }
}