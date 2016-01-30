using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
namespace ESBHelper
{
    public partial class XmlHttpCommon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strYwType, strOnlyFlag, strTxt, strTemp;
            string strReturn = "";
            strYwType = Request.QueryString["ywtype"];          //业务标识
            strOnlyFlag = Request.QueryString["ywonlyflag"];    //唯一键值
            strTxt = Request.QueryString["ywtxt"];              //其它参数值            
            switch (strYwType)
            {
                case "CheckDBConnection":
                    //buuug:怎么把前端传递的值传给下面的函数？实体类实例化？
                    //strReturn = ESBProviderService.CheckDBConnectionTemp();
                    strReturn = CheckDBConnection();
                    break;
                case "CreateSQL":  //前端设置一键生成按钮
                    strReturn = CreateSQL();
                    break;
                case "":
                    strReturn = "";
                    break;
                default:
                    strReturn = "";
                    break;
            }

            Response.ContentType = "text/HTML";
            Response.Clear();
            Response.Write(strReturn);
            //Response.Flush();
            Response.End();
        }

        /// <summary>
        /// 获取POST过来的数据
        /// </summary>
        /// <returns></returns>
        private string GetPostStr()
        {
            //Dim b(Request.InputStream.Length - 1) As Byte
            //Request.InputStream.Read(b, 0, Request.InputStream.Length)
            //Return WorkflowPublicProxy.ReplaceKeywords(Encoding.UTF8.GetString(b))
            return "";
        }

        /// <summary>
        /// 数据库链接测试
        /// </summary>
        /// <returns></returns>
        private string CheckDBConnection()
        {
            string dbServer, dbName, dbPort, dbUserName, dbPassword;
            string result = "true";

            dbServer = Request.Form["dbServer"].ToString();
            dbName = Request.Form["dbName"].ToString();
            dbPort = Request.Form["dbPort"].ToString();
            dbUserName = Request.Form["dbUserName"].ToString();
            dbPassword = Request.Form["dbPassword"].ToString();

            //buuug:是否存在前端传递过来的时候，数据有遗漏？所以需要再次进行数据校验？
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

            SqlConnection dbConnection = null;
            try
            {
                dbConnection = new SqlConnection(sqlConnection);
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                result = "false";
            }
            finally
            {
                if (dbConnection != null)
                    dbConnection.Close();
            }            
            return result;
        }

        /// <summary>
        /// 生成格式化后的SQL文件
        /// </summary>
        /// <returns></returns>
        private string CreateSQL()
        {
            string dbEsbName, dbServer, dbName, dbPort, dbUserName, dbPassword, dbDomain;
            string providerName, displayName, isNewErp, sysSign;
            string result = "true";
            //原始文件路径
            string origiFilePath = @"D:\03MySite\01ESBHelper\ESBSiteSql.txt";
            //目标文件路径
            string targetFilePath = @"D:\03MySite\01ESBHelper\CompletedSql\";
            dbEsbName = Request.Form["dbEsbName"].ToString();
            dbServer = Request.Form["dbServer"].ToString();
            dbName = Request.Form["dbName"].ToString();
            dbPort = Request.Form["dbPort"].ToString();
            dbUserName = Request.Form["dbUserName"].ToString();
            dbPassword = Request.Form["dbPassword"].ToString();
            dbDomain = Request.Form["dbDomain"].ToString();
            providerName = Request.Form["providerName"].ToString();
            displayName = Request.Form["displayName"].ToString();
            isNewErp = Request.Form["isNewErp"].ToString();
            sysSign = Request.Form["sysSign"].ToString();

            StringBuilder MyStringBuilder = new StringBuilder();
            try
            {
                StreamReader sr = new StreamReader(origiFilePath, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {                    
                    if (line.IndexOf("%ESBDataBaseName%") != -1)
                    {
                        line = line.Replace("%ESBDataBaseName%", dbEsbName);
                    }
                    if (line.IndexOf("%ProviderName%") != -1)
                    {
                        line = line.Replace("%ProviderName%", providerName);
                    }
                    if (line.IndexOf("%DisplayName%") != -1)
                    {
                        line = line.Replace("%DisplayName%", displayName);
                    }
                    if (line.IndexOf("%DataBaseName%") != -1)
                    {
                        line = line.Replace("%DataBaseName%", dbName);
                    }
                    if (line.IndexOf("%SysSign%") != -1)
                    {
                        line = line.Replace("%SysSign%", sysSign);
                    }
                    if (line.IndexOf("%IsNewErp%") != -1)
                    {
                        line = line.Replace("%IsNewErp%", isNewErp);
                    }
                    if (line.IndexOf("%Domain%") != -1)
                    {
                        line = line.Replace("%Domain%", dbDomain);
                    }
                    if (line.IndexOf("%DataBaseServer%") != -1)
                    {
                        line = line.Replace("%DataBaseServer%", dbServer);
                    }
                    if (line.IndexOf("%DataBaseUserName%") != -1)
                    {
                        line = line.Replace("%DataBaseUserName%", dbUserName);
                    }
                    if (line.IndexOf("%DataBasePassword%") != -1)
                    {
                        line = line.Replace("%DataBasePassword%", dbPassword);
                    }
                    if (line.IndexOf("%Port%") != -1)
                    {
                        line = line.Replace("%Port%", dbPort);
                    }
                    MyStringBuilder.AppendLine(line);
                }
                //文件名称
                string targetFileName = displayName + "-" + DateTime.Now.ToString("D") + ".sql";
                //创建文件
                FileStream fs = new FileStream(targetFilePath + targetFileName, FileMode.Create);                                  
                //获得字节数组
                byte[] data = System.Text.Encoding.Default.GetBytes(MyStringBuilder.ToString());
                //写入文件
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                result = "false";
            }
            return result;
        }

        /// <summary>
        /// 获取格式化后的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetFormatedLine(string input)
        {
            return "";
        }
    }
}