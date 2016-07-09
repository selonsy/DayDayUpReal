using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Web.Services;
using ESBHelper.Entity;
using Devin;

namespace ESBHelper
{
    public partial class XmlHttpCommon : System.Web.UI.Page
    {
        //原始文件路径
        string origiFilePathForESB = @"D:\公用文件\ESBHelper\BasicFile\SQL\ESBSiteSql.txt";
        string origiFilePathForERP = @"D:\公用文件\ESBHelper\BasicFile\SQL\ERPSiteSql.txt";
        string origiFilePathForERP_1 = @"D:\公用文件\ESBHelper\BasicFile\SQL\NewERPFuncAndSPSql.txt";
        //目标文件夹路径
        string targetFilePath = @"D:\公用文件\ESBHelper\CompletedSql\";

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

        public string FastCreateSQL() 
        {
            //生成压缩文件
            //ZipHelper.MyZipFile(@"D:\公用文件\ESBHelper\CompletedSql", @"D:\公用文件\ESBHelper\CompletedZip\aaa.zip");        
            return "";
        }

        public string FastTestConnection() 
        {
            return "";
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
        /// 获取格式化后的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetFormatedLine(string input)
        {
            return "";
        }

        public string CreateSQL()
        {
            bool result = false;
            string createType = Request.Form["createType"];

            if (createType =="99")//SiteType.ESBSite.ToString() 
            {
                result = CreateSqlForESB();
            }
            else if (createType == "0" || createType == "1")
            {
                result = CreateSqlForERP();
            }

            if (result == false) return "false";
            return "true";
        }

        public bool CreateSqlForESB()
        {
            string dbEsbName,dbServer,dbPort,dbUserName,dbPassword;
            string dbName1, dbDomain1, dbPort1, providerName1, displayName1, isNewErp1, sysSign1;
            string dbName2, dbDomain2, dbPort2, providerName2, displayName2, isNewErp2, sysSign2;

            dbEsbName = Request.Form["dbEsbName"].ToString();
            dbServer = Request.Form["dbServer"].ToString();
            dbPort = Request.Form["dbPort"].ToString();
            dbUserName = Request.Form["dbUserName"].ToString();
            dbPassword = Request.Form["dbPassword"].ToString();

            dbName1 = Request.Form["dbName1"].ToString();
            var tempArray1 = Request.Form["dbDomain1"].ToString().Split(':');
            dbDomain1 = tempArray1[1].Substring(2);
            dbPort1 = tempArray1[2];
            providerName1 = Request.Form["providerName1"].ToString();
            displayName1 = Request.Form["displayName1"].ToString();
            isNewErp1 = Request.Form["isNewErp1"].ToString();
            sysSign1 = Request.Form["sysSign1"].ToString();

            dbName2 = Request.Form["dbName2"].ToString();
            var tempArray2 = Request.Form["dbDomain2"].ToString().Split(':');
            dbDomain2 = tempArray2[1].Substring(2);
            dbPort2 = tempArray2[2];
            providerName2 = Request.Form["providerName2"].ToString();
            displayName2 = Request.Form["displayName2"].ToString();
            isNewErp2 = Request.Form["isNewErp2"].ToString();
            sysSign2 = Request.Form["sysSign2"].ToString();

            StringBuilder MyStringBuilder = new StringBuilder();
            try
            {
                StreamReader sr = new StreamReader(origiFilePathForESB, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {                    
                    if (line.IndexOf("%dbEsbName%") != -1)
                    {
                        line = line.Replace("%dbEsbName%", dbEsbName);
                    }
                    if (line.IndexOf("%dbServer%") != -1)
                    {
                        line = line.Replace("%dbServer%", dbServer);
                    }
                    if (line.IndexOf("%dbPort%") != -1)
                    {
                        line = line.Replace("%dbPort%", dbPort);
                    }
                    if (line.IndexOf("%dbUserName%") != -1)
                    {
                        line = line.Replace("%dbUserName%", dbUserName);
                    }
                    if (line.IndexOf("%dbPassword%") != -1)
                    {
                        line = line.Replace("%dbPassword%", dbPassword);
                    }

                    if (line.IndexOf("%dbName1%") != -1)
                    {
                        line = line.Replace("%dbName1%", dbName1);
                    }
                    if (line.IndexOf("%dbDomain1%") != -1)
                    {
                        line = line.Replace("%dbDomain1%", dbDomain1);
                    }
                    if (line.IndexOf("%port1%") != -1)
                    {
                        line = line.Replace("%port1%", dbPort1);
                    }
                    if (line.IndexOf("%providerName1%") != -1)
                    {
                        line = line.Replace("%providerName1%", providerName1);
                    }
                    if (line.IndexOf("%displayName1%") != -1)
                    {
                        line = line.Replace("%displayName1%", displayName1);
                    }
                    if (line.IndexOf("%isNewErp1%") != -1)
                    {
                        line = line.Replace("%isNewErp1%", isNewErp1);
                    }
                    if (line.IndexOf("%sysSign1%") != -1)
                    {
                        line = line.Replace("%sysSign1%", sysSign1);
                    }

                    if (line.IndexOf("%dbName2%") != -1)
                    {
                        line = line.Replace("%dbName2%", dbName2);
                    }
                    if (line.IndexOf("%dbDomain2%") != -1)
                    {
                        line = line.Replace("%dbDomain2%", dbDomain2);
                    }
                    if (line.IndexOf("%port2%") != -1)
                    {
                        line = line.Replace("%port2%", dbPort2);
                    }
                    if (line.IndexOf("%providerName2%") != -1)
                    {
                        line = line.Replace("%providerName2%", providerName2);
                    }
                    if (line.IndexOf("%displayName2%") != -1)
                    {
                        line = line.Replace("%displayName2%", displayName2);
                    }
                    if (line.IndexOf("%isNewErp2%") != -1)
                    {
                        line = line.Replace("%isNewErp2%", isNewErp2);
                    }
                    if (line.IndexOf("%sysSign2%") != -1)
                    {
                        line = line.Replace("%sysSign2%", sysSign2);
                    }
                    MyStringBuilder.AppendLine(line);
                }
                //文件名称
                string targetFileName = "ESB站点SQL语句-" + DateTime.Now.ToString("D") + ".sql";
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
                return false;
            }
            return true;            
        }

        public bool CreateSqlForERP()
        {
            string dbName1, dbName2, sysSign1, sysSign2, isNewErp;

            dbName1 = Request.Form["dbName1"].ToString();
            dbName2 = Request.Form["dbName2"].ToString();
            sysSign1 = Request.Form["sysSign1"].ToString();
            sysSign2 = Request.Form["sysSign2"].ToString();
            isNewErp = Request.Form["isNewErp"].ToString();

            StringBuilder MyStringBuilder = new StringBuilder();
            try
            {
                StreamReader sr = new StreamReader(origiFilePathForERP, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.IndexOf("%ERPDataBaseName1%") != -1)
                    {
                        line = line.Replace("%ERPDataBaseName1%", dbName1);
                    }
                    if (line.IndexOf("%ERPDataBaseName2%") != -1)
                    {
                        line = line.Replace("%ERPDataBaseName2%", dbName2);
                    }
                    if (line.IndexOf("%SysSign1%") != -1)
                    {
                        line = line.Replace("%SysSign1%", sysSign1);
                    }     
                    if (line.IndexOf("%SysSign2%") != -1)
                    {
                        line = line.Replace("%SysSign2%", sysSign2);
                    } 
                    if (line.IndexOf("%IsNewErp%") != -1)
                    {
                        line = line.Replace("%IsNewErp%", isNewErp);
                    }
                    MyStringBuilder.AppendLine(line);
                }
                //文件名称
                string targetFileName = sysSign1+"SQL语句-" + DateTime.Now.ToString("D") + ".sql";
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
                return false;
            }
            return true;            
        }

    }
}