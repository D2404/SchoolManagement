using InvoiceManagementSystem.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManagementSystem.Controllers
{
    public class ExamController : Controller
    {
        clsCommon objCommon = new clsCommon();

        // GET: Exam
        public ActionResult Exam()
        {

            return View();
        }

        [HttpPost]

        public ActionResult InsertExam(ExamModel model)
        {
            model = model.addExam(model);
            return Json(model.Response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetExam(ExamModel cls)
        {
            try
            {
                int TotalEntries = 0;
                int showingEntries = 0;
                int startentries = 0;
                List<ExamModel> lstExamList = new List<ExamModel>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetExamList", conn);
                cmd.Parameters.AddWithValue("@PageSize", cls.PageSize);
                cmd.Parameters.AddWithValue("@PageIndex", cls.PageIndex);
                cmd.Parameters.AddWithValue("@Search", cls.SearchText);
                cmd.Parameters.AddWithValue("@ClassId", cls.ClassId);
                cmd.Parameters.AddWithValue("@UserId", objCommon.getUserIdFromSession());
                cmd.Parameters.AddWithValue("@intActive", cls.intActive);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                conn.Close();


                if (dt != null && dt.Rows.Count > 0)
                {

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamModel obj = new ExamModel();
                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.ClassId = Convert.ToInt32(dt.Rows[i]["ClassId"] == null || dt.Rows[i]["ClassId"].ToString().Trim() == "" ? null : dt.Rows[i]["ClassId"].ToString());
                        obj.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"] == null || dt.Rows[i]["IsActive"].ToString().Trim() == "" ? null : dt.Rows[i]["IsActive"].ToString());
                        obj.SubjectId = Convert.ToInt32(dt.Rows[i]["SubjectId"] == null || dt.Rows[i]["SubjectId"].ToString().Trim() == "" ? null : dt.Rows[i]["SubjectId"].ToString());
                        obj.SubjectName = dt.Rows[i]["SubjectName"] == null || dt.Rows[i]["SubjectName"].ToString().Trim() == "" ? null : dt.Rows[i]["SubjectName"].ToString();
                        obj.ClassNo = dt.Rows[i]["ClassNo"] == null || dt.Rows[i]["ClassNo"].ToString().Trim() == "" ? null : dt.Rows[i]["ClassNo"].ToString();
                        obj.RollNo = Convert.ToInt32(dt.Rows[i]["RollNo"] == null || dt.Rows[i]["RollNo"].ToString().Trim() == "" ? null : dt.Rows[i]["RollNo"].ToString());
                        obj.TotalMarks = Convert.ToInt32(dt.Rows[i]["TotalMarks"] == null || dt.Rows[i]["TotalMarks"].ToString().Trim() == "" ? null : dt.Rows[i]["TotalMarks"].ToString());
                        obj.OutOfMarks = Convert.ToInt32(dt.Rows[i]["OutOfMarks"] == null || dt.Rows[i]["OutOfMarks"].ToString().Trim() == "" ? null : dt.Rows[i]["OutOfMarks"].ToString());
                        obj.ROWNUMBER = Convert.ToInt32(dt.Rows[i]["ROWNUMBER"] == null || dt.Rows[i]["ROWNUMBER"].ToString().Trim() == "" ? null : dt.Rows[i]["ROWNUMBER"].ToString());
                        obj.PageCount = Convert.ToInt32(dt.Rows[i]["PageCount"] == null || dt.Rows[i]["PageCount"].ToString().Trim() == "" ? null : dt.Rows[i]["PageCount"].ToString());
                        obj.PageSize = Convert.ToInt32(dt.Rows[i]["PageSize"] == null || dt.Rows[i]["PageSize"].ToString().Trim() == "" ? null : dt.Rows[i]["PageSize"].ToString());
                        obj.PageIndex = Convert.ToInt32(dt.Rows[i]["PageIndex"] == null || dt.Rows[i]["PageIndex"].ToString().Trim() == "" ? null : dt.Rows[i]["PageIndex"].ToString());
                        obj.TotalRecord = Convert.ToInt32(dt.Rows[i]["TotalRecord"] == null || dt.Rows[i]["TotalRecord"].ToString().Trim() == "" ? null : dt.Rows[i]["TotalRecord"].ToString());
                        lstExamList.Add(obj);
                    }
                }
                cls.LSTExamList = lstExamList;
                if (cls.LSTExamList.Count > 0)
                {
                    var pager = new Models.Pager((int)cls.LSTExamList[0].TotalRecord, cls.PageIndex, (int)cls.PageSize);

                    cls.Pager = pager;
                }
                cls.TotalEntries = TotalEntries;
                cls.ShowingEntries = showingEntries;
                cls.fromEntries = startentries;
                cls.LSTExamList = lstExamList;

                return PartialView("_ExamListPartial", cls);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetSingleExamData(ExamModel cls)
        {
            try
            {
                cls = cls.GetExam(cls);
                return Json(cls, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult deleteExam(ExamModel cls)
        {
            try
            {
                cls = cls.deleteExam(cls);
                return Json(cls, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetClassRoom(ClassRoomModel cls)
        {
            try
            {
                List<ClassRoomModel> lstClientList = new List<ClassRoomModel>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetClassRoomList", conn);
                cmd.Parameters.AddWithValue("@UserId", objCommon.getUserIdFromSession());
                cmd.Parameters.AddWithValue("@intActive", 1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                conn.Close();


                if (dt != null && dt.Rows.Count > 0)
                {

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        ClassRoomModel obj = new ClassRoomModel();

                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.ClassNo = dt.Rows[i]["ClassNo"] == null || dt.Rows[i]["ClassNo"].ToString().Trim() == "" ? null : dt.Rows[i]["ClassNo"].ToString();

                        lstClientList.Add(obj);
                    }
                }
                cls.LSTClassRoomList = lstClientList;

                return Json(cls, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        

        [HttpGet]
        public ActionResult GetSubjectList(int? ClassId = 0)
        {
            try
            {
                var lstUser = objCommon.Fill_Subject(Convert.ToInt32(ClassId));
                return Json(lstUser, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public ActionResult LoadSubject(int ClassId)
        {
            try
            {

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                clsCommon objCommon = new clsCommon();
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_LoadSubjectDropDown", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", objCommon.getUserIdFromSession());
                cmd.Parameters.Add("@ClassId", ClassId);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
                List<ExamModel> clsLst = new List<ExamModel>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamModel obj = new ExamModel();
                        obj.SubjectId = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.SubjectName = dt.Rows[i]["SubjectName"] == null || dt.Rows[i]["SubjectName"].ToString().Trim() == "" ? null : dt.Rows[i]["SubjectName"].ToString();
                        clsLst.Add(obj);
                    }
                }
                return Json(clsLst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}