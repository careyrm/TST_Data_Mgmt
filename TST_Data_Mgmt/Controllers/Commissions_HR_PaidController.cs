using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReportViewerForMvc;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using TST_DataMgmt.Models;

namespace TST_DataMgmt.Controllers
{
    public class Commissions_Paid_Controller : Controller
    {
        private Commissions_HR_Paid_Summary_Report db = new Commissions_HR_Paid_Summary_Report();
        private Commissions_HR_Paid_Report dbPaid = new Commissions_HR_Paid_Report();

        // GET: View_Commissions_HR_Paid
        public ActionResult Index()
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(120);
            reportViewer.Height = Unit.Percentage(120);
            
            reportViewer.ShowCredentialPrompts = true;
            reportViewer.ServerReport.ReportPath = "/Reports/Paid_Commissions_By_Employee";
            reportViewer.ServerReport.ReportServerUrl = new Uri("http://testSQLsvr/SSRS_Server/");
            reportViewer.ServerReport.SetParameters(GetParametersServer());

            ViewBag.ReportViewer = reportViewer;

            return View(db.Commissions_Paid_Summary.ToList());
        }

        public ActionResult HRPaidReport()
        {
            return View(dbHRPaid.Commissions_HR_Paid.ToList());
        }

        private ReportParameter[] GetParametersServer()
        {
            string query = "SELECT CONVERT(VARCHAR(10),MAX(DATE_PAID),101) as 'LAST_COMM_PAID_DATE' FROM [dbo].[TST_COMMISSION_PAYMENTS]";
            DataAccess.TSTDataAccess dataAccess = new DataAccess.TSTDataAccess();
            DataSet ds = dataAccess.ReadDataSet(query);
            string lastCommPaidDate = ds.Tables[0].Rows[0]["LAST_COMM_PAID_DATE"].ToString();
                        
            ReportParameter p1 = new ReportParameter("pCommissionPaymentDate", lastCommPaidDate);
            return new ReportParameter[] { p1 };
        }

