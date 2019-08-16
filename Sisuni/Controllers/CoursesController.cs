using Newtonsoft.Json;
using Sisuni.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;

namespace Sisuni.Controllerr{
    public class CoursesController : Controller{
        IEnumerable<CourseStudentCourse> cs = new List<CourseStudentCourse>();
        

        string baseURL = "http://localhost:3030/api/";

        public async Task<ActionResult> Index() {
            ViewBag.Full = TempData["Full"];
            if(ViewBag.Full != 1) {
                return RedirectToAction("Index", "Dash");
            } else {
                await GetCourses();
                return View(cs);
            }
            
        }

        /*[HttpPost]
        public ActionResult Index (int id ) {

            /*HtmlToPdfConverter converter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();
            settings.WebKitPath = @"/QtBinaries/";
            converter.ConverterSettings = settings;
            PdfDocument document = converter.Convert("~/Courses/Index");

            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            document.Close();
            ms.Position = 0;

            FileStreamResult result = new FileStreamResult(ms, "application/pdf");
            result.FileDownloadName = "Horario de clases";
            

            //Initialize the HTML to PDF converter 
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.WebKit);

            WebKitConverterSettings settings = new WebKitConverterSettings();

            //Set WebKit path
            settings.WebKitPath = @"/QtBinaries/";

            //Assign WebKit settings to HTML converter
            htmlConverter.ConverterSettings = settings;

            //Convert URL to PDF
            PdfDocument document = htmlConverter.Convert("https://www.google.com");

            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            document.Close();
            ms.Position = 0;

            FileStreamResult result = new FileStreamResult(ms, "application/pdf");
            result.FileDownloadName = "Horario de clases.pdf";

            

            document.Close(true);
            return result;
            
        }*/

        public async Task GetCourses() {
           

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
                    cs = JsonConvert.DeserializeObject<List<CourseStudentCourse>>(response);
                   // RedirectToAction("Index", "Courses", new { @csc = cs });
                } 
            }

        }
    }

}