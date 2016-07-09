using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class WebServiceTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ServiceReference.WebServiceTest aa = new ServiceReference.WebServiceTest();  
            WebServiceTestForWeb.WebServiceTest aa =new WebServiceTestForWeb.WebServiceTest();
           
            Label1.Text =Convert.ToString(aa.GetSum(Convert.ToInt32(TextBox1.Text.ToString()),Convert.ToInt32(TextBox2.Text.ToString())));
        }
    }
}