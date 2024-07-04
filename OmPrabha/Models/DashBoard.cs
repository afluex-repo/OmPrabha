using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OmPrabha.Models
{
    public class DashBoard : Common
    {
        public string Total { get; set; }

        public string Status { get; set; }
        public string Fk_UserId { get; set; }

        public DataSet GetDashBoardDetails()
        {

            DataSet ds = Connection.ExecuteQuery("GetDashBoardDetails");
            return ds;
        }

        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId",Fk_UserId ) ,

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }

        public string LoginId { get; set; }




        public List<DashBoard> lstmessages { get; set; }

        public string Pk_MessageId { get; set; }

        public string MessageTitle { get; set; }

        public string Message { get; set; }

        public DataSet GetAllMessages()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId) };
            DataSet ds = Connection.ExecuteQuery("GetMessages", para);
            return ds;
        }


        public string cssclass { get; set; }

        public string MessageBy { get; set; }

        public DataSet SaveMessage()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Message", Message),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@MessageBy", MessageBy),
                                      new SqlParameter("@Fk_UserId", Fk_UserId)
                                  };
            DataSet ds = Connection.ExecuteQuery("InsertMessage", para);
            return ds;
        }

        public string ProfilePic { get; set; }

        public string MemberName { get; set; }

        public List<DashBoard> lstinvestment { get; set; }

        public string Amount { get; set; }

        public string ProductName { get; set; }

        public string Month { get; set; }


        #region admin dashboard




        public DataSet GetDetails()
        {
            DataSet ds = Connection.ExecuteQuery("BindDataForDashboard");
            return ds;
        }

        public string Associates { get; set; }

        public string Customers { get; set; }

        public string Plots { get; set; }

        public string TotalBusiness { get; set; }



        public DataSet GetBookingDetailsList()
        {
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForDashboard");
            return ds;
        }
        public List<DashBoard> List { get; set; }
        #endregion

        public string PK_BookingId { get; set; }

        public string BranchID { get; set; }

        public string CustomerID { get; set; }

        public string BranchName { get; set; }

        public string CustomerLoginID { get; set; }

        public string CustomerName { get; set; }

        public string AssociateID { get; set; }

        public string AssociateLoginID { get; set; }

        public string AssociateName { get; set; }

        public string PlotNumber { get; set; }

        public string BookingDate { get; set; }

        public string BookingAmount { get; set; }

        public string PaymentPlanID { get; set; }

        public string BookingStatus { get; set; }

        public string PlotRate { get; set; }

        public DataSet GetAssociateDetails()
        {
            DataSet ds = Connection.ExecuteQuery("GetAssociateForDashboard");
            return ds;
        }
        public List<DashBoard> ListAssociate { get; set; }

        public string JoiningDate { get; set; }

        public string FK_DesignationID { get; set; }

        public string DesignationName { get; set; }
        public List<DashBoard> ListInstallment { get; set; }
        public DataSet GetDueInstallmentList()
        {
            DataSet ds = Connection.ExecuteQuery("GetDueInstallmentForDashboard");
            return ds;
        }


        public string InstallmentNo { get; set; }

        public string InstallmentAmount { get; set; }




        public List<DashBoard> dataList { get; set; }



        public DataSet BindGraphDetails()
        {
            DataSet ds = Connection.ExecuteQuery("PlotDetailsOnGraphForDashboard");
            return ds;
        }
        public List<DashBoard> dataList2 { get; set; }


        public DataSet BindGraphDetailsAssociate()
        {
            DataSet ds = Connection.ExecuteQuery("AssociateStatusDetailsOnGraphForDashboard");
            return ds;
        }
        public List<DashBoard> dataList3 { get; set; }
        public DataSet GetAssociateJoining()
        {
            DataSet ds = Connection.ExecuteQuery("GetAssociateJoiningForDashboard");
            return ds;
        }


        public string TotalUser { get; set; }
        public string Fk_ParentId { get; set; }
        public string IntallmentDate { get; set; }
        public List<DashBoard> lsttree { get; set; }

        public DataSet GetTreeDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@RootAgentCode", LoginId),
                                      new SqlParameter("@AgentCode", LoginId),
                                  };
            DataSet ds = Connection.ExecuteQuery("BrokerTree", para);
            return ds;
        }
    }
}