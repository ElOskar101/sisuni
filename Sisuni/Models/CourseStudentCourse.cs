using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sisuni.Models {
    public class CourseStudentCourse {
        
        public int CourseStudentID { get; set; }
        public int CourseID { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public int Credits { get; set; }
        public int CareerID { get; set; }

        public string TeacherName { get; set; }
        public string Schedule { get; set; }
    }
}