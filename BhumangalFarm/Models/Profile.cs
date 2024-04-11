using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OmPrabha.Models
{
    public class Profile:Common
    {
        public string DOB { get; set; }
        public string EncryptLoginID { get; set; }
        public string EncryptPayoutNo { get; set; }
        public string RealtionName { get; set; }
        public string LoginId { get; set; }
        public string ProductID { get; set; }
        public string Gender { get; set; }
        public string Relation { get; set; }
        public string PK_UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JoiningDate { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public List<Profile> profilelst { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSC { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public string LeadershipBonus { get; set; }
        public string AccountHolder { get; set; }
        public string PanNumber { get; set; }
        public string Address { get; set; }
        public string AdharStatus { get; set; }
        public string PanStatus { get; set; }

        public DataSet GetUserProfile()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("UserProfile", para);
            return ds;
        }

     
        public DataSet UpdateProfile()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserID",PK_UserID ) ,
                                      //new SqlParameter("@FirstName", FirstName) ,
                                      //new SqlParameter("@LastName", LastName) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Email", EmailId) ,
                                      new SqlParameter("@AccountHolderName", AccountHolderName),
                                      new SqlParameter("@AccountNo", AccountNumber) ,
                                      new SqlParameter("@BankName", BankName) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@IFSC", IFSC),
                                      new SqlParameter("@ProfilePic", ProfilePicture),
                                      new SqlParameter("@Address", Address),
                                      new SqlParameter("@PanNumber", PanNumber),
                                       new SqlParameter("@DOB", DOB)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateProfile", para);
            return ds;
        }
    }
}