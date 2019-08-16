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

        public ActionResult Index() {
            ViewBag.BadPassword = 0;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Student student) {

            if (ModelState.IsValid) {
                string baseURL = "http://localhost:3030/api/";
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsJsonAsync<Student>("login/authenticate/", student);
                    if(response.ReasonPhrase.Equals("Internal Server Error")) {
                        return RedirectToAction("Index", "ServerError");
                    }
                    if(response.IsSuccessStatusCode) {
                        User accesToken = await response.Content.ReadAsAsync<User>();

                        Session["Name"] = accesToken.Name;
                        Session["LastName"] = accesToken.LastName;
                        Session["Identification"] = accesToken.Identification;
                        Session["Password"] = accesToken.Password;
                        Session["CareerID"] = accesToken.CareerID;
                        Session["StudentID"] = accesToken.StudentID;
                        Session["Token"] = accesToken.Token;

                        return RedirectToAction("Index", "Dash");
                    } else {
                        if (response.ReasonPhrase.Equals("Unauthorized"))
                            ViewBag.BadPassword = 1;
                        return View();
                    }
                }
            } else {
                ModelState.AddModelError("Identification", "Email not found or matched");
                return View(student);
            }
        }
    }
}