using StockDamage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockDamage.Controllers
{
    public class StockDamageController : Controller
    {
        string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public ActionResult Index()
        {
            ViewBag.Godown = GetData("SELECT GodownNo, GodownName FROM Godown");
            ViewBag.Items = GetData("SELECT SubItemCode, SubItemName FROM SubItemCode");
            ViewBag.Currency = GetData("SELECT CurrencyName FROM Currency");
            ViewBag.Employee = GetData("SELECT EmployeeName FROM Employee");
            return View();
        }

        private DataTable GetData(string query)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // AJAX: Get Item Details
        public JsonResult GetItemDetails(string subItemCode)
        {
            string query = $@"
            SELECT 
                s.SubItemCode,
                s.Unit,
                ISNULL(st.StockQty, 0) AS StockQty
            FROM SubItemCode s
            LEFT JOIN Stock st ON s.SubItemCode = st.SubItemCode
            WHERE s.SubItemCode = @SubItemCode";

            SqlParameter[] param = {
                new SqlParameter("@SubItemCode", subItemCode)
            };

            DataTable dt = GetData(query, param);

            if (dt.Rows.Count > 0)
            {
                return Json(new
                {
                    SubItemCode = dt.Rows[0]["SubItemCode"].ToString(),
                    Unit = dt.Rows[0]["Unit"].ToString(),
                    StockQty = dt.Rows[0]["StockQty"].ToString()
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // AJAX: Get Exchange Rate
        public JsonResult GetExchangeRate(string currencyName)
        {
            string query = @"
            SELECT ConversionRate 
            FROM Currency 
            WHERE CurrencyName = @CurrencyName";

            SqlParameter[] param = {
                new SqlParameter("@CurrencyName", currencyName)
            };

            DataTable dt = GetData(query, param);

            if (dt.Rows.Count > 0)
            {
                return Json(new
                {
                    Rate = dt.Rows[0]["ConversionRate"].ToString()
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // GetData helper (2 params for SQL parameter)
        public DataTable GetData(string query, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        [HttpPost]
        public ActionResult Save(List<StockDamageModel> model)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                foreach (var item in model)
                {
                    SqlCommand cmd = new SqlCommand("SP_TableName_Save", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@WarehouseName", item.WarehouseName);
                    cmd.Parameters.AddWithValue("@SubItemName", item.SubItemName);
                    cmd.Parameters.AddWithValue("@SubItemCode", item.SubItemCode);
                    cmd.Parameters.AddWithValue("@BatchNo", item.BatchNo);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    cmd.Parameters.AddWithValue("@Rate", item.Rate);
                    cmd.Parameters.AddWithValue("@AmountIn", item.AmountIn);
                    cmd.Parameters.AddWithValue("@ExchangeRate", item.ExchangeRate);
                    cmd.Parameters.AddWithValue("@TotalAmount", item.TotalAmount);
                    cmd.Parameters.AddWithValue("@EmployeeName", item.EmployeeName);
                    cmd.Parameters.AddWithValue("@Comments", item.Comments);

                    cmd.ExecuteNonQuery();
                }
            }
            return Json("Saved Successfully");
        }
    }
}