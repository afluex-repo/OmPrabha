using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmPrabha.Models
{
    public class AssociateBooking : Common
    {
        #region Properties
        
        public string ProfileStatus { get; set; }
        public string AdharBackSideImage { get; set; }
        public string Abbreviation { get; set; }
        public string Fk_SponsorId { get; set; }
        public string ActiveStatus { get; set; }
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
        public string FarmerId { get; set; }
        public string ExpenseID { get; set; }
        //public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotAmount { get; set; }
        public string NetPlotAmount { get; set; }
        public string PK_PLCCharge { get; set; }
        public string PlotRate { get; set; }
        public string PaymentPlanID { get; set; }
        public string BookingAmount { get; set; }
        public string PayAmount { get; set; }
        public string Discount { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNumber { get; set; }
        public string JoiningFromDate { get; set; }
        public string JoiningToDate { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Remark { get; set; }
        public string TotalPLC { get; set; }
        public string LoginId { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public List<SelectListItem> lstAssociate { get; set; }
        public List<AssociateBooking> lstCustomer { get; set; }
        public List<SelectListItem> ddlSite { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public string BookingDate { get; set; }
        public string ActualPlotRate { get; set; }
        public string DevelopmentCharge { get; set; }
        public List<AssociateBooking> lstPlot { get; set; }
        public string BookingStatus { get; set; }
        public string EncryptKey { get; set; }
        public string ActionDateTime { get; set; }
        public string FormAction { get; set; }
        public string Remarks { get; set; }
        public string TransactionBy { get; set; }
        public string AdminId { get; set; }

        public string hdBookingNo { get; set; }
        public string PlotSize { get; set; }
        public string SiteName { get; set; }
        public string SectorName { get; set; }
        public string BlockName { get; set; }

        public string GeneratedAmount { get; set; }
        public string RemainingAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string ReceiptNo { get; set; }
        public string EncryptFarmerId { get; set; }
        public List<SelectListItem> ddlexpensename { get; set; }


        public List<AssociateBooking> ClosingWisePayoutlist { get; set; }
        public string CommPercentage { get; set; }
        public string PK_PaidPayoutId { get; set; }

        public string AssociateImage { get; set; }

        #endregion

        public DataSet GetExpenseNameList()
        {
            SqlParameter[] para = { new SqlParameter("@ExpenseID", 4) };
            DataSet ds = Connection.ExecuteQuery("GetExpenseNameList", para);
            return ds;
        }
        public DataSet GetFarmerList()
        {
            SqlParameter[] para = { new SqlParameter("@Name", Name) };
            DataSet ds = Connection.ExecuteQuery("GetFarmerListforAutoSearch",para);
            return ds;
        }
        public DataSet GetcustomerList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", CustomerLoginID) };
            DataSet ds = Connection.ExecuteQuery("GetCustomerlist", para);
            return ds;
        }
        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", AssociateLoginID) };
            DataSet ds = Connection.ExecuteQuery("GetAssociate", para);
            return ds;
        }




        public DataSet GetEmployeeList()
        {
            
            DataSet ds = Connection.ExecuteQuery("GetEmployeeListforAutoSearch");
            return ds;
        }
        public DataSet List()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                     new SqlParameter("@AssociateID", LoginId)   ,                               
                                  
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
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForAssociate", para);
            return ds;
        }

        public DataSet GetDownlineDetails()
        {
            SqlParameter[] para = { 
                                       
                                      new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetDownlineAssociateDetails", para);
            return ds;
        }



        public string DesignationName { get; set; }

        public string Percentage { get; set; }
 public string PK_RewardItemId { get; set; }
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
        public List<AssociateBooking> lstBlockList { get; set; }

        public string PK_BookingDetailsId { get; set; }

        public string InstallmentNo { get; set; }

        public string InstallmentDate { get; set; }

        public string DueAmount { get; set; }
        

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
            DataSet ds = Connection.ExecuteQuery("GetLedgerDetailsForAssociate", para);
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

        public DataSet GetDetails()
        {
            SqlParameter[] para = { 
                                        new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetUplineAssociateDetails", para);
            return ds;
        }
        public DataSet UpdatePassword()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OldPassword", Password) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy) 
                                  };
            DataSet ds = Connection.ExecuteQuery("ChangePasswordAssociate", para);
            return ds;
        }

        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public string Total { get; set; }

        public string Status { get; set; }
        
        public string TotalBooking { get; set; }

        public string Month { get; set; }
        public DataSet GetDetailsForBarGraph()
        {
            SqlParameter[] para = {
                                    
                                      new SqlParameter("@Fk_AssociateId", AssociateID) 
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForGraphOnAssociateDashboard", para);
            return ds;
        }
        public List<AssociateBooking> dataList3 { get; set; }
        public List<AssociateBooking> ListInstallment { get; set; }

        public DataSet GetDueInstallmentList()
        {
            SqlParameter[] para = {
                                    
                                      new SqlParameter("@Fk_AssociateId", AssociateID) 
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDueInstallmentForAssociateDashboard", para);
            return ds;
        }
        public DataSet BindGraphDetails()
        {
            SqlParameter[] para = {
                                    
                                      new SqlParameter("@LoginId", LoginId) 
                                  };
            DataSet ds = Connection.ExecuteQuery("PlotDetailsOnGraphForAssociateDashboard", para);
            return ds;
        }


        public string SponsorID { get; set; }

        public string SponsorName { get; set; }

        public string DesignationID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }
        

        public string Email { get; set; }

        public string PanNo { get; set; }

        
        public string Address { get; set; }
        public string ActionStatus { get; set; }
        public string NewsFor { get; set; }


        public string Nomani { get; set; }
        public string NomineeAge { get; set; }
        public string NomineeRelation { get; set; }



        //public string LoginID { get; set; }



        #region EditProfile


        public DataSet GetList()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@AssociateId", LoginId) 
                                  };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDetailsForEditProfile", para);
            return ds;
        }

        public DataSet GetListCustomer()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", UserID),
                                  new SqlParameter("@CustomerLoginID", CustomerLoginID),
                                  new SqlParameter("@CustomerName", CustomerName),
                                  new SqlParameter("@AssociateLoginID", AssociateLoginID),
                                  new SqlParameter("@AssociateName", AssociateName),
                                  new SqlParameter("@FromDate", JoiningFromDate),
                                  new SqlParameter("@ToDate", JoiningToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("SelectCustomerForAssociate", para);
            return ds;
        }

        public DataSet UpdatePersonalDetails()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@Email", Email) ,
                                       new SqlParameter("@PinCode", Pincode) ,
                                        new SqlParameter("@State", State) ,
                                         new SqlParameter("@City", City) ,
                                          new SqlParameter("@Address", Address) ,
                                           new SqlParameter("@PanNumber", PanNo) ,
                                            new SqlParameter("@UpdatedBy", UpdatedBy) ,
                                            new SqlParameter("@ProfilePic", ProfilePic)
                                  };
            DataSet ds = Connection.ExecuteQuery("EditAssociateDetailsForProfile", para);
            return ds;
        }



        #endregion


        public List<AssociateBooking> ListNEWS { get; set; }
        public DataSet GetNews()
        {
            SqlParameter[] para = {
                                    
                                      new SqlParameter("@PK_NewsID", PK_NewsID) ,
                                      new SqlParameter("@NewsFor", NewsFor)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetNewsList", para);
            return ds;
        }


        public string PK_NewsID { get; set; }
        public string NewsHeading { get; set; }
        public string NewsBody { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }


        public DataSet GetDownlineTree()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_UserId", Fk_UserId),
                                          new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDownlineTree", para);
            return ds;
        }

        public string Fk_RewardID { get; set; }
        public string RewardID { get; set; }

        public string QualifyDate { get; set; }
        public string Target { get; set; }
        public string RewardName { get; set; }
        public string RewardImage { get; set; }
        public DataSet RewardList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_RewardId", RewardID),
                                        new SqlParameter("@FK_UserId", UserID)};
            DataSet ds = Connection.ExecuteQuery("_GetRewardData", para);
            return ds;
        }

        public DataSet ClaimReward()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_RewardItemId", PK_RewardItemId),
                                        new SqlParameter("@FK_UserId", Fk_UserId),
                                        new SqlParameter("@Status", Status),
            };
            DataSet ds = Connection.ExecuteQuery("ClaimReward", para);
            return ds;
        }
        public string FromID { get; set; }
        public string FromName { get; set; }
        public string ToID { get; set; }
        public string ToName { get; set; }
        public string Amount { get; set; }
        public string Name { get; set; }
        public string EnquiryID { get; set; }
        public string Details { get; set; }
        public string DifferencePercentage { get; set; }
        public string Income { get; set; }
        public string PayoutWalletID { get;  set; }
        public string Narration { get;  set; }
        public string Credit { get;  set; }
        public string TType { get; set; }
        public string Debit { get;  set; }
        public string PayOutNo { get; set; }
        public string ClosingDate { get; set; }
        public string GrossAmount { get;  set; }
        public string TDS { get;  set; }
        public string Processing { get;  set; }
        public string NetAmount { get;  set; }
        public string RequestID { get;  set; }
        public string Action { get;  set; }
        public string AdharNumber { get;  set; }
        public string AdharImage { get;  set; }
        public string AdharStatus { get;  set; }
        public string PanNumber { get;  set; }
        public string PanImage { get;  set; }
        public string PanStatus { get;  set; }
        public string DocumentImage { get;  set; }
        public string DocumentNumber { get;  set; }
        public string DocumentStatus { get;  set; }
        public string PK_DocumentID { get;   set; }
        public string DocumentType { get;   set; }
        public string BankAccountNo { get;   set; }
        public string IFSCCode { get;   set; }

        public DataSet UnpaidIncomes()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId", UserID),
                 new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate", ToDate),
                new SqlParameter("@FromLoginId", FromID),
                new SqlParameter("@ToLoginId", ToID),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetUnpaidIncomes", para);
            return ds;
        }

        public DataSet PayoutLedger()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserID),
                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutLedgerAssociate", para);
            return ds;
        }

        public string DirectIncome { get; set; }
        public string DifferentialIncome { get; set; }
        public string DirectLeadershipIncome { get; set; }

        public DataSet PayoutDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_Userid", UserID),
                  new SqlParameter("@PayoutNo", PayOutNo),
                    new SqlParameter("@FromDate", FromDate),
                     new SqlParameter("@ToDate", ToDate),
                      new SqlParameter("@LoginId", LoginId),

                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutDetails", para);
            return ds;
        }
        public DataSet GetPayoutBalance()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserID),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }
        public DataSet SavePayoutRequest()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", UserID),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@AddedBy", AddedBy),
                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutRequest", para);
            return ds;
        }
        public DataSet PayoutRequestReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", UserID),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                     new SqlParameter("@Status", Status),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }

        public DataSet ApproveRequest()
        {
            SqlParameter[] para = { new SqlParameter("@PK_RequestID", RequestID),
                                    new SqlParameter("@ApprovedBy", AddedBy),
                                    
                                      };
            DataSet ds = Connection.ExecuteQuery("ApprovePayoutRequest", para);
            return ds;
        }
        public DataSet DeclineRequest()
        {
            SqlParameter[] para = { new SqlParameter("@PK_RequestID", RequestID),
                                    new SqlParameter("@ApprovedBy", AddedBy),

                                      };
            DataSet ds = Connection.ExecuteQuery("DeclinePayoutRequest", para);
            return ds;
        }
        public DataSet SaveDistributedPayment()
        {
            SqlParameter[] para = { new SqlParameter("@ClosingDate", ToDate),
                                     

                                      };
            DataSet ds = Connection.ExecuteQuery("_AutoDistributePayment", para);
            return ds;
        }
        public DataSet TransactionLogReportBy()
        {
            SqlParameter[] para = 
                {
                 new SqlParameter("@FromDate", FromDate),
                  new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@CustomerID", CustomerID),
                  new SqlParameter("@ExpenseID", ExpenseID),
                    new SqlParameter("@AssociateID", AssociateID),
                  new SqlParameter("@FarmerId", FarmerId)
         };
            DataSet ds = Connection.ExecuteQuery("TransactionReport", para);
            return ds;
        }
        #region kyc
        public DataSet GetKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID", UserID) };
            DataSet ds = Connection.ExecuteQuery("GetKYCDocuments", para);
            return ds;
        }

        public DataSet UploadKYCDocuments()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserID",UserID ),
                                      new SqlParameter("@AdharNumber", AdharNumber) ,
                                      new SqlParameter("@AdharImage", AdharImage) ,
                                      new SqlParameter("@AdharBackSideImage",AdharBackSideImage),
                                      new SqlParameter("@PanNumber", PanNumber),
                                      new SqlParameter("@PanImage", PanImage) ,
                                      new SqlParameter("@DocumentNumber", DocumentNumber) ,
                                      new SqlParameter("@DocumentImage", DocumentImage),
                                      new SqlParameter("@ProfilePic", ProfilePic)
                                  };
            DataSet ds = Connection.ExecuteQuery("UploadKYC", para);
            return ds;

        }


             public DataSet AssociateListForKYC()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Status", Status)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetAgentListForKYC", para);
            return ds;
        }

        public DataSet ApproveKYC()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId),
                                      new SqlParameter("@PK_DocumentID", PK_DocumentID),
                                      new SqlParameter("@DocumentType", DocumentType),
                                      new SqlParameter("@Status", Status),
                                      new SqlParameter("@UpdatedBy", AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("ApproveKYC", para);
            return ds;
        }

        #endregion

        public DataSet GetPayoutWiseIncomeDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_PaidPayoutId", PK_PaidPayoutId)

                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutWiseIncomeDetails", para);
            return ds;
        }



        public DataSet GetBranchList()
        {
            DataSet ds = Connection.ExecuteQuery("GetBranchList");
            return ds;
        }



        public DataSet GetDesignationList()
        {

            SqlParameter[] para = {

                                      new SqlParameter("@Percentage", Percentage)

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList", para);

            return ds;
        }


        public DataSet GetAssociateLists()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }


        public DataSet AssociateRegistration()
        {
            SqlParameter[] para = { new SqlParameter("@BranchID", 1) ,
                                  new SqlParameter("@SponsorID", UserID) ,
                                  new SqlParameter("@DesignationID", DesignationID) ,
                                  new SqlParameter("@RoleID", 2) ,
                                  new SqlParameter("@FirstName", FirstName) ,
                                  new SqlParameter("@LastName", LastName) ,
                                  new SqlParameter("@Contact", Contact) ,
                                  new SqlParameter("@Email", Email) ,
                                  new SqlParameter("@Pincode", Pincode) ,
                                  new SqlParameter("@State", State) ,
                                  new SqlParameter("@City", City) ,
                                  new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@PanNo", PanNo) ,
                                  new SqlParameter("@PanImage", PanImage) ,
                                  new SqlParameter("@AddedBy", AddedBy) ,
                                  new SqlParameter("@Password", Password) ,

                                  new SqlParameter("@AdharNumber", AdharNumber) ,
                                  new SqlParameter("@BankAccountNo", BankAccountNo) ,
                                  new SqlParameter("@BankName", BankName) ,
                                  new SqlParameter("@BankBranch", BankBranch) ,
                                  new SqlParameter("@IFSCCode", IFSCCode) ,

                                    new SqlParameter("@ProfilePic", ProfilePic) ,
                                      new SqlParameter("@Signature", Signature) ,

                                  };
            DataSet ds = Connection.ExecuteQuery("AssociateRegistrationTraditional", para);
            return ds;
        }
        public string Signature { get; set; }
        public string SponsorDesignationID { get; set; }
        public List<SelectListItem> ddlDesignation { get; set; }


        //public DataSet GetSponsorName()
        //{
        //    SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
        //    DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
        //    return ds;
        //}


        public DataSet GetSponsorName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }


        public DataSet CustomerRegistration()
        {
            SqlParameter[] para = { new SqlParameter("@BranchID", BranchID) ,
                                  new SqlParameter("@SponsorID", UserID) ,
                                  new SqlParameter("@RoleID",3) ,
                                  new SqlParameter("@FatherName", FatherName),
                                   new SqlParameter("@Nomani", Nomani) ,
                                   new SqlParameter("@NomineeAge", NomineeAge) ,
                                   new SqlParameter("@NomineeRelation", NomineeRelation) ,
                                    new SqlParameter("@FirstName", FirstName) ,
                                  new SqlParameter("@LastName", LastName) ,
                                  new SqlParameter("@Contact", Contact) ,
                                  new SqlParameter("@Email", Email) ,
                                  new SqlParameter("@Pincode", Pincode) ,
                                  new SqlParameter("@State", State) ,
                                  new SqlParameter("@City", City) ,
                                  new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@PanNo", PanNo) ,
                                  new SqlParameter("@AddedBy", AddedBy) ,
                                  new SqlParameter("@Password", Password),
                                  new SqlParameter("@Abbreviation",Abbreviation)
                                  };
            DataSet ds = Connection.ExecuteQuery("CustomerRegistration", para);
            return ds;
        }


        public DataSet GetAssociateListss()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }

        public DataSet GetSponsorForCustomerRegistraton()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }



        public DataSet GetSponsorNameFOrCustomer()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }



        public DataSet GettingUserProfile()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId)};
            DataSet ds = Connection.ExecuteQuery("GetUserProfile", para);
            return ds;
        }


        public DataSet GetAssociateListsssss()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }

        #region MyIncome

        public string PaidStatus { get; set; }
        public string Date { get; set; }
        public string IncomeType { get; set; }
        public List<AssociateBooking> lstDirectIncome { get; set; }

        public DataSet GetDirectIncomes()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", UserID) };
            DataSet ds = Connection.ExecuteQuery("GetDirectIncome", para);
            return ds;
        }


        public List<AssociateBooking> lstDifferentialIncome { get; set; }

        public DataSet GetDifferentialIncome()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", UserID) };
            DataSet ds = Connection.ExecuteQuery("GetDifferentialIncome", para);
            return ds;
        }


        public List<AssociateBooking> lstDirectLeadershipIncome { get; set; }

        public DataSet GetDirectLeadershipIncome()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", UserID) };
            DataSet ds = Connection.ExecuteQuery("GetDirectLeadershipIncome", para);
            return ds;
        }

        #endregion
    }

}



