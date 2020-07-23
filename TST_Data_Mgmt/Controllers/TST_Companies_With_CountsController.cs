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
    public class TST_Companies_With_CountsController : Controller
    {
        private MAS_Companies_Report db = new MAS_Companies_Report();

        public ActionResult Index()
        {
            return View(db.TST_Companies_With_Counts.ToList());
        }

        public JsonResult GetCompanies()
        {
            string query = "SELECT COMP_ID,COMPANY_NAME,ACCOUNT_TYPE,COMPANY_ID,ACCOUNT_REP,DATE_CREATED,DATE_MODIFIED " +
                            "FROM [TST_DATA].[dbo].[TST_Companies_With_Counts]   " +
                            "WHERE (COMPANY_ID NOT IN ('UNKOWN','TRAINING')) " +
                            "ORDER BY COMPANY_NAME";

            DataAccess.TSTDataAccess da = new DataAccess.TSTDataAccess();
            DataTable dt = da.ReadDataTable(query);

            var tstData = (from row in dt.AsEnumerable()
                           select new
                           {
                               COMP_ID = row.Field<int>("COMP_ID"),
                               COMPANY_NAME = row.Field<string>("COMPANY_NAME").ToString(),
                               ACCOUNT_TYPE = row.Field<string>("ACCOUNT_TYPE").ToString(),
                               COMPANY_ID = row.Field<string>("COMPANY_ID").ToString(),
                               ACCOUNT_REP = row.Field<string>("ACCOUNT_REP").ToString(),
                               DATE_CREATED = row.Field<DateTime?>("DATE_CREATED"),
                               DATE_MODIFIED = row.Field<DateTime?>("DATE_MODIFIED")
                           });
            JsonResult json = Json(tstData, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
		}

        public JsonResult GetInvoices(string COMPANY_ID)
        {
            string query = "SELECT  d.[INVOICE_ID],d.[INVOICE_STATUS],d.[INVOICE_TYPE],d.[COMPANY_NAME] ,d.[COMPANY_ID] ,d.[PROPERTY_NAME] "+
                            ", d.[PROPERTY_CITY],d.[PROPERTY_STATE],d.[BILL_TO_ACCOUNT_MANAGER],d.[INVOICE_TOTAL],d.[INVOICE_TAX], d.[INVOICE_BILL_TO_ID] " +
                            ",CASE WHEN d.[INVOICE_SENT_DATE] = '1900-01-01 00:00:00.000' OR d.[INVOICE_SENT_DATE] IS NULL THEN ''  ELSE CONVERT(varchar(10),d.[INVOICE_SENT_DATE],101) END AS [INVOICE_SENT_DATE] " +
                            ",CASE WHEN d.[SENT_TO_MAS_DATE] IS NULL THEN '' ELSE CONVERT(varchar(10),d.[SENT_TO_MAS_DATE],101) END AS [SENT_TO_MAS_DATE] " +
                            " FROM [TST_DATA].[dbo].[JOB_DETAILS] d  " +
                            " WHERE [COMPANY_ID]='" + COMPANY_ID + "'";

            DataAccess.TSTDataAccess da = new DataAccess.TSTDataAccess();
            DataTable dt = da.ReadDataTable(query);

            var tstData = (from row in dt.AsEnumerable()
                           select new
                           {
                               INVOICE_ID = row.Field<string>("INVOICE_ID").ToString(),
                               INVOICE_STATUS = row.Field<string>("INVOICE_STATUS").ToString(),
                               INVOICE_TYPE = row.Field<string>("INVOICE_TYPE").ToString(),
                               COMPANY_NAME = row.Field<string>("COMPANY_NAME").ToString(),
                               COMPANY_ID = row.Field<string>("COMPANY_ID").ToString(),
                               PROPERTY_NAME = row.Field<string>("PROPERTY_NAME").ToString(),
                               PROPERTY_CITY = row.Field<string>("PROPERTY_CITY").ToString(),
                               PROPERTY_STATE = row.Field<string>("PROPERTY_STATE").ToString(),
                               BILL_TO_ACCOUNT_MANAGER = row.Field<string>("BILL_TO_ACCOUNT_MANAGER").ToString(),
                               INVOICE_TOTAL = row.Field<string>("INVOICE_TOTAL").ToString(),
                               INVOICE_TAX = row.Field<string>("INVOICE_TAX").ToString(),
                               INVOICE_BILL_TO_ID = row.Field<string>("INVOICE_BILL_TO_ID").ToString(),
                               INVOICE_SENT_DATE = row.Field<string>("INVOICE_SENT_DATE").ToString(),
                               SENT_TO_MAS_DATE = row.Field<string>("SENT_TO_MAS_DATE").ToString()
                           });

            return Json(tstData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyDetails(int COMP_ID)
        {
            string query = "SELECT [COMP_ID],[COMPANY_ID],[COMPANY_NAME],[ADDRESS],[ADDRESS_2],[CITY],[STATE],[ZIP]" +
                           ",[BILL_ADDRESS],[BILL_ADDRESS_2],[BILL_CITY],[BILL_STATE],[BILL_ZIP],[ACCOUNT_TYPE] "+
                           ", ISNULL([ACCOUNT_REP],'Unknown') AS ACCOUNT_REP ,ISNULL([ACCOUNT_REP_ID],280) AS ACCOUNT_REP_ID" +
                           ",[LEAK_COUNT],[CONTRACT_COUNT],[BILLABLE_COUNT] ,[NONBILLABLE_COUNT],[CANCELLED_COUNT],[TOTAL_INVOICE_COUNT],[DATE_CREATED],[DATE_MODIFIED]" +
                           " FROM [TST_DATA].[dbo].[TST_Companies_With_Counts] " +
                           " WHERE [COMP_ID]=" + COMP_ID;

            DataAccess.TSTDataAccess da = new DataAccess.TSTDataAccess();
            DataTable dt = da.ReadDataTable(query);          

            var tstData = (from row in dt.AsEnumerable()
                           select new
                           {
                               COMP_ID = row.Field<int>("COMP_ID").ToString(),
                               COMPANY_ID = row.Field<string>("COMPANY_ID").ToString(),
                               COMPANY_NAME = row.Field<string>("COMPANY_NAME").ToString(),
                               ADDRESS = row.Field<string>("ADDRESS").ToString(),
                               ADDRESS_2 = row.Field<string>("ADDRESS_2").ToString(),
                               CITY = row.Field<string>("CITY").ToString(),
                               STATE = row.Field<string>("STATE").ToString(),
                               ZIP = row.Field<string>("ZIP").ToString(),
                               BILL_ADDRESS = row.Field<string>("BILL_ADDRESS").ToString(),
                               BILL_ADDRESS_2 = row.Field<string>("BILL_ADDRESS_2").ToString(),
                               BILL_CITY = row.Field<string>("BILL_CITY").ToString(),
                               BILL_STATE = row.Field<string>("BILL_STATE").ToString(),
                               BILL_ZIP = row.Field<string>("BILL_ZIP").ToString(),
                               ACCOUNT_TYPE = row.Field<string>("ACCOUNT_TYPE").ToString(),
                               ACCOUNT_REP = row.Field<string>("ACCOUNT_REP").ToString(),
                               ACCOUNT_REP_ID = row.Field<int>("ACCOUNT_REP_ID").ToString(),
                               LEAK_COUNT = row.Field<int>("LEAK_COUNT").ToString(),
                               CONTRACT_COUNT = row.Field<int>("CONTRACT_COUNT").ToString(),
                               BILLABLE_COUNT = row.Field<int>("BILLABLE_COUNT").ToString(),
                               NONBILLABLE_COUNT = row.Field<int>("NONBILLABLE_COUNT").ToString(),
                               CANCELLED_COUNT = row.Field<int>("CANCELLED_COUNT").ToString(),
                               TOTAL_INVOICE_COUNT = row.Field<int>("TOTAL_INVOICE_COUNT").ToString(),
                               DATE_CREATED = row.Field<DateTime?>("DATE_CREATED"),
                               DATE_MODIFIED = row.Field<DateTime?>("DATE_MODIFIED")
                           });

            return Json(tstData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMASCompany(string COMPANY_ID)
        {
            string query = "SELECT [MAS_CUSTOMER_ID],[COMPANY_NAME],[ADDRESS],[ADDRESS_2] ,[CITY] ,[STATE] ,[ZIP] " +
                            "FROM [TST_DATA].[dbo].[MAS_Companies] " +
                            "WHERE [MAS_CUSTOMER_ID] = '"+ COMPANY_ID +"' " +
                            "ORDER BY [COMPANY_NAME]";

            DataAccess.TSTDataAccess da = new DataAccess.TSTDataAccess();
            DataTable dt = da.ReadDataTable(query);

            var tstData = (from row in dt.AsEnumerable()
                           select new
                           {
                               MAS_CUSTOMER_ID = row.Field<string>("MAS_CUSTOMER_ID").ToString(),
                               COMPANY_NAME = row.Field<string>("COMPANY_NAME").ToString(),
                               ADDRESS = row.Field<string>("ADDRESS").ToString(),
                               ADDRESS_2 = row.Field<string>("ADDRESS_2").ToString(),
                               CITY = row.Field<string>("CITY").ToString(),
                               STATE = row.Field<string>("STATE").ToString(),
                               ZIP = row.Field<string>("ZIP").ToString()
                           });

            return Json(tstData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccountReps()
        {
            string query = "SELECT [EMPLOYEE_ID],[EMPLOYEE] " +
                            "FROM [TST_DATA].[dbo].[TST_Employees] " +
                            "WHERE [EMPLOYEE_TYPE] = 'SALES' AND [EMPLOYEE_STATUS]='Active' " +
                            "ORDER BY [EMPLOYEE]";

            DataAccess.TSTDataAccess da = new DataAccess.TSTDataAccess();
            DataTable dt = da.ReadDataTable(query);

            var tstData = (from row in dt.AsEnumerable()
                           select new
                           {
                               EMPLOYEE_ID = row.Field<int>("EMPLOYEE_ID").ToString(),
                               EMPLOYEE = row.Field<string>("EMPLOYEE").ToString()
                           });

            return Json(tstData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddEditCompany(int COMP_ID, string COMPANY_NAME, string COMPANY_ID,  string ACCOUNT_TYPE
                                         , string ACCOUNT_REP, int ACCOUNT_REP_ID, string ADDRESS, string ADDRESS_2, string CITY, string STATE, string ZIP
                                         , string BILL_ADDRESS, string BILL_ADDRESS_2, string BILL_CITY, string BILL_STATE, string BILL_ZIP, string frmAction)
        {

           if (frmAction == "Add")
            {
                string queryAdd = "INSERT INTO  [TST_DATA].[dbo].[TST_COMPANIES] " +
                              "([COMPANY_NAME],[COMPANY_ID],[ACCOUNT_TYPE] ,[ACCOUNT_REP] ,[ACCOUNT_REP_ID]" +
                              ",[ADDRESS],[ADDRESS_2],[CITY] ,[STATE] ,[ZIP]" +
                              ",[BILL_ADDRESS],[BILL_ADDRESS_2],[BILL_CITY],[BILL_STATE],[BILL_ZIP],[DATE_CREATED]) " +
                              "VALUES ('" + COMPANY_NAME + "','" + COMPANY_ID + "','" + ACCOUNT_TYPE + "','" + ACCOUNT_REP + "'" + "','" + ACCOUNT_REP_ID + "'" +
                               "','" + ADDRESS + "','" + ADDRESS_2 + "','" + CITY + "'" + "','" + STATE + "'" + "','" + ZIP + "'" +
                              "','" + BILL_ADDRESS + "','" + BILL_ADDRESS_2 + "','" + BILL_CITY + "'" + "','" + BILL_STATE + "'" + "','" + BILL_ZIP + "',GETDATE())";

                DataAccess.TSTDataAccess daAdd = new DataAccess.TSTDataAccess();
                bool actionResults = daAdd.QryCommand(queryAdd);

                string queryGetNewRecord = "SELECT MAX(COMP_ID) AS COMP_ID,ACTION_RESULTS = '" + actionResults.ToString() + "' " +
                                           "FROM [TST_DATA].[dbo].[TST_COMPANIES] " +
                                           "WHERE [COMPANY_ID]='" + COMPANY_ID + "' AND " +
                                           "[COMPANY_NAME]='" + COMPANY_NAME + "'";
                DataAccess.TSTDataAccess daNewRec = new DataAccess.TSTDataAccess();
                DataTable dt = daNewRec.ReadDataTable(queryGetNewRecord);

                var tstData = (from row in dt.AsEnumerable()
                               select new
                               {
                                   COMP_ID = row.Field<int>("COMP_ID"),
                                   ACTION_RESULTS = row.Field<string>("ACTION_RESULTS").ToString()
                               });


                return Json(tstData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string queryOrigRec = "SELECT [COMPANY_ID],[COMPANY_NAME] " +
                           ", ISNULL([ACCOUNT_REP],'Unknown') AS ACCOUNT_REP ,ISNULL([ACCOUNT_REP_ID],280) AS ACCOUNT_REP_ID" +
                            " FROM [TST_DATA].[dbo].[TST_COMPANIES] " +
                           " WHERE [COMP_ID]=" + COMP_ID;

                DataAccess.TSTDataAccess daOrigRec = new DataAccess.TSTDataAccess();
                DataTable dtOrigRec = daOrigRec.ReadDataTable(queryOrigRec);

                string origCompanyID = dtOrigRec.Rows[0].Field<string>("COMPANY_ID");
                string origCompanyName = dtOrigRec.Rows[0].Field<string>("COMPANY_NAME");
                string origAcctRep = dtOrigRec.Rows[0].Field<string>("ACCOUNT_REP");
                int origAcctRepID = dtOrigRec.Rows[0].Field<int>("ACCOUNT_REP_ID");

                string queryEdit = "UPDATE [TST_DATA].[dbo].[TST_COMPANIES] SET " +
                              "[COMPANY_ID] = '" + COMPANY_ID + "'" +
                              ",[ACCOUNT_TYPE]='" + ACCOUNT_TYPE.ToUpper() + "'" +
                              ",[ACCOUNT_REP]='" + ACCOUNT_REP + "'" +
                              ",[ACCOUNT_REP_ID]='" + ACCOUNT_REP_ID + "'" +
                              ",[ADDRESS]='" + ADDRESS + "'" +
                              ",[ADDRESS_2] = '" + ADDRESS_2 +"'" +
                              ",[CITY]='" + CITY + "'" +
                              ",[STATE]='" + STATE + "'" +
                              ",[ZIP]='" + ZIP + "'" +
                              ",[BILL_ADDRESS]='" + BILL_ADDRESS + "'" +
                              ",[BILL_ADDRESS_2] = '" + BILL_ADDRESS_2 + "'" +
                              ",[BILL_CITY]='" + BILL_CITY + "'" +
                              ",[BILL_STATE]='" + BILL_STATE + "'" +
                              ",[BILL_ZIP]='" + BILL_ZIP + "'" +
                              ",[DATE_MODIFIED]=GETDATE() " +
                              " WHERE [COMP_ID] = " + COMP_ID;

                DataAccess.TSTDataAccess daAdd = new DataAccess.TSTDataAccess();
                bool actionResults = daAdd.QryCommand(queryEdit);

                string queryGetUpdatedRecord = "SELECT COMP_ID,ACTION_RESULTS = '" + actionResults.ToString() + "' " +
                                           "FROM [TST_DATA].[dbo].[TST_COMPANIES] " +
                                           "WHERE [COMP_ID]=" + COMP_ID ;

                DataAccess.TSTDataAccess daNewRec = new DataAccess.TSTDataAccess();
                DataTable dt = daNewRec.ReadDataTable(queryGetUpdatedRecord);

                var tstData = (from row in dt.AsEnumerable()
                               select new
                               {
                                   COMP_ID = row.Field<int>("COMP_ID"),
                                   ACTION_RESULTS = row.Field<string>("ACTION_RESULTS").ToString()
                               });
                //update invoices with the new company id only if it has changed
                if(origCompanyID != COMPANY_ID || origCompanyName != COMPANY_NAME)
                {
                    string qryUpdateInvCompanyID = "UPDATE [TST_DATA].[dbo].[JOB_DETAILS] SET COMPANY_ID = '" + COMPANY_ID + "', COMPANY_NAME='"+ COMPANY_NAME + "' " +
                                                   "WHERE COMPANY_ID='" + origCompanyID + "' AND COMPANY_NAME='"+ origCompanyName + "'";
                    DataAccess.TSTDataAccess daUpdateInvCompanyID = new DataAccess.TSTDataAccess();
                    bool updateInvCompanyIDResults = daUpdateInvCompanyID.QryCommand(qryUpdateInvCompanyID);

                    //Update the pending commission record for the company if it had been changed
                    string qryUpdateCommRecCompanyID = "UPDATE [TST_DATA].[dbo].[TST_COMMISSION_RECORDS] SET COMPANY_ID = '" + COMPANY_ID + "' " +
                                                       "WHERE COMPANY_ID='" + origCompanyID + "' AND SVC_STATUS in  ('Pending') ";
                    DataAccess.TSTDataAccess daUpdateCommRecCompanyID = new DataAccess.TSTDataAccess();
                    bool updateCommRecCompanyIDResults = daUpdateCommRecCompanyID.QryCommand(qryUpdateCommRecCompanyID);
                }

                if(origAcctRepID != ACCOUNT_REP_ID)
                {
                    //if the original acct rep was unknown so update all invoices with the new acct rep
                    if (origAcctRepID == 123)
                    {
                        string qryUpdateInvAcctRep = "UPDATE [TST_DATA].[dbo].[JOB_DETAILS] SET BILL_TO_ACCOUNT_MANAGER = '" + ACCOUNT_REP + "' " +
                                                     "WHERE COMPANY_ID='" + COMPANY_ID + "' AND BILL_TO_ACCOUNT_MANAGER='" + origAcctRep + "'";
                        DataAccess.TSTDataAccess daUpdateInvAcctRep = new DataAccess.TSTDataAccess();
                        bool updateInvAcctRepResults = daUpdateInvAcctRep.QryCommand(qryUpdateInvAcctRep);

                        //Update the commission record for the account manager if it had been originally set to unknown
                        string qryUpdateCommRecAcctRep = "UPDATE [TST_DATA].[dbo].[TST_COMMISSION_RECORDS] SET BILL_TO_ACCOUNT_MANAGER = '" + ACCOUNT_REP + "' " +
                                                         "WHERE COMPANY_ID='" + COMPANY_ID + "' AND  ISNULL(cr.SVC_STATUS,'Pending') IN ('Pending') AND BILL_TO_ACCOUNT_MANAGER='" + origAcctRep + "'";
                        DataAccess.TSTDataAccess daUpdateCommRecAcctRep = new DataAccess.TSTDataAccess();
                        bool updateCommRecAcctRepResults = daUpdateCommRecAcctRep.QryCommand(qryUpdateCommRecAcctRep);

                        //Update the commission record for the employee owed (AcctMgr only) if it had been originally set to unknown
                        string qryUpdateCommRecEmpOwed = "UPDATE [TST_DATA].[dbo].[TST_COMMISSION_RECORDS] SET EMPLOYEE = '" + ACCOUNT_REP + "', EMPLOYEE_ID= " + ACCOUNT_REP_ID +
                                                         "WHERE COMPANY_ID='" + COMPANY_ID + "' AND EMPLOYEE_TYPE='ACCT_MGR' ANDISNULL(cr.SVC_STATUS,'Pending') IN ('Pending') AND EMPLOYEE_ID='" + origAcctRepID + "'";
                        DataAccess.TSTDataAccess daUpdateCommRecEmpOwed = new DataAccess.TSTDataAccess();
                        bool updateCommRecEmpOwedpResults = daUpdateCommRecEmpOwed.QryCommand(qryUpdateCommRecAcctRep);
                    }
                }

                return Json(tstData, JsonRequestBehavior.AllowGet);

            }
        }




        }
    }
