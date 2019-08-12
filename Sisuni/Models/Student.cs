using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sisuni.Models {
    public partial class Student {

        [Display(Name = "ID"), Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(20)]
        public string Identification { get; set; }


        [Display(Name = "Contraseña"), Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Password { get; set; }
    }
}