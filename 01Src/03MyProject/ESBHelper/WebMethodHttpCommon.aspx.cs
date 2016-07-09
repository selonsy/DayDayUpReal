using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Devin;

namespace ESBHelper
{
    public partial class WebMethodHttpCommon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 将生成的SQL语句压缩
        /// </summary>
        [WebMethod]
        public static void ZipFileAfterCreateSQL() 
        {
            ZipHelper.Zip(@"D:\公用文件\ESBHelper\CompletedSql", @"D:\公用文件\ESBHelper\CompletedZip\深圳华南城ESB数据库更新包.zip");        
        }
    }
}