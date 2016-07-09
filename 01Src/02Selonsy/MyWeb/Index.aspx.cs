using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyWeb
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //注册异步公用调用类
            Ajax.Utility.RegisterTypeForAjax(typeof(XmlHttpCommon));
        }
    }
}