        public JsonResult GetCommissionHRPaidReportDataByDate(DateTime fromDate, DateTime toDate)
        {
            var context = new Commissions_HR_Paid_Summary_Report();

            var query = from p in context.Commissions_HR_Paid_Summary
                        select p;

            var dbResult = query.ToList();
            var tstData = (from tst in dbResult
                           where tst.LAST_COMMISSION_PAID_DATE >= fromDate && tst.LAST_COMMISSION_PAID_DATE <= toDate
                           select new
                           {
                               COMMISSION_RECORD_ID = tst.COMMISSION_RECORD_ID,
                               COMMISSION_ID = tst.COMMISSION_ID,
                               EMPLOYEE = tst.EMPLOYEE,
                               LAST_COMMISSION_PAID_DATE = tst.LAST_COMMISSION_PAID_DATE,
                               INVOICE_ID = tst.INVOICE_ID,
                               PROPERTY_NAME = tst.PROPERTY_NAME,
                               INVOICE_TYPE = tst.INVOICE_TYPE,
                               COMMISSION_RATE = tst.COMMISSION_RATE,
                               ALLOCATED_COMMISSION = tst.ALLOCATED_COMMISSION,
                               CONTRACT_AMT = tst.CONTRACT_AMT,
                               UNPAID_INV_AMT = tst.UNPAID_INV_AMT,
                               COMMISSIONS_PAID = tst.COMMISSIONS_PAID,
                               TOTAL_OWED_COMMISSIONS = tst.TOTAL_OWED_COMMISSIONS,
                               COMMISSION_PERCENT_PAID = tst.COMMISSION_PERCENT_PAID,
                               PAYMENT_AMOUNT = tst.PAYMENT_AMOUNT
                               
                           });
            return Json(tstData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCommissionHRPaidGridReportDataByDate(DateTime fromDate, DateTime toDate)
        {
            var context = new Commissions_HR_Paid_Report();

            var query = from p in context.Commissions_HR_Paid
                        select p;

            var dbResult = query.ToList();
            var tstData = (from tst in dbResult
                           where tst.LAST_COMMISSION_PAID_DATE >= fromDate && tst.LAST_COMMISSION_PAID_DATE <= toDate
                           select new
                           {
                               COMMISSION_RECORD_ID = tst.COMMISSION_RECORD_ID,
                               COMMISSION_ID = tst.COMMISSION_ID,
                               INVOICE_ID = tst.INVOICE_ID,
                               INVOICE_TYPE = tst.INVOICE_TYPE,
                               PROPERTY_NAME = tst.PROPERTY_NAME,
                               BILL_TO_ACCOUNT_MANAGER = tst.BILL_TO_ACCOUNT_MANAGER,
                               EMPLOYEE_ID = tst.EMPLOYEE_ID,
                               EMPLOYEE = tst.EMPLOYEE,
                               EMPLOYEE_TYPE = tst.EMPLOYEE_TYPE,
                               INVOICE_TOTAL = tst.INVOICE_TOTAL,
                               CONTRACT_AMOUNT = tst.CONTRACT_AMOUNT,
                               REV_CONTRACT_AMOUNT = tst.REV_CONTRACT_AMOUNT,
                               INVOICE_AMOUNT_PAID = tst.INVOICE_AMOUNT_PAID,
                               INVOICE_PERCENTAGE_PAID = tst.INVOICE_PERCENTAGE_PAID,
                               LAST_INVOICE_PAYMENT_DATE = tst.LAST_INVOICE_PAYMENT_DATE,
                               COMMISSION_RATE = tst.COMMISSION_RATE,
                               COMMISSION_RATE_TYPE = tst.COMMISSION_RATE_TYPE,
                               ALLOCATED_COMMISSION = tst.ALLOCATED_COMMISSION,
                               COMMISSIONS_PAID = tst.COMMISSIONS_PAID,
                               COMMISSION_PERCENT_PAID = tst.COMMISSION_PERCENT_PAID,
                               LAST_COMMISSION_PAID_DATE = tst.LAST_COMMISSION_PAID_DATE,
                               SVC_STATUS = tst.SVC_STATUS,
                               SVC_STATUS_UPDATED_ON = tst.SVC_STATUS_UPDATED_ON,
                               SVC_SALES_EMP_ID = tst.SVC_SALES_EMP_ID,
                               INVOICE_DATE = tst.INVOICE_DATE,
                               INVOICE_STATUS = tst.INVOICE_STATUS,
                               SVC_SALES_EMPLOYEE = tst.SVC_SALES_EMPLOYEE,
                               DISPATCH_DATE = tst.DISPATCH_DATE,
                               ACCOUNT_TYPE = tst.ACCOUNT_TYPE,
                               COMPANY_ID = tst.COMPANY_ID,
                               NOTES = tst.NOTES
                           });
            return Json(tstData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCommissionHRPaidReportDataByID(int commRecID)
        {
            var context = new Commissions_HR_Paid_Report();

            var query = from p in context.Commissions_HR_Paid
                        select p;

            var dbResult = query.ToList();
            var tstData = (from tst in dbResult
                           where tst.COMMISSION_RECORD_ID == commRecID
                           select new
                           {
                               COMMISSION_RECORD_ID = tst.COMMISSION_RECORD_ID,
                               COMMISSION_ID = tst.COMMISSION_ID,
                               INVOICE_ID = tst.INVOICE_ID,
                               INVOICE_TYPE = tst.INVOICE_TYPE,
                               PROPERTY_NAME = tst.PROPERTY_NAME,
                               BILL_TO_ACCOUNT_MANAGER = tst.BILL_TO_ACCOUNT_MANAGER,
                               EMPLOYEE_ID = tst.EMPLOYEE_ID,
                               EMPLOYEE = tst.EMPLOYEE,
                               EMPLOYEE_TYPE = tst.EMPLOYEE_TYPE,
                               INVOICE_TOTAL = tst.INVOICE_TOTAL,
                               CONTRACT_AMOUNT = tst.CONTRACT_AMOUNT,
                               REV_CONTRACT_AMOUNT = tst.REV_CONTRACT_AMOUNT,
                               INVOICE_AMOUNT_PAID = tst.INVOICE_AMOUNT_PAID,
                               INVOICE_PERCENTAGE_PAID = tst.INVOICE_PERCENTAGE_PAID,
                               LAST_INVOICE_PAYMENT_DATE = tst.LAST_INVOICE_PAYMENT_DATE,
                               COMMISSION_RATE = tst.COMMISSION_RATE,
                               COMMISSION_RATE_TYPE = tst.COMMISSION_RATE_TYPE,
                               ALLOCATED_COMMISSION = tst.ALLOCATED_COMMISSION,
                               COMMISSIONS_PAID = tst.COMMISSIONS_PAID,
                               COMMISSION_PERCENT_PAID = tst.COMMISSION_PERCENT_PAID,
                               LAST_COMMISSION_PAID_DATE = tst.LAST_COMMISSION_PAID_DATE,
                               SVC_STATUS = tst.SVC_STATUS,
                               SVC_STATUS_UPDATED_ON = tst.SVC_STATUS_UPDATED_ON,
                               SVC_SALES_EMP_ID = tst.SVC_SALES_EMP_ID,
                               INVOICE_DATE = tst.INVOICE_DATE,
                               INVOICE_STATUS = tst.INVOICE_STATUS,
                               SVC_SALES_EMPLOYEE = tst.SVC_SALES_EMPLOYEE,
                               DISPATCH_DATE = tst.DISPATCH_DATE,
                               ACCOUNT_TYPE = tst.ACCOUNT_TYPE,
                               COMPANY_ID = tst.COMPANY_ID,
                               NOTES = tst.NOTES
                           });
            return Json(tstData, JsonRequestBehavior.AllowGet);
        }
        
    }
}
