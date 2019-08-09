using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sisuni.Models {
    public class User {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Password { get; set; }
        public int CareerID { get; set; }
        public string Token { get; set; }
    }
}