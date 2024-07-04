using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace OmPrabha.Models
{
    public class TraditionalAssociate : Common
    {
        #region Properties
        
            
             public string ProfilePic { get; set; }
        public string Signature { get; set; }

        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }

        public string Password { get; set; }
        public string  JoiningFromDate { get; set; }
        public string  JoiningToDate { get; set; }
        public string  ActivateFromDate { get; set; }
        public string  ActivateToDate { get; set; }
        public string isBlocked { get; set; }
        public string  AssociateID { get; set; }
        public string AssociateName { get; set; }
        public string UserID { get; set; }
        public string LoginID { get; set; }
        public string BranchID { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
        public string SponsorDesignationID { get; set; }
        public string DesignationID { get; set; }
        public string RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PanNo { get; set; }
        public string PanImage { get; set; }
        public string BranchName { get; set; }
        public string Pin { get; set; }
        public string DesignationName { get; set; }
        public string OldDesignation { get; set; }
        public string Remark { get; set; }
        public string PK_DesignationID { get; set; }
        public List<TraditionalAssociate> lstTrad { get; set; }
        public List<SelectListItem> ddlDesignation { get; set; }        
        #endregion

        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }

        #region AssociateRegistrationTraditional

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
            DataSet ds = Connection.ExecuteQuery("GetDesignationList",para);

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
        
        public DataSet GetList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", UserID),
                                  new SqlParameter("@AssociateLoginID", AssociateID),
                                  new SqlParameter("@AssociateName", AssociateName),
                                  new SqlParameter("@SponsorLoginID", SponsorID),
                                  new SqlParameter("@SponsorName", SponsorName),
                                  new SqlParameter("@FromDate", JoiningFromDate),
                                  new SqlParameter("@ToDate", JoiningToDate),
                                   new SqlParameter("@Status", Status),
                                   new SqlParameter("@Fk_DesignationId",FK_DesignationId)
                                  };
            DataSet ds = Connection.ExecuteQuery("SelectAssociate", para);
            return ds;
        }
        
        public DataSet UpdateAssociate()
        {
            SqlParameter[] para = { 
                                  new SqlParameter("@PK_UserId", UserID) ,
                                  new SqlParameter("@FirstName", FirstName) ,
                                  new SqlParameter("@LastName", LastName) ,
                                  new SqlParameter("@Contact", Contact) ,
                                  new SqlParameter("@Email", Email) ,
                                  new SqlParameter("@PinCode", Pincode) ,
                                  new SqlParameter("@State", State) ,
                                  new SqlParameter("@City", City) ,
                                  new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@PanNo", PanNo) ,
                                  new SqlParameter("@UpdatedBy", UpdatedBy) ,
                                   new SqlParameter("@AdharNumber", AdharNumber) ,
                                  new SqlParameter("@BankAccountNo", BankAccountNo) ,
                                  new SqlParameter("@BankName", BankName) ,
                                  new SqlParameter("@BankBranch", BankBranch) ,
                                  new SqlParameter("@IFSCCode", IFSCCode) ,
                                   new SqlParameter("@ProfilePic", ProfilePic) ,
                                      new SqlParameter("@Signature", Signature) ,
                                       new SqlParameter("@DesignationID", DesignationID) ,

                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateAssociateTraditional", para);
            return ds;
        }
        
        public DataSet DeleteAssociate()
        {
            SqlParameter[] para = { 
                                       new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteAssociate", para);
            return ds;
        }
        
        #endregion

        #region AssociateUpline
        
        public DataSet GetDetails()
        {
            SqlParameter[] para = { 
                                        new SqlParameter("@LoginId", AssociateID) };
            DataSet ds = Connection.ExecuteQuery("GetUplineAssociateDetails", para);
            return ds;
        }
        
        #endregion
        
        #region Associatedownline

        public DataSet GetDownlineDetails()
        {
            SqlParameter[] para = { 
                                       
                                      new SqlParameter("@LoginId", AssociateID) };
            DataSet ds = Connection.ExecuteQuery("GetDownlineAssociateDetails", para);
            return ds;
        }
        public string Percentage { get; set; }
        #endregion

        public DataSet BlockUser()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID),
                                      new SqlParameter("@FK_UserID", Fk_UserId),
                                   new SqlParameter("@BlockedBy", UpdatedBy)};

            DataSet ds = Connection.ExecuteQuery("BlockAssociate", para);
            return ds;
        }

        public DataSet UnblockUser()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID),
                                      new SqlParameter("@FK_UserID", Fk_UserId),
                                   new SqlParameter("@BlockedBy", UpdatedBy)};
            DataSet ds = Connection.ExecuteQuery("UnblockAssociate", para);
            return ds;
        }



        public string EncryptKey { get; set; }

        public string Body { get; set; }

        public string Char { get; set; }
        public string Sms { get; set; }

       
       

        #region claim report
        public DataSet ClaimRewardReport()
        {
            SqlParameter[] para = { new SqlParameter("@PK_RewardAchieverId", RewardAchieverID),
                                      };
            DataSet ds = Connection.ExecuteQuery("ClaimRewardReport", para);
            return ds;
        }
        public string RewardAchieverID { get; set; }
        public string RewardName { get; set; }
        public string Status { get; set; }
        public string FromID { get;  set; }
        public string FromName { get;  set; }
        public string ToID { get;  set; }
        public string ToName { get;  set; }
        public string Amount { get;  set; }
        public string DifferencePercentage { get;  set; }
        public string Income { get;  set; }
        public string AdharNumber { get;   set; }
        public string BankAccountNo { get;  set; }
        public string BankName { get;  set; }
        public string BankBranch { get;  set; }
        public string IFSCCode { get;  set; }
        public string PlotDetails { get; set; }
        #endregion
        public DataSet UnpaidIncomes()
        {
            SqlParameter[] para = {
                new SqlParameter("@FK_UserId", UserID),
                new SqlParameter("@FromDate", JoiningFromDate),
                new SqlParameter("@ToDate", JoiningToDate),
                new SqlParameter("@FromLoginId", FromID),
                new SqlParameter("@ToLoginId", ToID), 

                                      };
            DataSet ds = Connection.ExecuteQuery("GetUnpaidIncomes", para);
            return ds;
        }
        public DataSet GetUnPaidForDistribute()
        {
            SqlParameter[] para = {
                new SqlParameter("@ToLoginId", ToID),

                                      };
            DataSet ds = Connection.ExecuteQuery("GetUnPaidForDistribute", para);
            return ds;
        }

        public DataSet GetRankList()
        {
            SqlParameter[] para = {
                             new SqlParameter("@PK_UserId", UserID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetRankList", para);
            return ds;
        }

        public DataSet UpdateAssociateRank()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@FK_UserId", UserID) ,
                                   new SqlParameter("@Fk_OldDesignationId", PK_DesignationID),
                                  new SqlParameter("@Fk_NewDesignationId", DesignationID),
                                  //new SqlParameter("@Remark", Remark),
                                  new SqlParameter("@UpdatedBy", UpdatedBy) 
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateAssociateRank", para);
            return ds;
        }
    }
}