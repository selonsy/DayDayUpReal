using Mysoft.ESB.Entity;
using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.Web;
using Mysoft.Map.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Data.SqlClient;
using Mysoft.ESB.UI.Services;

namespace ESBHelper.Services
{

    public class ESBProviderService
    {
        public static string CheckDBConnectionTemp()
        {
            //HttpResult hr = new HttpResult() { Result = true };
            //return new JsonResult(hr);
            return "True";
        }
   
        public JsonResult CheckDBConnection(string dbServer, string dbName, string dbPort, string dbUserName, string dbPassword)
        {
            if (string.IsNullOrEmpty(dbServer))
                throw new ArgumentException("服务器地址不能为空.");
            if (string.IsNullOrEmpty(dbName))
                throw new ArgumentException("数据库名称不能为空.");
            if (string.IsNullOrEmpty(dbUserName))
                throw new ArgumentException("数据库账号不能为空.");
            if (string.IsNullOrEmpty(dbPassword))
                throw new ArgumentException("数据库密码不能为空.");

            string sqlConnection = string.Empty;
            if (dbPort != "1433" && !string.IsNullOrEmpty(dbPort))
                sqlConnection = string.Format("server={0},{1};uid={2};pwd={3};database={4}", dbServer, dbPort, dbUserName, dbPassword, dbName);
            else
                sqlConnection = string.Format("server={0};uid={1};pwd={2};database={3}", dbServer, dbUserName, dbPassword, dbName);

            HttpResult hr = new HttpResult() { Result = true };
            SqlConnection dbConnection = null;
            try
            {
                dbConnection = new SqlConnection(sqlConnection);
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                hr.Result = false;
                hr.ErrorMessage = ex.Message;
            }
            finally
            {
                if (dbConnection != null)
                    dbConnection.Close();
            }
            return new JsonResult(hr);
        }

    }

}
