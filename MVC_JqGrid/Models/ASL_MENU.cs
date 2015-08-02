using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_JqGrid.Models
{
    public class ASL_MENU
    {
        [Key]
        public Int64 Id { get; set; }
        public string MODULEID { get; set; }

        [Display(Name = "Menu Type")]
        public string MENUID { get; set; }

        [Display(Name = "Menu Name")]
        public string MENUNM { get; set; }




    }
}