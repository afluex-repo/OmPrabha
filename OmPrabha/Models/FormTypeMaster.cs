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
    public class FormTypeMaster
    {
        public string PK_FormTypeId { get; set; }
        public string FormType { get; set; }
        public string Parameter { get; set; }
        public string AlertMessage { get; set; }
        public string CreatedBy { get; set; }

        public string FormTypeSearch { get; set; }

        public List<FormTypeMaster> ListFormType { get; set; }

        public DataSet GetFormTypeList()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", Parameter) };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);
            return ds;
        }

        public DataSet GetFormTypeById()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FormTypeId", PK_FormTypeId),
                                  new SqlParameter("@Parameter",Parameter)
                                  };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);
            return ds;
        }
        public DataSet UpdateFormType()
        {
            SqlParameter[] para ={new SqlParameter("@PK_FormTypeId",PK_FormTypeId),
                                new SqlParameter("@FormType",FormType),
                                new SqlParameter("@Parameter",Parameter),
                                new SqlParameter("@UpdatedBy",CreatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);
            return ds;
        }
        public DataSet SaveFormType()
        {
            SqlParameter[] para ={
                                new SqlParameter("@FormType",FormType),
                                new SqlParameter("@Parameter",Parameter),
                                new SqlParameter("@CreatedBy",CreatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);
            return ds;
        }
        public DataSet DeleteFormType()
        {
            SqlParameter[] para ={
                                new SqlParameter("@PK_FormTypeId",PK_FormTypeId),
                                new SqlParameter("@Parameter",Parameter),
                                new SqlParameter("@DeletedBy",CreatedBy)
                                };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);
            return ds;
        }
    }
}