using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Sql;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using MvcAppMain.Filters;

namespace MvcAppMain.Controllers
{
    [Authorize]
    [AdminFilter]
    public class BackupController : Controller
    {
       
        // GET: Backup
        public ActionResult Backup()
        {
            return View();
        }
        public ActionResult DoBackup()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["QLPMContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Exec BK_PK_CTPK_HDProcedure";
            
            cmd.ExecuteNonQuery(); 
            conn.Close();

            return View("Backup");
          
        }
    }
}