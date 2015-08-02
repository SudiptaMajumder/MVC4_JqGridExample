using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace MVC_JqGrid.Models
{
    public class SamplaData : DropCreateDatabaseIfModelChanges<JQGridDbContext>
    {
        protected override void Seed(JQGridDbContext context)
        {
            context.AslMenumstDbSet.Add(new ASL_MENUMST() { MODULEID = "01", MODULENM = "User Module" });
            context.AslMenumstDbSet.Add(new ASL_MENUMST() { MODULEID = "02", MODULENM = "Empty Module" });


            context.AslMenuDbSet.Add(new ASL_MENU() { MODULEID = "01", MENUID = "P0101", MENUNM = "User Information" });
            //context.AslMenuDbSet.Add(new ASL_MENU() { MODULEID = "02", MENUTP = "F", MENUID = "F0201", MENUNM = "Head Creation", ACTIONNAME = "Index", CONTROLLERNAME = "AccountHead" });
            context.AslMenuDbSet.Add(new ASL_MENU() { MODULEID = "01", MENUID = "P0102", MENUNM = "User Log Data List"});
           // context.AslMenuDbSet.Add(new ASL_MENU() { MODULEID = "02", MENUTP = "R", MENUID = "R0201", MENUNM = "Listed Items", ACTIONNAME = "GetCategoryReport", CONTROLLERNAME = "BillingReport" });


           
            context.SaveChanges();
            //base.Seed(context);
        }
    }
}