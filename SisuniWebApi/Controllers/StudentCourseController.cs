using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data;
using Data.ViewModels;

namespace SisuniWebApi.Controllers{
    public class StudentCourseController : ApiController {


        [HttpPost] // A little post created by two ids. It appears to be intermediate table. 
        public IHttpActionResult Post([FromBody] CourseStudent cs) {

            Context db = new Context();

            if (ModelState.IsValid) {
                db.CourseStudent.Add(cs);
                db.SaveChanges();
                return Ok(cs);

            } else
                return BadRequest();
        }


        [HttpGet]// Listado de todos los cursos de un usuario (StudenCourse/{id})
        public IEnumerable<CourseStudentCourse> Get(int id) { // We just need it... Do you think we need a Get method without id?

            Context db = new Context(); // We are join too many relationships for get some students and courses. I think I'm coursed.

            var course = from c in db.Courses
                         join t in db.Teacher on c.TeacherID equals t.TeacherID
                         join cs in db.CourseStudent on c.CourseID equals cs.CourseID
                         where id == cs.StudentID
                         select new CourseStudentCourse {
                             CourseID = cs.CourseID,
                             CourseStudentID = cs.CourseStudentID,
                             Name = c.Name,
                             Section = c.Section,
                             Credits = c.Credits,
                             CareerID = c.CareerID,
                             Schedule = c.Schedule,
                             TeacherName = t.FullName
                         };
            return (course);

        }
        [Route("api/StudentCourse/courses/{section}/{careerID}")] // Get the career searched by the student.
        [HttpGet]
        public IEnumerable<CourseStudentCourse> Get(string section, int careerID) { // Filter determinate the courses that student can get

            Context db = new Context();
            var courses = from c in db.Courses join t in db.Teacher on c.TeacherID equals t.TeacherID
                          where c.Section.Equals(section) && c.CareerID.Equals(careerID)
                          select new CourseStudentCourse {
                              CourseID = c.CourseID,
                              Name = c.Name,
                              Section = c.Section,
                              Credits = c.Credits,
                              CareerID = c.CareerID,
                              Schedule = c.Schedule,
                              TeacherName = t.FullName
                          };
            return (courses);
        }

        /* Delete a student's course. I'd create this method for delete and union between Student and Course.
         For that I need two elements wich will find the identification of both column.*/
        [Route("api/StudentCourse/{StudentID}/{CourseID}")]
        [HttpDelete]
        public IHttpActionResult Delete(int StudentID, int CourseID) { // I am not sure if this will work

            using (var db = new Context()) {

                var courseStudent = db.CourseStudent.Where(x => x.CourseID == CourseID && x.StudentID == StudentID).FirstOrDefault();

                if (courseStudent != null) {

                    db.CourseStudent.Remove(courseStudent);
                    db.SaveChanges();

                    return Ok(courseStudent);
                } else {
                    return NotFound();
                }
            }
        }



    }
}
        
      


