using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmPrabha.Models
{
    public class Reports : Common
    {
        
        public string ErrorMessage { get; set; }
        public string Downline { get; set; }
        public string Direct { get; set; }
        public int hdRows1 { get; set; }
        public string ReturnBenefitStartDate { get; set; }
        public string SponsorName { get; set; }
        public string SponsorId { get; set; }
        public string Ispaid { get; set; }
        public string InstAmt { get; set; }
        public string DueDate { get; set; }
        public string ExpenseID { get; set; }
        public string ExType { get; set; }
        public string Remarks { get; set; }
        public string ExpenseName { get; set; }
        public string Transaction { get; set; }
        public List<Reports> lstDayBook { get; set; }
        public string TransactionNo { get; set; }
        public string BankBranch { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string SiteName { get; set; }
        public string BlockName { get; set; }
        public string PlotID { get; set; }
        public string PlotStatus { get; set; }
        public string PlotSize { get; set; }
        public string SectorName { get; set; }
        public string AssociateName { get; set; }
        public string AssociateID { get; set; }
        public string CustomerId { get; set; }
        public string Customername { get; set; }
        public string CustomerLoginID { get; set; }
        public string ColorCSS { get; set; }
        public List<Reports> lstPlot { get; set; }
        public string BlockID { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public int Id { get; set; }
        public List<Reports> lstLuckyDraw { get; set; }
        public string Month { get; set; }
        public string ReceiptNo { get; set; }
        public string FK_InvestmentID { get; set; }
        public string UpgradtionDate { get; set; }
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string PlotNumber { get; set; }
        public List<Reports> lsttopupreport { get; set; }

        public string LeadershipBonus { get; set; }
        public string NetAmount { get; set; }
        public string ClosingDate { get; set; }
        public bool IsDownline { get; set; }
        public bool IsDirect { get; set; }
        public string Password { get; set; }
        public List<Reports> lstassociate { get; set; }
        public string PK_BookingId { get; set; }
        public string BookingNumber { get; set; }
        public List<Reports> lstP { get; set; }
        public string PlotAmount { get; set; }
        public string ActualPlotRate { get; set; }
        public string PlotRate { get; set; }
        public string PayAmount { get; set; }
        public string BookingDate { get; set; }
        public string BookingAmount { get; set; }


        public string Discount { get; set; }
        public string PaymentPlanID { get; set; }
        public string PlanName { get; set; }
        public string TotalAllotmentAmount { get; set; }
        public string PaidAllotmentAmount { get; set; }
        public string BalanceAllotmentAmount { get; set; }
        public string TotalInstallment { get; set; }

        public string Balance { get; set; }
        public string PlotArea { get; set; }
        public string PK_BookingDetailsId { get; set; }
        public string InstallmentNo { get; set; }
        public string InstallmentDate { get; set; }
        public string PaymentDate { get; set; }
        public string PK_paymentID { get; set; }
        public string PaidAmount { get; set; }
        public string InstallmentAmount { get; set; }
        public string PaymentMode { get; set; }
        public string DueAmount { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Name { get; set; }
        public string Leg { get; set; }
        public string Status { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string Mobile { get; set; }
        public string JoiningDate { get; set; }
        public string Package { get; set; }
        public string PermanentDate { get; set; }
        public string Email { get; set; }
        public string LoginIDD { get; set; }
        public string UserName { get; set; }
        public string NFromDate { get; set; }
        public string NToDate { get; set; }
        public string PaidEMI { get; set; }
        public string PaymentStatus { get; set; }
        public string PlotInfo { get; set; }
        public string CustomerInfo { get; set; }
        public string AssociateInfo { get; set; }
        public string TotalPaid { get; set; }
        public string RemainingAmount { get; set; }
        public int MaxJsonLength { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
        public List<Reports> EMIBookingReports { get; set; }
        public List<Reports> lstkharijdakhil { get; set; }
        public List<Reports> lstAssSelfdownBusinessReport { get; set; }
        public List<Reports> lstAutoUpdateDesignation { get; set; }



        public string Pk_KharijId { get; set; }
        public string IsKharijDakhilDone { get; set; }


        public string SelfBusiness { get; set; }
        public string TeamBusiness { get; set; }
        public string KharijDakhilDate { get; set; }
        public string KharijDakhilRemarks { get; set; }
        public string IFSCCode { get; set; }
        public string MemberAccNo { get; set; }
        public string Percentage { get; set; }
        public string DesignationID { get; set; }
        public string OldFk_DesignationId { get; set; }
        public string OldDesignationName { get; set; }
        public string OldBusiness { get; set; }
        public string NewFk_DesignationId { get; set; }
        public string NewDesignationName { get; set; }
        public string NewBusiness { get; set; }
        public string DesignationUpgradeDate { get; set; }

        public string OldDesignationPercentage { get; set; }
        public string NewDesignationPercentage { get; set; }

        public string BV { get; set; }
        public string TopupBy { get; set; }
        public string Pk_InvestmentId { get; set; }
        public List<Reports> TopupList { get; set; }
        public string Fk_PackageId { get; set; }

        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId) };
            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }
        public DataSet GetSiteList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("SiteList", para);
            return ds;
        }
        public DataSet GetSalaryInstallment()
        {

            SqlParameter[] para = {
                                      new SqlParameter("@UserID", Fk_UserId),
                                       new SqlParameter("@InstallmentNo", InstallmentNo),
                                        new SqlParameter("@LoginId", LoginId),
                                     };
            DataSet ds = Connection.ExecuteQuery("GetDataForBenefit", para);
            return ds;
        }

        public DataSet GetDataForBenefitDetails()
        {

            SqlParameter[] para = {
                                      new SqlParameter("@UserID", Fk_UserId),
                                       new SqlParameter("@InstallmentNo", InstallmentNo),
                                        new SqlParameter("@LoginId", LoginId),
                                     };
            DataSet ds = Connection.ExecuteQuery("GetDataForBenefitDetails", para);
            return ds;
        }

        public DataSet SaveBenefit()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PaymentDate", PaymentDate),
                                      new SqlParameter("@InstallmentNo", InstallmentNo),
                                       new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@TransactionNo", TransactionNo),
                                    new SqlParameter("@TransactionDate", TransactionDate),
                                    new SqlParameter("@BankName", BankName),
                                    new SqlParameter("@BankBranch", BankBranch)};


            DataSet ds = Connection.ExecuteQuery("PayBenefit", para);
            return ds;
        }
        public DataSet ReturnBenefit()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@FirstName", DisplayName),
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Status", Ispaid),
                                     };
            DataSet ds = Connection.ExecuteQuery("ReturnBenefitData", para);
            return ds;
        }
        public DataSet ReturnBenefitAssociate()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@FirstName", DisplayName),
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Status", Ispaid),
                                     };
            DataSet ds = Connection.ExecuteQuery("ReturnBenefitAssociate", para);
            return ds;
        }

        public DataSet GetPaymentMode()
        {

            DataSet ds = Connection.ExecuteQuery("GetPaymentModeList");
            return ds;
        }
        public DataSet ReturnBenefitView()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@FirstName", DisplayName),
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Status", Ispaid),
                                     };
            DataSet ds = Connection.ExecuteQuery("ReturnBenefitReport", para);
            return ds;
        }

        public DataSet GetBlockList()
        {
            SqlParameter[] para ={ new SqlParameter("@SiteID",SiteID),
                                     new SqlParameter("@SectorID",SectorID),
                                     new SqlParameter("@BlockID",BlockID),
                                 };
            DataSet ds = Connection.ExecuteQuery("GetBlockList", para);
            return ds;
        }
        public DataSet GetDetails()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber)

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotAvailabilityStatus", para);
            return ds;
        }
        public DataSet GetSectorList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetSectorList", para);
            return ds;
        }

        public DataSet GetDownlineList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                     new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status), };
            DataSet ds = Connection.ExecuteQuery("DownlineList", para);
            return ds;
        }
        public DataSet LuckyDrawList()
        {
            DataSet ds = Connection.ExecuteQuery("LuckyDrawListForAdmin");
            return ds;
        }


        public DataSet GetDirectList()
        {

            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status),
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDirectList", para);
            return ds;
        }

        public DataSet BusinessReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),

                                     new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@IsDownline", IsDownline),
                                    new SqlParameter("@IsDirect", IsDirect)

            };
            DataSet ds = Connection.ExecuteQuery("GetBusiness", para);

            return ds;
        }

        public DataSet GetTopupReport()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginID", LoginId),
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@Package", Package),
                                      new SqlParameter("@ClaculationStatus", Status),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetTopupreport", para);
            return ds;
        }
        public DataSet PayReport()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@UserID", Fk_UserId),
                                          new SqlParameter("@LoginID", LoginId),
                                            new SqlParameter("@Name", DisplayName),
                                             new SqlParameter("@FromDate", FromDate),
                                              new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = Connection.ExecuteQuery("PayReport", para);
            return ds;
        }
        public DataSet GetAssociateList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@UserID",Fk_UserId),
                new SqlParameter("@LoginID",LoginId),
                new SqlParameter("@ToDate",ToDate),
            };
            DataSet ds = Connection.ExecuteQuery("GetAssociateListForApprove", para);
            return ds;
        }

        public DataSet ApprovedAssociate()
        {

            SqlParameter[] para = {

                                      new SqlParameter("@AssociateID", AssociateID),
                                        new SqlParameter("@ApprovedBy", AddedBy),
                                        new SqlParameter("LoginId",LoginId),
                                     };
            DataSet ds = Connection.ExecuteQuery("ApprovedAssociate", para);
            return ds;
        }
        public DataSet RejectAssociate()
        {

            SqlParameter[] para = {

                                      new SqlParameter("@AssociateID", AssociateID),
                                        new SqlParameter("@RejectedBy", AddedBy),
                                        new SqlParameter("LoginId",LoginId),
                                     };
            DataSet ds = Connection.ExecuteQuery("RejectAssociate", para);
            return ds;
        }
        public DataSet DayBookList1()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ExpenseId",ExpenseID),
                new SqlParameter("@FromDate",NFromDate),
                new SqlParameter("@ToDate",NToDate)
            };
            DataSet ds = Connection.ExecuteQuery("DayBookList", para);
            return ds;
        }
        public DataSet DayBookList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ExpenseId",ExpenseID),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("DayBookList", para);
            return ds;
        }
        public DataSet GettingUserProfile()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId)};
            DataSet ds = Connection.ExecuteQuery("GetUserProfile", para);
            return ds;
        }
        public DataSet GetCustomerListAutoSeach()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId)};
            DataSet ds = Connection.ExecuteQuery("GetCustomerListforAutoSearch", para);
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

        public DataSet GetEMIBookingReportsDetails()
        {
            SqlParameter[] para =
                            {
                                //new SqlParameter("@PK_BookingDetailsId",PK_BookingDetailsId),
                                 new SqlParameter("@FromDate",FromDate),
                                  new SqlParameter("@ToDate",ToDate),
                                   new SqlParameter("@PayMentMode",PK_paymentID)
                            };
            DataSet ds = Connection.ExecuteQuery("GetEMIBookingReportsDetails", para);
            return ds;
        }




        public DataSet GetKharijDakhilList()
        {
            SqlParameter[] para =
                         {
                 new SqlParameter("@CustomerId",CustomerId),
                     new SqlParameter("@Pk_KharijId",Pk_KharijId),
                                 new SqlParameter("@FromDate",FromDate),
                                  new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetKharijDakhilList", para);
            return ds;
        }




        public DataSet DeleteIsKharijDakhil()
        {
            SqlParameter[] para =
                         {
                                 new SqlParameter("@Pk_KharijId",Pk_KharijId),
                                  new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("DeleteIsKharijDakhil", para);
            return ds;
        }


        #region Income

        public string ToUserName { get; set; }
        public string ToLoginId { get; set; }
        public string FromUserName { get; set; }
        public string Income { get; set; }
        public string IncomeType { get; set; }
        public string FromLoginId { get; set; }
        public string Date { get; set; }
        public List<Reports> lstDirectIncome { get; set; }
        public List<Reports> lstDifferentialIncome { get; set; }
        public List<Reports> lstDirectLeadershipIncome { get; set; }


        public DataSet GetDirectIncome()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@FromDate", FromDate),
                  new SqlParameter("@ToDate", ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetDirectIncome", para);
            return ds;
        }
        public DataSet GetDifferentialIncome()
        {
            SqlParameter[] para = {
               new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@FromDate", FromDate),
                  new SqlParameter("@ToDate", ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetDifferentialIncome", para);
            return ds;
        }
        public DataSet GetDirectLeadershipIncome()
        {
            SqlParameter[] para = {
              new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@FromDate", FromDate),
                  new SqlParameter("@ToDate", ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetDirectLeadershipIncome", para);
            return ds;
        }


        #endregion

        #region PayPayout

        public string BankHolderName { get; set; }
        public DataSet GetPayPayout()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@IsDownline", Downline),
                                       new SqlParameter("@IsDirect", Direct)
            };
            DataSet ds = Connection.ExecuteQuery("GetBalancePayoutforPayment", para);
            return ds;
        }
        public DataSet SavePayPayout()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId),
                                    new SqlParameter("@TransactionNo", TransactionNo),
                                    new SqlParameter("@TransactionDate", TransactionDate),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@AddedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("PayPayout", para);
            return ds;
        }
        #endregion


        public DataSet GetAssociateSelfdownBusinessReport()
        {
            SqlParameter[] para =
                         {
                                 new SqlParameter("@LoginId",AssociateID),
                                 new SqlParameter("@FromDate",FromDate),
                                 new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetSelftDownReport", para);
            return ds;
        }

        

        public DataSet GetDesignationList()
        {
            DataSet ds = Connection.ExecuteQuery("GetDesignationListNew");
            return ds;
        }


        public DataSet GetAutoUpdateDesignation()
        {
            SqlParameter[] para =
                         {
                                 new SqlParameter("@LoginId",LoginId),
                                 new SqlParameter("@NewFk_Designationid",DesignationID),
                                 new SqlParameter("@FromDate",FromDate),
                                 new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetAutoUpdateDesignation", para);
            return ds;
        }

        public DataSet GetTopupreport()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@LoginID",LoginId),
                new SqlParameter("@Package",Package),
                new SqlParameter("@ClaculationStatus",Status),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetTopupDetails", para);
            return ds;
        }

        public DataSet GetPackage()
        {
            DataSet ds = Connection.ExecuteQuery("GetPackage");
            return ds;
        }
    }
}


