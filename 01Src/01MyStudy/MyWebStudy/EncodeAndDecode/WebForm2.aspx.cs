using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.JScript.Vsa;
using Devin;

namespace Web.EncodeAndDecode
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username1 = Request.QueryString["username1"];
            string username2 = Request.QueryString["username2"];
            string username3 = Request.QueryString["username3"];
            string username4 = Request.QueryString["username4"];

            string myusername1 = Microsoft.JScript.GlobalObject.decodeURIComponent(username2);
            string myusername2 = Microsoft.JScript.GlobalObject.decodeURIComponent(username4);
            string myusername3 = System.Web.HttpUtility.UrlDecode(username1);
            string myusername4 = Microsoft.JScript.GlobalObject.decodeURI(username2);
            string myusername5 = System.Web.HttpUtility.UrlEncode("沈金龙");

            string myusername6 = Server.UrlDecode(username4);
            Microsoft.JScript.GlobalObject.encodeURIComponent("");
            Microsoft.JScript.GlobalObject.encodeURI("");

            try {
                string qq = Request.QueryString["89"];
                string a = qq.Replace('a','2');
            }
            catch(Exception ex) {
                //Devin.LogUtil.WriteError(new Exception(ex.Message));
                //Devin.LogUtil.WriteLog(ex.Message + "呵呵哒！", Devin.LogType.Info);
            }
        }
    }
}