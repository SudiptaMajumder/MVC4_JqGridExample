using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_JqGrid.Models;

namespace MVC_JqGrid.Controllers
{
    public class AppController : Controller
    {
        //
        // GET: /App/
        JQGridDbContext db = new JQGridDbContext();

        public JQGridDbContext DataContext
        {
            get { return db; }
        }

        public AppController()
        {

        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
           
            try
            {

            }
            catch
            {

            }
        }

    }
}
