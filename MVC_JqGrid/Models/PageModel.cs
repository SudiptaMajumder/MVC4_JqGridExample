using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;

namespace MVC_JqGrid.Models
{
    public class PageModel
    {

        public PageModel()
        {
            this.aslMenumst = new ASL_MENUMST();
            this.aslMenu = new ASL_MENU();
        }

        public ASL_MENUMST aslMenumst { get; set; }
        public ASL_MENU aslMenu { get; set; }
        



        //[Display(Name = "HeadType")]
        //public string HeadType { get; set; }


        //[Required(ErrorMessage = "From date field can not be empty!")]
        //[DataType(DataType.Date)]
        //public DateTime? FromDate { get; set; }

        //[Required(ErrorMessage = "To Date field can not be empty!")]
        //[DataType(DataType.Date)]
        //public DateTime? ToDate { get; set; }   

    }
}