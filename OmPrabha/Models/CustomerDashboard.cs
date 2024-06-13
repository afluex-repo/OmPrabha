using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace OmPrabha.Models
{
    public class CustomerDashboard: Common 
    {

        #region Properties
        public List<CustomerDashboard> dataList3 { get; set; }
        public List<CustomerDashboard> ListInstallment { get; set; }
        public string ProfilePic { get; set; }
        public string PK_BookingId { get; set; }
        public string UserID { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string PlotID { get; set; }
        public string PlotNumber { get; set; }
        public string CustomerID { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string AssociateID { get; set; }
        public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotAmount { get; set; }
        public string PK_PLCCharge { get; set; }
        public string NetPlotAmount { get; set; }
        public string PlotRate { get; set; }
        public string PaymentPlanID { get; set; }
        public string BookingAmount { get; set; }
        public string PayAmount { get; set; }
        public string Discount { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string DOB { get; set; }
        public string BankBranch { get; set; }
        public string Remark { get; set; }
        public string TotalPLC { get; set; }
        public string LoginId { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSC { get; set; }
        public string BookingDate { get; set; }
        public string ActualPlotRate { get; set; }
        public string DevelopmentCharge { get; set; }
        public string BookingStatus { get; set; }
        public string DesignationName { get; set; }
        public string Percentage { get; set; }
        public string Contact { get; set; }
        public string BookingNumber { get; set; }
        public string PaidAmount { get; set; }
        public string PlanName { get; set; }
        public string TotalAllotmentAmount { get; set; }
        public string PaidAllotmentAmount { get; set; }
        public string BalanceAllotmentAmount { get; set; }
        public string TotalInstallment { get; set; }
        public string InstallmentAmount { get; set; }
        public string PlotArea { get; set; }
        public string Balance { get; set; }
        public string PK_BookingDetailsId { get; set; }
        public string InstallmentNo { get; set; }
        public string InstallmentDate { get; set; }
        public string DueAmount { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Total { get; set; }
        public string Status { get; set; }
        public string TotalBooking { get; set; }
        public string Month { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
        public string DesignationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PanNo { get; set; }
        public string Address { get; set; }
        public string PK_NewsID { get; set; }
        public string NewsHeading { get; set; }
        public string NewsBody { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string NewsFor { get; set; }
        public List<CustomerDashboard> lstPlot { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public List<SelectListItem> ddlSite { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public List<CustomerDashboard> ListNEWS { get; set; }
        #endregion
        public DataSet GetDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("DetailsCustomerDashboard", para);
            return ds;
      
        }
        public string TotalPlotAmount { get; set; }
        public string TotalPaidAmount { get; set; }
        public string TotalPending { get; set; }
        public DataSet GetDueInstallmentList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_CustomerId", CustomerID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDueInstallmentForCustomerDashboard", para);
            return ds;
        }
        public DataSet GetNews()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_NewsID", PK_NewsID),
                                        new SqlParameter("@NewsFor", NewsFor)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetNewsList", para);
            return ds;
        }
        public DataSet BindGraphDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("PlotDetailsOnGraphForCustomerDashboard", para);
            return ds;
        }
        public DataSet GetDetailsForBarGraph()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_customerId", CustomerID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForGraphOnCustomerDashboard", para);
            return ds;
        }
        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }
        public DataSet List()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                     new SqlParameter("@CustomerID", LoginId)   ,
                                     new SqlParameter("@CustomerLoginID", CustomerLoginID)   ,
                                    new SqlParameter("@CustomerName", CustomerName)   ,
                                    new SqlParameter("@PK_SiteID", SiteID)   ,
                                    new SqlParameter("@PK_SectorID", SectorID)   ,
                                    new SqlParameter("@PK_BlockID", BlockID)   ,
                                    new SqlParameter("@PlotNumber", PlotNumber)   ,
                                    new SqlParameter("@BookingNumber", BookingNumber)   ,
                                    new SqlParameter("@FromDate", FromDate)   ,
                                    new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForCustomer", para);
            return ds;
        }
        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                        new SqlParameter("@CustomerID", CustomerID),
                                          new SqlParameter("@AssociateID", AssociateID)

                                  };

            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }
        public DataSet FillDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),
                                  new SqlParameter("@LoginId",LoginId),
                                   new SqlParameter("@FK_SiteID",SiteID),
                                    new SqlParameter("@FK_SectorID",SectorID),
                                     new SqlParameter("@FK_BlockID",BlockID),
                                      new SqlParameter("@PlotNumber",PlotNumber)


                            };
            DataSet ds = Connection.ExecuteQuery("GetLedgerDetailsForCustomer", para);
            return ds;
        }
        public DataSet UpdatePassword()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OldPassword", Password) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("ChangePasswordCustomer", para);
            return ds;
        }
        public DataSet GetList()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@CustomerID", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetCustomerDetailsForEditProfile", para);
            return ds;
        }
        public DataSet UpdatePersonalDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserID",UserID ) ,
                                      new SqlParameter("@FirstName", FirstName) ,
                                      new SqlParameter("@LastName", LastName) ,
                                      new SqlParameter("@Mobile", Contact) ,
                                      new SqlParameter("@Email", Email) ,
                                      new SqlParameter("@AccountHolderName", AccountHolderName),
                                      new SqlParameter("@AccountNo", AccountNumber) ,
                                      new SqlParameter("@BankName", BankName) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@IFSC", IFSC),
                                      new SqlParameter("@ProfilePic", ProfilePic),
                                      new SqlParameter("@Address", Address),
                                      new SqlParameter("@PanNumber", PanNo),
                                       new SqlParameter("@DOB", DOB)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateProfile", para);
            return ds;
        }
        public DataSet FillDueInstDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                   new SqlParameter("@FK_SiteID",SiteID),
                                   new SqlParameter("@FK_SectorID",SectorID),
                                   new SqlParameter("@FK_BlockID",BlockID),
                                   new SqlParameter("@PlotNumber",PlotNumber),

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForDueInstallment", para);
            return ds;
        }
        public DataSet GetBookingDetailsListForCustomer()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId),
                                      new SqlParameter("@CustomerID", CustomerID),
                                      new SqlParameter("@AssociateID", AssociateID),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber),
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingDetailsForCustomersss", para);
            return ds;
        }
    
    }
}