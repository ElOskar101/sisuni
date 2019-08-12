using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using System.Collections.Specialized;
using Newtonsoft.Json;
using Sisuni.Models;

namespace Sisuni.Controllers {
    
    public class SearchController : Controller {
        string baseURL = "http://localhost:3030/api/";
        IEnumerable<CourseStudentCourse> csc = new List<CourseStudentCourse>();
        IEnumerable<FoundResult> cs = new List<FoundResult>();

        // GET: Search
        public ActionResult Index() { // Calling index as normal method when is starting the view
            ViewBag.Type = 1;
            setDataAlert();

            return View(csc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(string section) { // Este método se ejecuta al buscar un curso

           await CheckCourse(section); // Llamamos a este método para verificar primero que no venga null la sección y para saber si ya inscibió ese curso
          //  int NullSearch = Int32.Parse(TempData["NullSearch"].ToString()); // Si este trae uno, significa que vino nulo

            //if(NullSearch == 1) TempData["Full"] = 99; else TempData["Full"] = 2; // Si viene nula la cadena, no significa que tenga todos los cursos inscritos

            if (cs != null) { // Verificamos que no venga nulo el modelo de course - student
                if (cs.Count() == 0) { // Verificamos que si no hay registros y si la cadena no vino nula.

                    string careerID = Session["CareerID"].ToString();

                    IEnumerable<CourseStudentCourse> csc = new List<CourseStudentCourse>();
                    string resource = "StudentCourse/courses/" + section + "/" + careerID;

                    using (var client = new HttpClient()) {

                        client.BaseAddress = new Uri(baseURL);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)(Session["Token"]));


                        HttpResponseMessage res = await client.GetAsync(resource);

                        if (res.IsSuccessStatusCode) {
                            var response = res.Content.ReadAsStringAsync().Result;
                            csc = JsonConvert.DeserializeObject<List<CourseStudentCourse>>(response);
                            try {

                                if (csc.Count() >= 1) { // Si trajo registros, entonces le decimos que el tipo será uno para indicar que lo encontró
                                    ViewBag.type = 1;
                                    return View(csc);
                                } else {// El tipo cero indica que no encontró el resultado
                                    ViewBag.type = 0;
                                    return View(csc);
                                }
                            } catch (Exception e) {
                                TempData["NullSearch"] = 1;
                                return RedirectToAction("Index", "Search");
                            }

                        } else
                            return View();
                    }
                } else {// Redireccionamos el index con el error de que debe establecer la sección del curso
                    TempData["Full"] = 2;
                    TempData["NullSearch"] = 99;
                    return RedirectToAction("Index", "Search");
                }
            } else {// Redirecionamos que no encontró nada
                ViewBag.type = 0;
                return View();
            }
        }

        public async Task CheckCourse(string section) { // Sirve para verificar si el estudiante ya tiene inscrita esa clase

            if (!section.Equals("")) { // VAlidamos que la cadena no venga vacía

                string studentID = Session["StudentID"].ToString();
                string resource = "StudentCourse/" + section + "/" + studentID;

                using (var client = new HttpClient()) {

                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)(Session["Token"]));

                    HttpResponseMessage res = await client.GetAsync(resource);

                    if (res.IsSuccessStatusCode) {
                        var response = res.Content.ReadAsStringAsync().Result;
                        cs = JsonConvert.DeserializeObject<List<FoundResult>>(response);
                    }
                }
            } else {
                TempData["NullSearch"] = 1; 
            }
        }



        public async Task<ActionResult> Enroll(int id) {
            //await getCount();
            //if (csc.Count() < 5) {
                int studentID = Int32.Parse(Session["StudentID"].ToString());
                CourseStudent studentCourse = new CourseStudent();
                studentCourse.StudentID = studentID;
                studentCourse.CourseID = id;

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)(Session["Token"]));

                    var response = await client.PostAsJsonAsync<CourseStudent>("StudentCourse", studentCourse);

                    if (response.IsSuccessStatusCode) {

                        TempData["Enroll"] = 1;
                        return RedirectToAction("Index", "Search");
                    } else {
                        TempData["Enroll"] = 99;
                        return RedirectToAction("Index", "Search");
                    }
                }
           //} else {
             //   TempData["Full"] = 1; 
               // return RedirectToAction("Index", "Search");
            //}
        }

        /* To validate the number of courses;
        public async Task getCount() {
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
            ViewBag.Enroll = TempData["Enroll"];
            ViewBag.Full = TempData["Full"];
            ViewBag.NullSearch = TempData["NullSearch"];
        }

        public void ola() {

        }
        

    }
}


    
