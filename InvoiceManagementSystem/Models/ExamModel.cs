using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace InvoiceManagementSystem.Models
{
    public class ExamModel
    {
        clsCommon objCommon = new clsCommon();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int RollNo { get; set; }
        public int TotalMarks { get; set; }
        public int OutOfMarks { get; set; }
        public string ClassNo { get; set; }
        public int ClassId { get; set; }
        public int UserId { get; set; }
        public int intActive { get; set; }
        public bool IsActive { get; set; }
        public string Response { get; set; }
        public string SearchText { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? TotalRecord { get; set; }
        public decimal? PageCount { get; set; }
        public long? ROWNUMBER { get; set; }
        public int TotalEntries { get; set; }
        public string CreatedDate { get; set; }
        public int ShowingEntries { get; set; }
        public int fromEntries { get; set; }

        public Pager Pager { get; set; }
        public List<ExamModel> LSTExamList { get; set; }
        public ExamModel addExam(ExamModel cls)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddUpdateExam", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = cls.Id;
                cmd.Parameters.AddWithValue("@ClassId", cls.ClassId);
                cmd.Parameters.AddWithValue("@SubjectId", cls.SubjectId);
                cmd.Parameters.AddWithValue("@TotalMarks", cls.TotalMarks);
                cmd.Parameters.AddWithValue("@OutOfMarks", cls.OutOfMarks);
                cmd.Parameters.AddWithValue("@RollNo", cls.RollNo);
                cmd.Parameters.AddWithValue("@UserId", objCommon.getUserIdFromSession());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                da.ReturnProviderSpecificTypes = true;
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    string intRefId = dt.Rows[0][0].ToString();
                    if (intRefId == "1")
                    {
                        cls.Response = "Success";
                    }
                    else if (intRefId == "2")
                    {
                        cls.Response = "Updated";
                    }
                    else if (intRefId == "-1")
                    {
                        cls.Response = "Exists";
                    }
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return cls;
        }
        public ExamModel GetExam(ExamModel cls)
        {
            try
            {
                List<ExamModel> LSTList = new List<ExamModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetSingleExam", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", cls.Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamModel obj = new ExamModel();
                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.ClassId = Convert.ToInt32(dt.Rows[i]["ClassId"] == null || dt.Rows[i]["ClassId"].ToString().Trim() == "" ? null : dt.Rows[i]["ClassId"].ToString());
                        obj.SubjectId = Convert.ToInt32(dt.Rows[i]["SubjectId"] == null || dt.Rows[i]["SubjectId"].ToString().Trim() == "" ? null : dt.Rows[i]["SubjectId"].ToString());
                        obj.RollNo = Convert.ToInt32(dt.Rows[i]["RollNo"] == null || dt.Rows[i]["RollNo"].ToString().Trim() == "" ? null : dt.Rows[i]["RollNo"].ToString());
                        obj.TotalMarks = Convert.ToInt32(dt.Rows[i]["TotalMarks"] == null || dt.Rows[i]["TotalMarks"].ToString().Trim() == "" ? null : dt.Rows[i]["TotalMarks"].ToString());
                        obj.OutOfMarks = Convert.ToInt32(dt.Rows[i]["OutOfMarks"] == null || dt.Rows[i]["OutOfMarks"].ToString().Trim() == "" ? null : dt.Rows[i]["OutOfMarks"].ToString());
                        LSTList.Add(obj);
                    }
                }
                cls.LSTExamList = LSTList;
                return cls;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                throw ex;
            }
        }
        public ExamModel deleteExam(ExamModel cls)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteExam", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", cls.Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                da.ReturnProviderSpecificTypes = true;
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                if (dt.Rows[0][0].ToString() == "1")
                {
                    cls.Response = "Success";
                }
                else if (dt.Rows[0][0].ToString() == "2")
                {
                    cls.Response = "dependency";
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return cls;
        }
    }
}