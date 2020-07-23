using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TST_DataMgmt.Models;

namespace TST_DataMgmt.Controllers
{
    public class TST_OPTIONSController : Controller
    {
        private TST_Options_Select db = new TST_Options_Select();

        public ActionResult Index()
        {
            return View(db.TST_OPTIONS.ToList());
        }

        public JsonResult GetOptions(string OptionType)
        {
            var context = new TST_Options_Select();

            var query = from p in context.TST_OPTIONS
                        select p;

            var dbResult = query.ToList();
            var tstData = (from tst in dbResult
                           where tst.OPTION_TYPE==OptionType
                           select new
                           {
                               OptionText = tst.OPTION_TEXT,
                               OptionValue = tst.OPTION_VALUE
                           });
            return Json(tstData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployees(string EmployeeType)
        {
            var context = new TST_Employees_Select();

            var query = from p in context.TST_Employees
                        select p;

            var dbResult = query.ToList();
            var tstData = (from tst in dbResult
                           where tst.EMPLOYEE_TYPE == EmployeeType
                           select new
                           {
                               EMPLOYEE = tst.EMPLOYEE,
                               EMPLOYEE_ID = tst.EMPLOYEE_ID
                           });
            return Json(tstData, JsonRequestBehavior.AllowGet);
        }
                
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_OPTIONS TST_OPTIONS = db.TST_OPTIONS.Find(id);
            if (view_FCS_OPTIONS == null)
            {
                return HttpNotFound();
            }
            return View(TST_OPTIONS);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
