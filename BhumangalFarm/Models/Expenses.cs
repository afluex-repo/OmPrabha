using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace OmPrabha.Models
{
    public class Expenses : Common
    {
        #region Properties
      
        public string AcountHolder { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string Amount { get; set; }
        public string Date { get; set; }
        public string JoiningDate { get; set; }
        public string BranchName { get; set; }
        public string ExpenseID { get; set; }
        public string Remarks { get; set; }
        public List<SelectListItem> ddlexpensename { get; set; }
        public string Photo { get; set; }
        public string CrBalance { get; set; }
        public string DrBalance { get; set; }
        public string IsActive { get; set; }
        public string Pk_BankId { get; set; }
        public string Pk_ExpenseId { get; set; }
        public string ExpenseName { get; set; }
        public string Pk_ExpenseDetailsId { get; set; }
        public string DeletedBy { get; set; }
        public string EncryptKey { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Check { get; set; }
        public DataTable dtExpenseDetails { get; set; }
        public string OrderDateString { get; set; }
        public string TransactionID { get; set; }
        public string Type { get; set; }
        public string FK_ExpenseNameId { get; set; }
        public List<Expenses> AccountList { get; set; }
        public List<Expenses> ExpenseList { get; set; }
        public List<SelectListItem> lstBlock { get; set; }

        public List<ExpenseCR> CRExpenses { get; set; }
        public List<Expenses> BounceListItem { get; set; }
        public List<Expenses> PendingListItem { get; set; }
        public List<Expenses> ClearedListItem { get; set; }

        public List<Expenses> CrExpenseList { get; set; }
        public string ChequeStatus { get; set; }
        public string TransactionTy { get; set; }
        public string Transanction { get; set; }
        public string CrAmount { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeStatusUpdateDate { get; set; }
        public string FK_ExpenseDetailsId { get; set; }
        public string DrAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string PlotInfo { get; set; }

        #endregion
        public DataSet UpdateCheckStatus()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@FK_ExpenseDetailsId",FK_ExpenseDetailsId),
                                new SqlParameter("@ChequeDate",ChequeStatusUpdateDate),
                                new SqlParameter("@ChequeStaus",ChequeStatus),
                                new SqlParameter("@UpdatedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseCheckStatus", para);
            return ds;
        }
        public DataSet updateRemarks()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Pk_ExpenseDetailsId ",Pk_ExpenseDetailsId),
                                        new SqlParameter("@UpdatedBy ",UpdatedBy),
                                        new SqlParameter("@ExpenseRemarks ", Remarks)

                            };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseRemarks", para);
            return ds;
        }
        public DataSet DeleteExpenseDetails()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Pk_ExpenseDetailsId ",Pk_ExpenseDetailsId),
                                        new SqlParameter("@UpdatedBy ",UpdatedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("DeleteExpenseDetails", para);
            return ds;
        }
        public DataSet GetExpenseLedger()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ExpenseTypeId",ExpenseID),
                new SqlParameter("@Fk_ExpenseId",ExpenseName),
                new SqlParameter("@LedgerType",Type),
                new SqlParameter("@FK_BankId",TransactionID),
                  new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("ExpenseLedger", para);
            return ds;
        }
        public DataSet SaveAccountDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@AccountNumber", AccountNumber),
                                      new SqlParameter("@AcountHolder", AcountHolder),
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BranchName", BranchName),
                                       new SqlParameter("@IFSCCode", IFSCCode),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@Amount",Amount)
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveAccountDetails", para);
            return ds;
        }
        public DataSet UpdateAccountDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_BankId", Pk_BankId),
                                      new SqlParameter("@AccountNumber", AccountNumber),
                                      new SqlParameter("@AcountHolder", AcountHolder),
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BranchName", BranchName),
                                       new SqlParameter("@IFSCCode", IFSCCode),
                                      new SqlParameter("@UpdatedBy", UpdatedBy),
                                       new SqlParameter("@Amount",Amount)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateAccountDetails", para);
            return ds;
        }
        public DataSet GetAccountlist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AcountHolder",AcountHolder),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetAccountlist", para);
            return ds;
        }
        public DataSet DeleteAccount()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_BankId",Pk_BankId),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteAccount", para);
            return ds;
        }
        public DataSet AccountStatus()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_BankId",Pk_BankId),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@IsActive",IsActive)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateAccountStatus", para);
            return ds;
        }
        public DataSet GetAccountlistById()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_BankId",Pk_BankId),
            };
            DataSet ds = Connection.ExecuteQuery("GetAccountlistById", para);
            return ds;
        }
        public DataSet SaveExpenseDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@ExpenseName", ExpenseName),
                                      new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveExpenseDetails", para);
            return ds;
        }
        public DataSet SaveCrExpenseDetails()
        {
            SqlParameter[] para = { new SqlParameter("@AddedBy",AddedBy) ,
                 new SqlParameter("@ChequeStatus",ChequeStatus) ,
                                  new SqlParameter("@CrdtExpenseDetails",dtExpenseDetails)
            };
            DataSet ds = Connection.ExecuteQuery("SaveExpenseDetailsCr", para);
            return ds;
        }
        public DataSet SaveDrExpenseDetails()
        {
            SqlParameter[] para = { new SqlParameter("@AddedBy",AddedBy) ,
                                    new SqlParameter("@ChequeStatus",ChequeStatus) ,
                                  new SqlParameter("@CrdtExpenseDetails",dtExpenseDetails)
            };
            DataSet ds = Connection.ExecuteQuery("SaveExpenseDetailsDr", para);
            return ds;
        }
        public DataSet UpdateExpenseDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_ExpenseId", Pk_ExpenseId),
                                      new SqlParameter("@ExpenseName", ExpenseName),
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseDetails", para);
            return ds;
        }
        public DataSet GetExpenselist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ExpenseName",ExpenseName),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselist", para);
            return ds;
        }
        public DataSet DeleteExpense()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_ExpenseId",Pk_ExpenseId),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteExpense", para);
            return ds;
        }
        public DataSet ExpenseStatus()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_ExpenseId",Pk_ExpenseId),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@IsActive",IsActive)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseStatus", para);
            return ds;
        }
        public DataSet GetExpenselistById()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_ExpenseId",Pk_ExpenseId),
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselistById", para);
            return ds;
        }
        public DataSet GetExpenseNameList()
        {
            SqlParameter[] para = { new SqlParameter("@ExpenseID", ExpenseID) };
            DataSet ds = Connection.ExecuteQuery("GetExpenseNameList", para);
            return ds;
        }
        public DataSet GetTransactionList()
        {
            DataSet ds = Connection.ExecuteQuery("GetTransferAccountList");
            return ds;
        }

        public DataSet GetCrExpenselist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ExpenseType",ExpenseID),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@LedgerType",ChequeStatus),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselistByType", para);
            return ds;
        }
        public DataSet GetDrExpenselist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ExpenseType",ExpenseID),
                new SqlParameter("@FromDate",FromDate),
                  new SqlParameter("@LedgerType",ChequeStatus),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselistByType", para);
            return ds;
        }
        public DataSet GetledgerByName()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ExpenseId",FK_ExpenseNameId),
                new SqlParameter("@ExpenseName",ExpenseName)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenseLedgerByName", para);
            return ds;
        }
    }

    public class ExpenseCR
    {
        public string Amount { get; set; }
        public string Check { get; set; }
        public string Date { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseType { get; set; }
        public string PaymentMode { get; set; }
        public string Remarks { get; set; }
        public string TransactionID { get; set; }
    }

    
}

