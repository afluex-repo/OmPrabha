using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmPrabha.Models;
using System.Data;
using OmPrabha.Filter;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace OmPrabha.Controllers
{
    public class ExpenseController : AdminBaseController
    {
        // GET: Farmers
        public ActionResult AddAccount(string id)
        {
            Expenses model = new Expenses();
            try
            {
                if (id != null)
                {
                    model.Pk_BankId = Crypto.Decrypt(id);
                    List<Expenses> lst = new List<Expenses>();
                    DataSet ds = model.GetAccountlistById();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                        model.AccountNumber = ds.Tables[0].Rows[0]["AcNumber"].ToString();
                        model.AcountHolder = ds.Tables[0].Rows[0]["AcHolderName"].ToString();
                        model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                        model.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                        model.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                        model.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString();
                        model.EncryptKey = ds.Tables[0].Rows[0]["Pk_BankId"].ToString();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AddAccount")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveAccount(Expenses model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveAccountDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["MsgAccount"] = "Account Details Saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["MsgAccount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["MsgAccount"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AddAccount")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateAccount(Expenses model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateAccountDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["MsgAccount"] = "Account Details Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["MsgAccount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["MsgAccount"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult Accountlist(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            DataSet ds = model.GetAccountlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (i < 25)
                    {
                        Expenses obj = new Expenses();
                    obj.AccountNumber = r["AcNumber"].ToString();
                    obj.AcountHolder = r["AcHolderName"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.BalanceAmount = r["BalanceAmount"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.Pk_BankId = Crypto.Encrypt(r["Pk_BankId"].ToString());
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_BankId"].ToString());
                    lst.Add(obj);
                }
                    i = i + 1;
                }
                model.AccountList = lst;
            }
                return View(model);
        }

        [HttpPost]
        [ActionName("Accountlist")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AccountList(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetAccountlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.AccountNumber = r["AcNumber"].ToString();
                    obj.AcountHolder = r["AcHolderName"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.BalanceAmount = r["BalanceAmount"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.Pk_BankId = Crypto.Encrypt(r["Pk_BankId"].ToString());
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_BankId"].ToString());
                    lst.Add(obj);
                }
                model.AccountList = lst;
            }

            return View(model);
        }
        public ActionResult DeleteAccount(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses obj = new Expenses();
                obj.Pk_BankId = Crypto.Decrypt(id);
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteAccount();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["AccountList"] = "Account deleted successfully";
                        FormName = "AccountList";
                        Controller = "Expense";
                    }
                    else
                    {
                        TempData["AccountList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AccountList";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AccountList"] = ex.Message;
                FormName = "AccountList";
                Controller = "Expense";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult AccountStatus(string id, string IsActive)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses obj = new Expenses();
                if (IsActive == "False")
                {
                    obj.IsActive = "1";
                }
                else
                {
                    obj.IsActive = "0";
                }
                obj.Pk_BankId = Crypto.Decrypt(id);
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.AccountStatus();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["AccountList"] = "Status Updated successfully";
                        FormName = "AccountList";
                        Controller = "Expense";
                    }
                    else
                    {
                        TempData["AccountList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AccountList";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AccountList"] = ex.Message;
                FormName = "AccountList";
                Controller = "Expense";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult AccountDetailsBYId(string id)
        {
            string FormName = "";
            string Controller = "";
            Expenses model = new Expenses();
            try
            {
                model.Pk_BankId = Crypto.Decrypt(id);
                List<Expenses> lst = new List<Expenses>();
                DataSet ds = model.GetAccountlistById();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Expenses obj = new Expenses();
                        obj.AccountNumber = r["AcNumber"].ToString();
                        obj.AcountHolder = r["AcHolderName"].ToString();
                        obj.BankName = r["BankName"].ToString();
                        obj.BranchName = r["BranchName"].ToString();
                        obj.IFSCCode = r["IFSCCode"].ToString();
                        obj.IsActive = r["IsActive"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["Pk_BankId"].ToString());
                        lst.Add(obj);
                    }
                    model.AccountList = lst;
                    FormName = "AddAccount";
                    Controller = "Expense";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        public ActionResult AddExpense(string id)
        {
            Expenses model = new Expenses();
            try
            {
                if (id != null)
                {
                    model.Pk_ExpenseId = Crypto.Decrypt(id);
                    List<Expenses> lst = new List<Expenses>();
                    DataSet ds = model.GetExpenselistById();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.ExpenseName = ds.Tables[0].Rows[0]["ExpenseName"].ToString();
                        model.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString();
                        model.EncryptKey = ds.Tables[0].Rows[0]["Pk_ExpenseId"].ToString();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AddExpense")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveExpense(Expenses model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveExpenseDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["MsgExpense"] = "Expeses  Saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["MsgExpense"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["MsgExpense"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AddExpense")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateExpense(Expenses model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateExpenseDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["MsgExpense"] = "Expenses Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["MsgExpense"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["MsgExpense"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult Expenselist(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            DataSet ds = model.GetExpenselist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (i < 25)
                    {
                        Expenses obj = new Expenses();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.FK_ExpenseNameId = Crypto.Encrypt(r["ExpenseName"].ToString());
                    obj.IsActive = r["IsActive"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_ExpenseId"].ToString());
                    lst.Add(obj);
                }
                model.ExpenseList = lst;
                i = i + 1;
            }
        }
            return View(model);
        }
        [HttpPost]
        [ActionName("Expenselist")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ExpenseList(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetExpenselist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.FK_ExpenseNameId= Crypto.Encrypt(r["ExpenseName"].ToString());
                    obj.IsActive = r["IsActive"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_ExpenseId"].ToString());
                    lst.Add(obj);
                }
                model.ExpenseList = lst;
            }

            return View(model);
        }
        public ActionResult DeleteExpense(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses obj = new Expenses();
                obj.Pk_ExpenseId = Crypto.Decrypt(id);
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteExpense();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Expenselist"] = "Expense deleted successfully";
                        FormName = "Expenselist";
                        Controller = "Expense";
                    }
                    else
                    {
                        TempData["Expenselist"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Expenselist";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Expenselist"] = ex.Message;
                FormName = "Expenselist";
                Controller = "Expense";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ExpenseStatus(string id, string IsActive)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses obj = new Expenses();
                if (IsActive == "False")
                {
                    obj.IsActive = "1";
                }
                else
                {
                    obj.IsActive = "0";
                }
                obj.Pk_ExpenseId = Crypto.Decrypt(id);
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.ExpenseStatus();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Expenselist"] = "Status Updated successfully";
                        FormName = "Expenselist";
                        Controller = "Expense";
                    }
                    else
                    {
                        TempData["Expenselist"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Expenselist";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Expenselist"] = ex.Message;
                FormName = "Expenselist";
                Controller = "Expense";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ExpenseDetailsBYId(string id)
        {

            Expenses model = new Expenses();
            try
            {
                model.Pk_ExpenseId = Crypto.Decrypt(id);
                List<Expenses> lst = new List<Expenses>();
                DataSet ds = model.GetExpenselistById();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Expenses obj = new Expenses();
                        obj.ExpenseName = r["ExpenseName"].ToString();
                        obj.IsActive = r["IsActive"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["Pk_ExpenseId"].ToString());
                        lst.Add(obj);
                    }
                    model.ExpenseList = lst;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        public ActionResult CrExpense(Expenses model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            Plot obj = new Plot();
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            List<SelectListItem> ExpenseType = Common.ExpenseType();
            ViewBag.ExpenseType = ExpenseType;
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            int count = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
               
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                        //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "1" });
                    }
                  
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count = count + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            return View();
        }
        public ActionResult GetExpenseDetails(string ExpenseID)
        {
            try
            {
                Expenses model = new Expenses();
                model.ExpenseID = ExpenseID;
                #region GetExpenseName
                List<SelectListItem> ddlexpensename = new List<SelectListItem>();
                model.Result = "yes";
                DataSet ds = model.GetExpenseNameList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ddlexpensename.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["FK_ExpenseId"].ToString() });

                    }
                }
                model.ddlexpensename = ddlexpensename;
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult save(Expenses order, string dataValue)
        {
            bool status = false;
            string ExpenseType = "";
            string ExpenseName = "";
            string PaymentMode = "";
            string TransactionID = "";
            string Check = "";
            string Amount = "";
            string Date = "";
            string Remarks = "";
            var isValidModel = TryUpdateModel(order);
            var jss = new JavaScriptSerializer();
            var jdv = jss.Deserialize<dynamic>(dataValue);
            //var serializeData = JsonConvert.DeserializeObject<List<Customer>>(empdata);
            //System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            DataTable CrExpenseDetails = new DataTable();
            order.ChequeStatus = "Cr";
            CrExpenseDetails.Columns.Add("ExpenseType");
            CrExpenseDetails.Columns.Add("ExpenseName");
            CrExpenseDetails.Columns.Add("TransactionID");
            CrExpenseDetails.Columns.Add("PaymentMode");
            CrExpenseDetails.Columns.Add("Check");
            CrExpenseDetails.Columns.Add("Amount");
            CrExpenseDetails.Columns.Add("Date");
            CrExpenseDetails.Columns.Add("Remarks");
            DataTable dt = new DataTable();
            dt = JsonConvert.DeserializeObject<DataTable>(jdv["CRExpenses"]);
            int numberOfRecords = dt.Rows.Count;
            //foreach (DataRow row in dt.Rows)
                foreach (DataRow row in dt.Rows)
                {
                
                   ExpenseType = row["ExpenseType"].ToString();
                   ExpenseName = row["ExpenseName"].ToString();
                   TransactionID = row["TransactionID"].ToString();
                   PaymentMode = row["PaymentMode"].ToString();
                   Check = row["Check"].ToString();
                   Amount = row["Amount"].ToString();
                   Date = string.IsNullOrEmpty(row["Date"].ToString()) ? null : Common.ConvertToSystemDate(row["Date"].ToString(), "dd/MM/yyyy");
                   Remarks = row["Remarks"].ToString();
                   CrExpenseDetails.Rows.Add(ExpenseType, ExpenseName, TransactionID, PaymentMode, Check, Amount, Date,Remarks);
                }
            order.dtExpenseDetails = CrExpenseDetails;
            order.AddedBy = Session["Pk_AdminId"].ToString();
            //jdv["Amount"]= string.IsNullOrEmpty(jdv["Amount"]) ? null : Common.ConvertToSystemDate(jdv["Amount"], "dd/MM/yyyy");
            DataSet ds = new DataSet();
            ds = order.SaveCrExpenseDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["DrExpenses"] = " Expenses Save  successfully";
                    status = true;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["DrExpenses"] = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            else
            {
                TempData["DrExpenses"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult DrExpense(Expenses model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            Plot obj = new Plot();
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            List<SelectListItem> ExpenseType = Common.ExpenseType();
            ViewBag.ExpenseType = ExpenseType;
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            int count = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count = count + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            return View();
        }
        [HttpPost]
        public JsonResult SaveDrExpense(Expenses order, string dataValue)
        {
            bool status = false;
            try
            {
                string ExpenseType = "";
                string ExpenseName = "";
                string PaymentMode = "";
                string TransactionID = "";
                string Check = "";
                string Amount = "";
                string Date = "";
                string Remarks = "";
                var isValidModel = TryUpdateModel(order);
                var jss = new JavaScriptSerializer();
                var jdv = jss.Deserialize<dynamic>(dataValue);

                DataTable DrExpenseDetails = new DataTable();
                DrExpenseDetails.Columns.Add("ExpenseType");
                DrExpenseDetails.Columns.Add("ExpenseName");
                DrExpenseDetails.Columns.Add("TransactionID");
                DrExpenseDetails.Columns.Add("PaymentMode");
                DrExpenseDetails.Columns.Add("Check");
                DrExpenseDetails.Columns.Add("Amount");
                DrExpenseDetails.Columns.Add("Date");
                DrExpenseDetails.Columns.Add("Remarks");
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(jdv["DrExpenses"]);
                int numberOfRecords = dt.Rows.Count;
                //foreach (DataRow row in dt.Rows)
                foreach (DataRow row in dt.Rows)
                {

                    ExpenseType = row["ExpenseType"].ToString();
                    ExpenseName = row["ExpenseName"].ToString();
                    TransactionID = row["TransactionID"].ToString();
                    PaymentMode = row["PaymentMode"].ToString();
                    Check = row["Check"].ToString();
                    Amount = row["Amount"].ToString();
                    Date = string.IsNullOrEmpty(row["Date"].ToString()) ? null : Common.ConvertToSystemDate(row["Date"].ToString(), "dd/MM/yyyy");
                    Remarks = row["Remarks"].ToString();
                    DrExpenseDetails.Rows.Add(ExpenseType, ExpenseName, TransactionID, PaymentMode, Check, Amount, Date, Remarks);
                }
                order.dtExpenseDetails = DrExpenseDetails;
                order.AddedBy = Session["Pk_AdminId"].ToString();
                order.ChequeStatus = "Dr";
                DataSet ds = new DataSet();
                ds = order.SaveDrExpenseDetails();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        order.Result = "Yes";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        order.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["DrExpenses"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return new JsonResult { Data = new { status = order.Result } };
        }

        public ActionResult ViewCrExpense(Expenses model)
        {
            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            List<SelectListItem> ExpenseType = Common.ExpenseType();
            ViewBag.ExpenseType = ExpenseType;
            return View(model);
        }
        [HttpPost]
        [ActionName("ViewCrExpense")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult ViewCrExpens(Expenses model)
        {
            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            List<SelectListItem> ExpenseType = Common.ExpenseType();
            ViewBag.ExpenseType = ExpenseType;
            //List<Expenses> Crlst = new List<Expenses>();
            List<Expenses> Clearedlst = new List<Expenses>();
            List<Expenses> Pendinglst = new List<Expenses>();
            List<Expenses> Bouncelst = new List<Expenses>();
            model.ChequeStatus = "Cr";
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetCrExpenselist();
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.PlotInfo = r["PlotInfo"].ToString();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                 
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    Clearedlst.Add(obj);
                }
                model.ClearedListItem = Clearedlst;
                ViewBag.TotalCleredPaid = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
            }
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow pr in ds.Tables[1].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.PlotInfo = pr["PlotInfo"].ToString();
                    obj.Pk_ExpenseDetailsId = pr["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = pr["ExpenseName"].ToString();
                    obj.ExpenseID = pr["ExpenseType"].ToString();
                    obj.Remarks = pr["Remarks"].ToString();
                    obj.ChequeStatus = pr["TransactionStatus"].ToString();
                    obj.TransactionTy = pr["TransactionType"].ToString();
                    obj.Transanction = pr["TransactionStatus"].ToString();
                    obj.ChequeNo = pr["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = pr["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = pr["CrAmount"].ToString();
                    obj.DrAmount = pr["DrAmount"].ToString();
                    obj.Date = pr["CheckDate"].ToString();
                    Pendinglst.Add(obj);
                }
                model.PendingListItem = Pendinglst;
                ViewBag.TotalPendingPaid = double.Parse(ds.Tables[1].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
            }
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.PlotInfo = r["PlotInfo"].ToString();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    Bouncelst.Add(obj);
                }
                model.BounceListItem = Bouncelst;
                ViewBag.TotalBouncePaid = double.Parse(ds.Tables[2].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
            }
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        Expenses obj = new Expenses();
            //        obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
            //        obj.ExpenseName = r["ExpenseName"].ToString();
            //        obj.ExpenseID = r["ExpenseType"].ToString();
            //        obj.Remarks = r["Remarks"].ToString();
            //        obj.ChequeStatus = r["TransactionStatus"].ToString();
            //        obj.TransactionTy = r["TransactionType"].ToString();
            //        obj.Transanction = r["TransactionStatus"].ToString();
            //        obj.CrAmount = r["CrAmount"].ToString();
            //        obj.DrAmount = r["DrAmount"].ToString();
            //        obj.Date = r["CheckDate"].ToString();   
            //        Crlst.Add(obj);
            //    }
            //    model.CrExpenseList = Crlst;
            //}

            return View(model);
        }
        public ActionResult UpdateExpenseCheckStaus(string CheqStatus, string ExpenseDetailsId, string CheckDate)
        {
            string FormName = "";
            string Controller = "";
            Expenses obj = new Expenses();
            try
            {
                obj.FK_ExpenseDetailsId = ExpenseDetailsId;
                obj.ChequeStatus = CheqStatus;
                obj.ChequeStatusUpdateDate = string.IsNullOrEmpty(CheckDate) ? null : Common.ConvertToSystemDate(CheckDate, "dd/MM/yyyy"); ;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateCheckStatus();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "Yes";
                        FormName = "ViewCrExpense";
                        Controller = "Expense";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ViewCrExpense";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
                FormName = "ViewCrExpense";
                Controller = "Expense";
            }
            //return RedirectToAction(FormName); 
            var uid = (ExpenseDetailsId);
            return RedirectToAction(FormName, new { fid = uid });
            // return RedirectToAction((FormName+"?id="+UserId).Trim(),Controller);
        }
        public ActionResult ViewDrExpense(Expenses model)
        {
            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            List<SelectListItem> ExpenseType = Common.ExpenseType();
            ViewBag.ExpenseType = ExpenseType;
            return View(model);
        }
        [HttpPost]
        [ActionName("ViewDrExpense")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult ViewDrExpens(Expenses model)
        {
            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            List<SelectListItem> ExpenseType = Common.ExpenseType();
            ViewBag.ExpenseType = ExpenseType;
            List<Expenses> Clearedlst = new List<Expenses>();
            List<Expenses> Pendinglst = new List<Expenses>();
            List<Expenses> Bouncelst = new List<Expenses>();
            model.ChequeStatus = "Dr";
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetDrExpenselist();
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();

                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    Clearedlst.Add(obj);
                }
                model.ClearedListItem = Clearedlst;
                ViewBag.TotalCleredPaid = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
            }
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow pr in ds.Tables[1].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.Pk_ExpenseDetailsId = pr["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = pr["ExpenseName"].ToString();
                    obj.ExpenseID = pr["ExpenseType"].ToString();
                    obj.Remarks = pr["Remarks"].ToString();
                    obj.ChequeStatus = pr["TransactionStatus"].ToString();
                    obj.TransactionTy = pr["TransactionType"].ToString();
                    obj.Transanction = pr["TransactionStatus"].ToString();
                    obj.ChequeNo = pr["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = pr["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = pr["CrAmount"].ToString();
                    obj.DrAmount = pr["DrAmount"].ToString();
                    obj.Date = pr["CheckDate"].ToString();
                    Pendinglst.Add(obj);
                }
                model.PendingListItem = Pendinglst;
                ViewBag.TotalPendingPaid = double.Parse(ds.Tables[1].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
            }
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    Bouncelst.Add(obj);
                }
                model.BounceListItem = Bouncelst;
                ViewBag.TotalBouncePaid = double.Parse(ds.Tables[2].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
            }

            return View(model);
        }
        public ActionResult ExpenseLedgerByName(string FId,string N)
        {
            Expenses model = new Expenses();
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.ExpenseName = Crypto.Decrypt(N);
            model.FK_ExpenseNameId = Crypto.Decrypt(FId);
            List<Expenses> Clearedlst = new List<Expenses>();
            DataSet ds = model.GetledgerByName();
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.BankName = r["TransactionType"].ToString();
                     obj.TransactionTy = r["EntryType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    Clearedlst.Add(obj);
                }
                model.ClearedListItem = Clearedlst;
                ViewBag.TotalDrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.TotalCrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult ExpenseLedger(string bankid)
        {
            Expenses model = new Expenses();
            if (bankid !="" || bankid!=null)
            {
                List<Expenses> lst = new List<Expenses>();
                model.TransactionID = Crypto.Decrypt(bankid);
                DataSet ds = model.GetExpenseLedger();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Expenses expense = new Expenses();
                        expense.ExpenseName = r["ExpenseName"].ToString();
                        expense.ExpenseID = r["ExpenseTypeName"].ToString();
                        expense.ChequeStatus = r["TransactionStatus"].ToString();
                        expense.TransactionTy = r["TransactionType"].ToString();
                        expense.Transanction = r["TransactionStatus"].ToString();
                        expense.CrAmount = r["CrAmount"].ToString();
                        expense.DrAmount = r["DrAmount"].ToString();
                        expense.Type = r["LedgerType"].ToString();
                        expense.BalanceAmount = r["Balance"].ToString();
                        expense.Remarks = r["Remarks"].ToString();
                        expense.Date = r["CheckDate"].ToString();
                        lst.Add(expense);
                    }
                    model.CrExpenseList = lst;
                }
            }
          
            #region ExpenseType
            int count = 0;
            Plot obj = new Plot();
            List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
            DataSet dsExpenseType = obj.GetExpenseTypeList();
            if (dsExpenseType != null && dsExpenseType.Tables.Count > 0 && dsExpenseType.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsExpenseType.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlExpenseType.Add(new SelectListItem { Text = "Select Expense Type", Value = "0" });
                    }
                    ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ExpenseType = ddlExpenseType;
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            int count1 = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            List<SelectListItem> EntryType = Common.EntryType();
            ViewBag.EntryType = EntryType;
            #endregion
            return View(model);
        }
        [HttpPost]
        [ActionName("ExpenseLedger")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult SearchExpenseLedger(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.ExpenseName = model.ExpenseName == "0" ? null : model.ExpenseName;
            model.Type = model.Type == "0" ? null : model.Type;
            model.TransactionID = model.TransactionID == "0" ? null : model.TransactionID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetExpenseLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseTypeName"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Type = r["LedgerType"].ToString();
                    obj.BalanceAmount = r["Balance"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    lst.Add(obj);
                }
                model.CrExpenseList = lst;
            }
            #region ExpenseType
            int count = 0;
            Plot obj1 = new Plot();
            List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
            DataSet dsExpenseType = obj1.GetExpenseTypeList();
            if (dsExpenseType != null && dsExpenseType.Tables.Count > 0 && dsExpenseType.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsExpenseType.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlExpenseType.Add(new SelectListItem { Text = "Select Expense Type", Value = "0" });
                    }
                    ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ExpenseType = ddlExpenseType;
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            int count1 = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            List<SelectListItem> EntryType = Common.EntryType();
            ViewBag.EntryType = EntryType;
            #endregion
            return View(model);
        }
        public ActionResult UpdateRemarks(string ExpenseDetailsId, string Remark)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses model = new Expenses();

                model.Pk_ExpenseDetailsId = ExpenseDetailsId;
                model.Remarks = Remark;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.updateRemarks();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["CrExpenseList"] = "Remarks Updated!";
                    }
                    else
                    {
                        TempData["CrExpenseList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["CrExpenseList"] = ex.Message;
            }
            FormName = "ViewCrExpense";
            Controller = "Expense";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult DeleteExpenseDetails(string ExpenseDetailsId)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses model = new Expenses();

                model.Pk_ExpenseDetailsId = ExpenseDetailsId;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteExpenseDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        
                        TempData["CrExpenseList"] = "Expense Details Deleted!";
                    }
                    else
                    {
                        TempData["CrExpenseList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["CrExpenseList"] = ex.Message;
            }
            FormName = "ViewCrExpense";
            Controller = "Expense";

            return RedirectToAction(FormName, Controller);
        }
    }

}

