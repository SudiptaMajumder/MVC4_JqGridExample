using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_JqGrid.Models;

namespace MVC_JqGrid.Controllers
{
    public class ASL_MENUController : Controller
    {
        //Datetime formet
       
        public DateTime td;

        JQGridDbContext db = new JQGridDbContext();
       

        public ASL_MENUController()
        {
           
        }


        // GET: /ASL_MENU/
        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public ActionResult Index()
        {
            var dt = (PageModel)TempData["data"];
            return View(dt);
        }


        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(PageModel model)
        {
            if (model.aslMenumst.MODULENM == null )
            {
                ViewBag.ModuleNameField = "Please input the Module Name. ";
                return View("Index");
            }


            var result =
                db.AslMenumstDbSet.Count(
                    d => d.MODULENM == model.aslMenumst.MODULENM);
            if (result == 0)
            {
                var id = Convert.ToString(db.AslMenumstDbSet.Max(s => s.MODULEID));

                if (id == null)
                {
                    model.aslMenumst.MODULEID = Convert.ToString("01");

                    db.AslMenumstDbSet.Add(model.aslMenumst);
                    db.SaveChanges();

                    TempData["message"] = "Module Name: '" + model.aslMenumst.MODULENM +
                                          "' successfully saved.\n Please Create the Menu List.";

                    TempData["data"] = model;
                    return RedirectToAction("Index");
                    //return View("index", new { ID = model.aslMenumst.MODULEID });
                }
                else if (id != null)
                {
                    int nid = int.Parse(id) + 1;
                    if (nid < 10)
                    {
                        model.aslMenumst.MODULEID = Convert.ToString("0" + nid);
                        db.AslMenumstDbSet.Add(model.aslMenumst);
                        db.SaveChanges();

                        TempData["message"] = "Module Name: '" + model.aslMenumst.MODULENM +
                                              "' successfully saved.\n Please Create the Menu List.";

                        TempData["data"] = model;
                        return RedirectToAction("Index");

                    }
                    else if (nid < 100)
                    {
                        model.aslMenumst.MODULEID = Convert.ToString(nid);
                        db.AslMenumstDbSet.Add(model.aslMenumst);
                        db.SaveChanges();

                        TempData["message"] = "Module Name: '" + model.aslMenumst.MODULENM +
                                              "' successfully saved.\n Please Create the Menu List.";

                        TempData["data"] = model;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = " Module entry not possible ";
                        return RedirectToAction("Index");
                    }
                }
            }

            else if (result > 0)
            {
                var getModuleID = from m in db.AslMenumstDbSet
                                  where m.MODULENM == model.aslMenumst.MODULENM
                                  select new { m.MODULEID };
                foreach (var VARIABLE in getModuleID)
                {
                    model.aslMenumst.MODULEID = VARIABLE.MODULEID;
                }

                if (model.aslMenumst.MODULENM != null)
                {
                    TempData["message"] = "Get the Menu List";
                    TempData["data"] = model;
                    return RedirectToAction("Index");
                }

            }

            return View("Index");
        }



        public JsonResult GetTodoLists(string ModId, string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            ASL_MENU aslMenuObj = new ASL_MENU();
            aslMenuObj.MODULEID = ModId;

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var todoListsResults = db.AslMenuDbSet
                .Where(a => a.MODULEID == ModId )
                .Select(
                    a => new
                    {
                        a.Id,
                        a.MENUID,
                        a.MENUNM,
                      
                    });

            int totalRecords = todoListsResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                todoListsResults = todoListsResults.OrderByDescending(s => s.MENUID);
                todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                todoListsResults = todoListsResults.OrderBy(s => s.MENUID);
                todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = todoListsResults
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }





        //insert a new row to the grid logic here 

