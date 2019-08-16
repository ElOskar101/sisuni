using Newtonsoft.Json;
using Sisuni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sisuni.Controllers{
    public class DashController : Controller{
        string baseURL = "http://localhost:3030/api/";
        IEnumerable<CourseStudentCourse> csc = new List<CourseStudentCourse>();

        // GET: Dash
        public async Task<ActionResult> Index(){
            setDataAlert();

            string studentID = Session["StudentID"].ToString();

            IEnumerable<CourseStudentCourse> csc = new List<CourseStudentCourse>();
            string resourse = "StudentCourse/" +  studentID;

            using (var client = new HttpClient()) {

                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)(Session["Token"]));

                HttpResponseMessage res = await client.GetAsync(resourse);
                if (res.ReasonPhrase.Equals("Internal Server Error")) {
                    return RedirectToAction("Index", "ServerError");
                }

                if (res.IsSuccessStatusCode) {
                    var response = res.Content.ReadAsStringAsync().Result;
                    csc = JsonConvert.DeserializeObject<List<CourseStudentCourse>>(response);

                    if (csc.Count() >= 1) {
                        TempData["Full"] = 99;
                        if (csc.Count() >= 5) {
                            TempData["Full"] = 1;
                            setDataAlert();
                        }
                        
                        return View(csc);
                    } else {
                        return View();
                    }

                } else
                    return View();
            }
        }

        public async Task<ActionResult> Unroll(int id) {

            string StudentID = Session["StudentID"].ToString();
            string resource = "StudentCourse/" + StudentID + "/" + id;
            IEnumerable<CourseStudentCourse> csc = new List<CourseStudentCourse>();

            using (var client = new HttpClient()) {

                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)(Session["Token"]));

                HttpResponseMessage res = await client.DeleteAsync(resource);
                if (res.ReasonPhrase.Equals("Internal Server Error")) {
                    return RedirectToAction("Index", "ServerError");
                }

                if (res.IsSuccessStatusCode) {
                    TempData["Full"] = 99;
                    //var response = res.Content.ReadAsStringAsync().Result;
                    //csc = JsonConvert.DeserializeObject<List<CourseStudentCourse>>(response);
                    TempData["Deleted"] = 1;
                    return RedirectToAction("Index", "Dash");
                    //if (csc.Count() >= 1) { return View(csc); } else { return View(); }

                } else {
                    TempData["Delete"] = 99;
                    return RedirectToAction("Index", "Dash");
                }
            }
        }

        /*   public async Task getCount() {
               string studentID = Session["StudentID"].ToString();

               string resourse = "StudentCourse/" + studentID;

               using (var client = new HttpClient()) {

                   client.BaseAddress = new Uri(baseURL);
                   client.DefaultRequestHeaders.Clear();
                   client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                   client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)(Session["Token"]));

                   HttpResponseMessage res = await client.GetAsync(resourse);

                   if (res.IsSuccessStatusCode) {
                       var response = res.Content.ReadAsStringAsync().Result;
                       csc = JsonConvert.DeserializeObject<List<CourseStudentCourse>>(response);
                   }
               }
           }*/

        public void setDataAlert() {
            ViewBag.Full = TempData["Full"];
            ViewBag.Deleted = TempData["Deleted"];
        }


        public ActionResult Logout() {

            Session["Token"] = null;
            return RedirectToAction("Index", "Home");
        }

       
    }
}