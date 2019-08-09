using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data;
using Data.ViewModel;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using SisuniWebApi.Models;

namespace SisuniWebApi.Controllers{
    //OK here we go
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController{

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing() {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser() {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate([FromBody]Student student) {
            Context db = new Context();
            Student found = new Student();
            User user = new User();

            if (student == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

             found = db.Student.Where(s => s.Identification == student.Identification
                                            && s.Password == student.Password).FirstOrDefault();
           
            if (found != null) {
                var token = TokenGenerator.GenerateTokenJwt(student.Identification);
                user.StudentID = found.StudentID;
                user.Name = found.Name;
                user.Password = found.Password;
                user.Token = token;
                user.CareerID = found.CareerID;
                user.Identification = found.Identification;
                user.LastName = found.LastName;
                return Ok(user);
            } else 
                return Unauthorized();
        }
    }
}
