using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_JqGrid.Models
{
    public class ASL_MENUMST
    {

        [Key]
        public string MODULEID { get; set; }

        [Required(ErrorMessage = "Module Name can not be empty!")]
        [Display(Name = "Module Name")]
        public string MODULENM { get; set; }

  
                 
    }
}