using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvoiceManagementSystem.Models
{
    public class DashboardModel
    {
        clsCommon objCommon = new clsCommon();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public int Id { get; set; }
        public int TotalClassRoom { get; set; }
        public int TotalStudent { get; set; }
        public int TotalTeacher { get; set; }
        public int TotalSubject { get; set; }
        public string Response { get; set; }
        public List<DashboardModel> LSTDashBoardList { get; set; }

        public DashboardModel GetUserAccountDashboardCount(DashboardModel cls)
        {
            try
            {
                List<DashboardModel> LSTList = new List<DashboardModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetDashboardCountList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", objCommon.getUserIdFromSession());
                //cmd.Parameters.AddWithValue("@intUserType", objCommon.getUserTypeFromSession());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        DashboardModel obj = new DashboardModel();
                        obj.TotalClassRoom = Convert.ToInt32(dt.Rows[i]["TotalClassRoom"] == null || dt.Rows[i]["TotalClassRoom"].ToString().Trim() == "" ? null : dt.Rows[i]["TotalClassRoom"].ToString());
                        obj.TotalStudent = Convert.ToInt32(dt.Rows[i]["TotalStudent"] == null || dt.Rows[i]["TotalStudent"].ToString().Trim() == "" ? null : dt.Rows[i]["TotalStudent"].ToString());
                        obj.TotalTeacher = Convert.ToInt32(dt.Rows[i]["TotalTeacher"] == null || dt.Rows[i]["TotalTeacher"].ToString().Trim() == "" ? null : dt.Rows[i]["TotalTeacher"].ToString());
                        obj.TotalSubject = Convert.ToInt32(dt.Rows[i]["TotalSubject"] == null || dt.Rows[i]["TotalSubject"].ToString().Trim() == "" ? null : dt.Rows[i]["TotalSubject"].ToString());

                        LSTList.Add(obj);
                    }
                }
                cls.LSTDashBoardList = LSTList;
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
    }
}