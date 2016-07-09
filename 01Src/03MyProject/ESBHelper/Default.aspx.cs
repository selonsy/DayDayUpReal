using Devin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ESBHelper
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserGUID"]==null||Session["UserGUID"]==""){
                Response.Redirect("/Login.aspx");
            }
        }

        /// <summary>
        /// 一键下载按钮实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownLoadSQLFast(object sender, EventArgs e)
        {
            string filepath = @"D:\公用文件\ESBHelper\CompletedZip\深圳华南城ESB数据库更新包.zip";
            FileHelper.WriteFile(filepath);
        }     
    }
}