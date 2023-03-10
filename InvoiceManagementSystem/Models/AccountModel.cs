using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvoiceManagementSystem.Models
{
    public class AccountModel
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        clsCommon objCommon = new clsCommon();
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Address { get; set; }

        public int UserId { get; set; }
        public int TeacherId { get; set; }
        public HttpPostedFileBase[] strFile { get; set; }
        public string Response { get; set; }


        public List<AccountModel> LSTAccountList { get; set; }
        public AccountModel SignUp(AccountModel cls)
        {
            try
            {
                var ddd = clsCommon.DecryptString("QU734hNlS/9lJ6Eof1tOcg==");
                cls.Password = clsCommon.EncryptString(cls.Password);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UserRegistration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", cls.UserName);
                cmd.Parameters.AddWithValue("@Email", cls.Email);
                cmd.Parameters.AddWithValue("@Password", cls.Password);

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

        public AccountModel Login(AccountModel cls)
        {
            AccountModel obj = new AccountModel();
            // clsCommon objcommon = new clsCommon();
            try
            {
                var ddd = clsCommon.DecryptString("QU734hNlS/9lJ6Eof1tOcg==");
                cls.Password = clsCommon.EncryptString(cls.Password);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Login", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", cls.UserName);
                cmd.Parameters.AddWithValue("@Password", cls.Password);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                da.ReturnProviderSpecificTypes = true;
                DataTable dt = new DataTable();
                da.Fill(dt); 
                conn.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    obj.Id = Convert.ToInt32(dt.Rows[0]["Id"] == null || dt.Rows[0]["Id"].ToString().Trim() == "" ? "0" : dt.Rows[0]["Id"].ToString());
                    obj.Email = dt.Rows[0]["Email"] == null || dt.Rows[0]["Email"].ToString().Trim() == "" ? "" : dt.Rows[0]["Email"].ToString();
                    obj.UserName = dt.Rows[0]["UserName"] == null || dt.Rows[0]["UserName"].ToString().Trim() == "" ? "" : dt.Rows[0]["UserName"].ToString();
                    obj.Password = dt.Rows[0]["Password"] == null || dt.Rows[0]["Password"].ToString().Trim() == "" ? "" : dt.Rows[0]["Password"].ToString();
                    obj.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"] == null || dt.Rows[0]["RoleId"].ToString().Trim() == "" ? "0" : dt.Rows[0]["RoleId"].ToString());
                    obj.TeacherId = Convert.ToInt32(dt.Rows[0]["TeacherId"] == null || dt.Rows[0]["TeacherId"].ToString().Trim() == "" ? "0" : dt.Rows[0]["TeacherId"].ToString());
                    obj.Response = "Success";
                }
                else
                {
                    obj.Response = "Error";
                }
                
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return obj;
        }
        public AccountModel MyProfile(AccountModel cls)
        {
            try
            {
                List<AccountModel> LSTList = new List<AccountModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetMyProfile", conn);
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
                        AccountModel obj = new AccountModel();
                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.FullName = dt.Rows[i]["FullName"] == null || dt.Rows[i]["FullName"].ToString().Trim() == "" ? null : dt.Rows[i]["FullName"].ToString();
                        obj.UserName = dt.Rows[i]["UserName"] == null || dt.Rows[i]["UserName"].ToString().Trim() == "" ? null : dt.Rows[i]["UserName"].ToString();
                        obj.Mobile = dt.Rows[i]["MobileNo"] == null || dt.Rows[i]["MobileNo"].ToString().Trim() == "" ? null : dt.Rows[i]["MobileNo"].ToString();
                        obj.Email = dt.Rows[i]["Email"] == null || dt.Rows[i]["Email"].ToString().Trim() == "" ? null : dt.Rows[i]["Email"].ToString();
                        obj.Address = dt.Rows[i]["Address"] == null || dt.Rows[i]["Address"].ToString().Trim() == "" ? null : dt.Rows[i]["Address"].ToString();
                        LSTList.Add(obj);
                    }
                }
                cls.LSTAccountList = LSTList;
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
        public AccountModel UpdateProfile(AccountModel cls)
        {
            AccountModel res = new AccountModel();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Sp_UpdateProfile", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", objCommon.getUserIdFromSession());
                cmd.Parameters.AddWithValue("@FullName", cls.FullName);
                //cmd.Parameters.AddWithValue("@RoleId", cls.RoleId);
                cmd.Parameters.AddWithValue("@MobileNo", cls.Mobile);
                cmd.Parameters.AddWithValue("@Email", cls.Email);
                cmd.Parameters.AddWithValue("@UserName", cls.UserName);
                cmd.Parameters.AddWithValue("@Address", cls.Address);

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
                        res.Response = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                cls.Response = "error";
            }
            return res;
        }

        public AccountModel ChangePassword(AccountModel cls)
        {
            AccountModel res = new AccountModel();
            try
            {
                var ddd = clsCommon.DecryptString("QU734hNlS/9lJ6Eof1tOcg==");
                cls.Password = clsCommon.EncryptString(cls.Password);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Sp_ChangePassword", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", objCommon.getUserIdFromSession());
                cmd.Parameters.AddWithValue("@Password", cls.Password);
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
                        res.Response = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                cls.Response = "error";
            }
            return res;
        }

    }
}