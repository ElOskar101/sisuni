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

        // Listado de todos los cursos de un usuario (StudenCourse/{id})
        [HttpGet]
        public IEnumerable<CourseStudentCourse> Get (int id) {

            using (var db = new Context()) {

                var query = from cs in db.CourseStudent
                            join c in db.Courses on cs.StudentID equals id where c.CourseID == cs.CourseID
                            select new CourseStudentCourse {
                                CSCourseID = cs.CourseID,
                                CSStudentID = cs.StudentID,
                                CourseStudentID = cs.CourseStudentID,
                                CourseID = cs.CourseID,
                                Name = c.Name,
                                Section = c.Section,
                                Credits = c.Credits,
                                CareerID = c.CareerID,
                                TeacherID = c.TeacherID,
                                Schedule = c.Schedule
                            };

                return query.ToList();
            }
        }

        // Delete a student's course
        [HttpDelete]
        public IHttpActionResult Delete(int CourseID, int StudentID) {

            using (var db = new Context()) {

                var courseStudent = db.CourseStudent.Where(x => x.CourseID == CourseID && x.StudentID == StudentID).FirstOrDefault();

                if(courseStudent != null) {

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
        
      


