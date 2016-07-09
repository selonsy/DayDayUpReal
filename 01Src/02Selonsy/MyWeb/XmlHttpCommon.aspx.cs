using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Ajax;
using System.Web.Services;

namespace MyWeb
{
    public partial class XmlHttpCommon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        [Ajax.AjaxMethod]
        public string AjaxMethodInstance(string str)
        {
            return str+ "Ajax.AjaxMethod调用成功！";
        }
        [WebMethod]
        public static string WebMethodInstance(string str)
        {
            return str+ "WebMethod调用成功！";
        }
    }
}