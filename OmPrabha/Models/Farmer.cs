using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace OmPrabha.Models
{
    public class Farmer : Common
    {
        #region Properties

        public string LandDhara { get; set; }
        public string KharijDakhil { get; set; }
        public string LandType { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Registry { get; set; }
        public string Agreement { get; set; }
        public string Notary { get; set; }

        public string FarmerName { get; set; }
        public string BranchID { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotNumber { get; set; }
        public string PlotID { get; set; }
        public string PlotSize { get; set; }
        public string RegistryId { get; set; }
        public string RegistryDate { get; set; }
        public string RemainingArea { get; set; }
        public string PlotArea { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string DOB { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string IDProof { get; set; }
        public string Hectare { get; set; }
        public string SqMtr { get; set; }
        public string IsActive { get; set; }
        public string PK_Farmerid { get; set; }
        public string EncryptKey { get; set; }
        public string FStatus { get; set; }
        public string DelearName { get; set; }
        public string AssociateID { get; set; }
        public string Area { get; set; }
        public string Acre { get; set; }
        public string SQFT { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Addrss { get; set; }
        public string JoiningDate { get; set; }
        public string GataKhasaraN { get; set; }
        public string TotalBalance { get; set; }
        public string CashDate { get; set; }
        public string PayType { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string Village { get; set; }
        public string Remarks { get; set; }
        public string TransactionDate { get; set;}
        public string Fk_BankId { get; set; }
        public string BankBranch { get; set; }
        public string Paymentdate { get; set; }
        public string Reciept { get; set; }
        public string ID { get; set; }
        public string TransactionNo { get; set; }
        public string CheqStatus { get; set; }
        public string PaidAmount { get; set; }
        public string GeneratedAmount { get; set; }
        public string RemainingAmount { get; set; }
        public List<Farmer> FarmerPlotList { get; set; }
        public List<Farmer> FarmerList { get; set; }
        public List<Farmer> PaymetListFarm { get; set; }
        public List<Farmer> BounceListItem { get; set; }
        public List<Farmer> PendingListItem { get; set; }
        public List<Farmer> ClearedListItem { get; set; }
        public DataTable dtPLCCharge { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public string Fk_PaymentId { get; set; }
        public List<SelectListItem> ddlRegistry { get; set; }
        public string IsRegistryByCompany { get; set; }


        #endregion
        public DataSet GetTransactionList()
        {
            DataSet ds = Connection.ExecuteQuery("GetTransferAccountList");
            return ds;
        }
        public DataSet SaveFarmerDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Title",Title),
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@Email", Email),
                                      new SqlParameter("@DOB", DOB),
                                      new SqlParameter("@SqMtr", SqMtr),
                                      new SqlParameter("@SQFT", SQFT),
                                      new SqlParameter("@Acre", Acre),
                                      new SqlParameter("@Pincode", Pincode),
                                      new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                      new SqlParameter("@Photo", Photo),
                                      new SqlParameter("@IDProof", IDProof),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@Address",Addrss),
                                      new SqlParameter("@AssociatId",AssociateID),
                                      new SqlParameter("@AssociatName", DelearName),
                                      new SqlParameter("@GataGhasaraN",GataKhasaraN),
                                      new SqlParameter("@Village",Village),
                                      new SqlParameter("@Registry",Registry),
                                      new SqlParameter("@Agreement",Agreement),
                                      new SqlParameter("@Notary",Notary),
                                      new SqlParameter("@Category",Category),
                                      new SqlParameter("@LandType",LandType),
                                      new SqlParameter("@KharijDakhil",KharijDakhil),
                                      new SqlParameter("@LandDhara",LandDhara)
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveFarmerDetails", para);
            return ds;
        }
        public DataSet GetFarmerpaymentList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Name",Name),
                 new SqlParameter("@FromDate",FromDate),
                  new SqlParameter("@ToDate",ToDate),
                  new SqlParameter("@CheckStatus",CheqStatus)
            };
            DataSet ds = Connection.ExecuteQuery("GetFarmerpaymentList", para);
            return ds;
        }
        public DataSet UpdateFarmerDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@Email", Email),
                                      new SqlParameter("@DOB", DOB),
                                      new SqlParameter("@SqMtr", SqMtr),
                                      new SqlParameter("@SQFT", SQFT),
                                      new SqlParameter("@Acre", Acre),
                                      new SqlParameter("@Pincode", Pincode),
                                      new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                      new SqlParameter("@Photo", Photo),
                                      new SqlParameter("@IDProof", IDProof),
                                      new SqlParameter("@DelearName", DelearName),
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@Pk_FarmerId",PK_Farmerid),
                                      new SqlParameter("@Address",Addrss),
                                      new SqlParameter("@AssociatId",AssociateID),
                                      new SqlParameter("@Title",Title),
                                      new SqlParameter("@GataGhasaraN",GataKhasaraN),
                                      new SqlParameter("@Village",Village),
                                      new SqlParameter("@Registry",Registry),
                                      new SqlParameter("@Agreement",Agreement),
                                      new SqlParameter("@Notary",Notary),
                                      new SqlParameter("@Category",Category),
                                      new SqlParameter("@LandType",LandType),
                                      new SqlParameter("@KharijDakhil",KharijDakhil),
                                      new SqlParameter("@LandDhara",LandDhara)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateFarmerListById", para);
            return ds;
        }
        public DataSet Getlist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Name",Name),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetFarmerList", para);
            return ds;
        }

        public DataSet GetlistById()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_FId",PK_Farmerid)
            };
            DataSet ds = Connection.ExecuteQuery("GetFarmerListById", para);
            return ds;
        }
        public DataSet GetPayFarmerlistById()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_FId",PK_Farmerid)
            };
            DataSet ds = Connection.ExecuteQuery("GetFarmerListById", para);
            return ds;
        }
       
        public DataSet GetSponsorName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", AssociateID) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }
        public DataSet FarmerStatus()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_Farmerid",PK_Farmerid),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@IsActive",IsActive)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateFarmerStatus", para);
            return ds;
        }

        public DataSet GetPaymentListByPkId()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Fk_UserId",PK_Farmerid)
                            };
            DataSet ds = Connection.ExecuteQuery("GetAddPaymentDetailsByID", para);
            return ds;
        }
        public DataSet GetFarmerPaymentByPaymentId()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Fk_PaymentId",Fk_PaymentId)
                            };
            DataSet ds = Connection.ExecuteQuery("GetFarmerPaymetListByPaymentId", para);
            return ds;
        }
        public DataSet GetPaymentDataByPkId()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@fk_Farmerid",PK_Farmerid)
                            };
            DataSet ds = Connection.ExecuteQuery("GetAddPaymentDetailsByIdDetails", para);
            return ds;
        }

        public DataSet GetPaymentModeList()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_paymentID",PaymentMode)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPaymentModeList", para);
            return ds;
        }

        public DataSet SavePayMentFarmerDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@TotalBalanc", TotalBalance),
                                      new SqlParameter("@PaidAmount",PaidAmount),
                                      //new SqlParameter("@PaidDate", CashDate),
                                      new SqlParameter("@Paytyp",PayType),
                                      new SqlParameter("@TransactionNo", TransactionNo),
                                      new SqlParameter("@TransactionDate", TransactionDate),
                                       new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BankBranch", BankBranch),
                                      new SqlParameter("@Remark", Remarks),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@Fk_Farmerid", PK_Farmerid),
                                      new SqlParameter("@Paymentdate",Paymentdate),
                                      new SqlParameter("@Fk_BankId",Fk_BankId)
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveFarmerPayMentDetails", para);
            return ds;
        }
        public DataSet UpdateFarmerPayment()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PaidAmount",PaidAmount),
                                      new SqlParameter("@Paytyp",PayType),
                                      new SqlParameter("@TransactionNo", TransactionNo),
                                      new SqlParameter("@TransactionDate", TransactionDate),
                                       new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BankBranch", BankBranch),
                                      new SqlParameter("@Remark", Remarks),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@Fk_PaymentId", Fk_PaymentId),
                                      new SqlParameter("@Paymentdate",Paymentdate),
                                      new SqlParameter("@Fk_BankId",Fk_BankId)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateFarmerPayment", para);
            return ds;
        }

        public DataSet GetPaymentListByIdDeta(string id, string res)
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Fk_UserId",id),
                                new SqlParameter("@Id",res)
                            };
            DataSet ds = Connection.ExecuteQuery("GetAddPaymentDetailsByIdDetails", para);
            return ds;
        }
        public DataSet UpdatePayMentFarmerDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@TotalBalanc", TotalBalance),
                                      new SqlParameter("@PaidAmount",Amount),
                                      new SqlParameter("@PaidDate", CashDate),
                                      new SqlParameter("@Paytyp",PayType),
                                      new SqlParameter("@ChequeN", ChequeNo),
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@Remark", Remarks),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@FK_UserId", PK_Farmerid),
                                      new SqlParameter("@Id",Reciept)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateFarmerPayMentDetails", para);
            return ds;
        }
        public DataSet DeletePayment()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Id",ID),
                                new SqlParameter("@DeletedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("DeletePayment", para);
            return ds;
        }
        public DataSet UpdateCheckStatus()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@ReferencdId",ID),
                                new SqlParameter("@ChequeDate",CashDate),
                                new SqlParameter("@Farmerid",Fk_UserId),
                                new SqlParameter("@ChequeStaus",CheqStatus),
                                new SqlParameter("@UpdatedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdateCheckStatus", para);
            return ds;
        }
        public DataSet FarmerListForPlotRegistry()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FarmerId", PK_Farmerid),

                                     };
            DataSet ds = Connection.ExecuteQuery("GetFarmerListForPltRegistry");
            return ds;
        }
        public DataSet CheckPlotNumberForPlotRegistry()
        {
            SqlParameter[] para = {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber)

                                     };
            DataSet ds = Connection.ExecuteQuery("CheckPlotNumberForPlotRegistry", para);
            return ds;
        }
        public DataSet FarmerListById()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FarmerId", PK_Farmerid),

                                     };
            DataSet ds = Connection.ExecuteQuery("GetFarmerListForPltRegistry",para);
            return ds;
        }
        public DataSet SaveFarmerPlotRegistry()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@CustomerId",CustomerId),
                                      new SqlParameter("@Fk_BranchId", BranchID),
                                      new SqlParameter("@Fk_PlotId", PlotID),
                                      new SqlParameter("@Fk_Siteid", SiteID),
                                      new SqlParameter("@Fk_SectorId", SectorID),
                                      new SqlParameter("@Fk_BlockId", BlockID),
                                      new SqlParameter("@Fk_FarmerId", PK_Farmerid),
                                      new SqlParameter("@FK_RegistryId",RegistryId),
                                      new SqlParameter("@GataKhasraNo", GataKhasaraN),
                                      new SqlParameter("@RegistryDate", RegistryDate),
                                      new SqlParameter("@RemainingArea", RemainingArea),
                                        new SqlParameter("@IsRegistryByCompany", IsRegistryByCompany),
                                      new SqlParameter("@AddedBy", AddedBy)

                                  };
            DataSet ds = Connection.ExecuteQuery("FarmerPlotRegistry", para);
            return ds;
        }
        public DataSet GetPlotRegistrylist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Name",Name),
                new SqlParameter("@CustomerName",CustomerName),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetPlotRegistryList", para);
            return ds;
        }
        public DataSet RegistryStatus()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_PlotRegistryid",PK_Farmerid),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@IsActive",IsActive)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateplotRegistryStatus", para);
            return ds;
        }

    }
}

