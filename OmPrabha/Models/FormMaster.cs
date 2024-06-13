using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmPrabha.Models
{
    public class FormMaster
    {
        public string PK_FormId { get; set; }
        public string FK_FormTypeId { get; set; }
        public string FormType { get; set; }
        public string FormName{get;set;}
        public string CreatedBy { get; set; }
        public string Parameter { get; set; }
        public string AlertMessage { get; set; }

        [NotMapped]
        public List<FormMaster> ListFormMaster { get; set; }

        public DataSet GetFormMasterList()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", Parameter) };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);
            return ds;
        }

        public DataSet GetFormMasterById()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FormId", PK_FormId),
                                  new SqlParameter("@Parameter",Parameter)
                                  };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);
            return ds;
        }

        public DataSet UpdateFormMaster()
        {
            SqlParameter[] para ={new SqlParameter("@FK_FormTypeId",FK_FormTypeId),
                                     new SqlParameter("@PK_FormId",PK_FormId),
                                new SqlParameter("@FormName",FormName),
                                new SqlParameter("@Parameter",Parameter),
                                new SqlParameter("@UpdatedBy",CreatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);
            return ds;
        }
        
        public DataSet SaveFormMaster()
        {
            SqlParameter[] para ={
                                new SqlParameter("@FK_FormTypeId",FK_FormTypeId),
                                new SqlParameter("@FormName",FormName),
                                new SqlParameter("@Parameter",Parameter),
                                new SqlParameter("@CreatedBy",CreatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);
            return ds;
        }
        
        public DataSet DeleteFormMaster()
        {
            SqlParameter[] para ={
                                new SqlParameter("@PK_FormId",PK_FormId),
                                new SqlParameter("@Parameter",Parameter),
                                new SqlParameter("@DeletedBy",CreatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);
            return ds;
        }

        public DataSet GetFormTypeMaster()
        {
            SqlParameter[] para ={
                                
                                new SqlParameter("@Parameter",Parameter)
                                };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);
            return ds;
        }

    }
}