using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace OmPrabha.Models
{
    public class Wallet:Common
    {
        public string EncryptPayoutNo { get; set; }
        public string EncryptLoginID { get; set; }
        public string PayoutBalance { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string Narration { get; set; }
        public string ClosingDate { get; set; }
        public string PayoutNo { get; set; }
        public string LastClosingDate { get; set; }
        public string LeadershipBonus { get; set; }
        public string NetIncome { get; set; }
        public string Processing { get; set; }
        public string TDS { get; set; }
        public string GrossIncome { get; set; }
        public string DirectIncome { get; set; }
        public string BinaryIncome { get; set; }
        public string FirstName { get; set; }
        public string Status { get; set; }
        public string Pk_RequestId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LoginId { get; set; }
        public string PaymentDate { get; set; }
        public string Amount { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionNo { get; set; }
        public List<Wallet> lstpayoutledger { get; set; }
        public List<Wallet> lstPayoutrequest { get; set; }
        public List<Wallet> lstassociate { get; set; }
        public List<AssociateBooking> lstdistribute { get; set; }
        public List<Wallet> lstPayoutDetail { get; set; }

        public string TopUpDate { get; set; }
        public string Description { get; set; }
        public string PaymentMode { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Name { get; set; }

        public string Package { get; set; }
        #region PaidPayout
        public DataSet GetPaidPayout()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId),
                                    new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate), };
            DataSet ds = Connection.ExecuteQuery("GetPaidPayoutDetails", para);
            return ds;
        }
        #endregion

        public DataSet GetPayoutRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),


                                     };
            DataSet ds = Connection.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }

        public DataSet ApprovePayputRequest()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@Pk_RequestId", Pk_RequestId),
                                         new SqlParameter("@Status", Status)
                                  };
            DataSet ds = Connection.ExecuteQuery("ApproveDeclinePayoutRequest", para);
            return ds;
        }

        public DataSet GetDitributePaymentList()
        {
            //SqlParameter[] para = { new SqlParameter("@LoginId", LoginID) };
            DataSet ds = Connection.ExecuteQuery("GetDetailsForDistributePayment");
            return ds;
        }
        public DataSet AutoDistributePayment()
        {
            SqlParameter[] para = { new SqlParameter("@ClosingDate", ClosingDate) };
            DataSet ds = Connection.ExecuteQuery("_AutoDistributePayment", para);
            return null;
        }
        public DataSet PayoutLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                       new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),
                                         new SqlParameter("@LoginId", LoginId),
                                     };
            DataSet ds = Connection.ExecuteQuery("GetPayoutLedger", para);
            return ds;
        }
        public DataSet GetPayoutReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PayoutNo", PayoutNo),

                                         new SqlParameter("@FromDate", FromDate),
                                         new SqlParameter("@ToDate", ToDate),

            };
            DataSet ds = Connection.ExecuteQuery("PayoutReportForMember", para);
            return ds;
        }

        public DataSet GetPackage()
        {
            DataSet ds = Connection.ExecuteQuery("GetPackage");
            return ds;
        }

        public DataSet TopUpIdByAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@Fk_ProductId", Package),
                                    new SqlParameter("@TopupDate", TopUpDate),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@PaymentMode", PaymentMode) ,
                                    new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                    new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                    new SqlParameter("@BankBranch", BankBranch) ,
                                    new SqlParameter("@BankName", BankName),
                                    new SqlParameter("@Description", Description)
                                 };
            DataSet ds = Connection.ExecuteQuery("TopUpByAdmin", para);
            return ds;
        }

        public DataSet GetUserName()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = Connection.ExecuteQuery("GetUserName", para);
            return ds;
        }
        public DataSet BindPriceByProduct()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_PackageId", Package)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetProductAmount", para);
            return ds;
        }
    }
}