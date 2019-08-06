using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel {
    class CourseTeacher {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public int Credits { get; set; }
        public int CareerID { get; set; }
        public int TeacherID { get; set; }
        public string Schedule { get; set; }
        public string FullName { get; set; }

    }
}