        [HttpPost]
        public string Create(string ModId, [Bind(Exclude = "Id")] ASL_MENU objTodo)
        {
            objTodo.MODULEID = ModId;

            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var maxData = Convert.ToString((from n in db.AslMenuDbSet where n.MODULEID == ModId  select n.MENUID).Max());
                    if (maxData == null )
                    {

                        objTodo.MENUID = Convert.ToString("P" + ModId + "01");

                        db.AslMenuDbSet.Add(objTodo);
                        db.SaveChanges();

                        msg = "Saved Successfully";
                    }
                    else if (maxData != null )
                    {
                        var subString = Convert.ToString((from n in db.AslMenuDbSet where n.MODULEID == ModId  select n.MENUID.Substring(3, 2)).Max());

                        string id = Convert.ToString(subString);
                        int nid = int.Parse(id) + 1;

                        if (nid < 10)
                        {
                            objTodo.MENUID = Convert.ToString("p" + ModId + "0" + nid);
                            db.AslMenuDbSet.Add(objTodo);
                            db.SaveChanges();
                            msg = "Saved Successfully";

                        }
                        else if (nid < 100)
                        {
                            objTodo.MENUID = Convert.ToString("F" + ModId + nid);
                            db.AslMenuDbSet.Add(objTodo);
                            db.SaveChanges();
                            msg = "Saved Successfully";

                        }
                        else
                        {
                            msg = "Module entry not possible";
                        }
                    }                    

                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }




        public string Edit(string ModId, string MenuType, ASL_MENU objTodo)
        {
            string msg;
            try
            {
                var query =
                        from a in db.AslMenuDbSet
                        where (a.MENUID == objTodo.MENUID)
                        select a;

                foreach (ASL_MENU a in query)
                {
                    // Insert any additional changes to column values.
                    a.MENUNM = objTodo.MENUNM;
                }

                db.SaveChanges();
                msg = "" + objTodo.MENUNM + "update Successfully.";
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        //Delete Menu From Grid
        public string Delete(int Id)
        {
            ASL_MENU todolist = db.AslMenuDbSet.Find(Id);

            db.AslMenuDbSet.Remove(todolist);
            db.SaveChanges();
            return "Deleted successfully";
        }


        //
        // GET: /ASL_MENUMST/
        public ActionResult ShowModuleList()
        {
            return View(db.AslMenumstDbSet.ToList());
        }


        //
        // GET: /ASL_MENUMST/Edit/5

        public ActionResult EditModuleList(string id = null)
        {
            ASL_MENUMST asl_menumst = db.AslMenumstDbSet.Find(id);
            if (asl_menumst == null)
            {
                return HttpNotFound();
            }
            return View(asl_menumst);
        }

        //
        // POST: /ASL_MENUMST/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditModuleList(ASL_MENUMST asl_menumst)
        {
            if (ModelState.IsValid)
            {

                db.Entry(asl_menumst).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ModuleUpdateMessage"] = "Module name: '" + asl_menumst.MODULENM + "' update Successfully!";
                return RedirectToAction("ShowModuleList");
            }
            return View(asl_menumst);
        }

        // Get All menu Information
        public ActionResult ShowMenuList()
        {
            return View(db.AslMenuDbSet.ToList());
        }


        //
        // GET: /ASL_MENU/Delete/5

        public ActionResult DeleteModule(string id = null)
        {
            ASL_MENUMST asl_menumst = db.AslMenumstDbSet.Find(id);
            if (asl_menumst == null)
            {
                return HttpNotFound();
            }
            return View(asl_menumst);
        }

        //
        // POST: /ASL_MENU/Delete/5

        [HttpPost, ActionName("DeleteModule")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteModuleConfirmed(string id)
        {
            ASL_MENUMST asl_menumst = db.AslMenumstDbSet.Find(id);

            //Seasrch all information from Menu Table,when it match to the Module ID
            var menuList = (from sub in db.AslMenuDbSet select sub)
               .Where(sub => sub.MODULEID == asl_menumst.MODULEID);
            foreach (var n in menuList)
            {
                db.AslMenuDbSet.Remove(n);
            }
            db.SaveChanges();

            db.AslMenumstDbSet.Remove(asl_menumst);
            db.SaveChanges();
            TempData["ModuleDeleteMessage"] = "Module name: '" + asl_menumst.MODULENM + "' delete Successfully!";
            return RedirectToAction("ShowModuleList");
        }


        //AutoComplete 
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ItemNameChanged(string changedText)
        {
            // var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            string itemId = "";
            var rt = db.AslMenumstDbSet.Where(n => n.MODULENM == changedText).Select(n => new
            {
                moduleId = n.MODULEID
            });

            foreach (var n in rt)
            {
                itemId = n.moduleId;
            }

            return Json(itemId, JsonRequestBehavior.AllowGet);

        }


        //AutoComplete
        public JsonResult TagSearch(string term)
        {
            //var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            var tags = from p in db.AslMenumstDbSet
                       select p.MODULENM;

            return this.Json(tags.Where(t => t.StartsWith(term)),
                            JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}