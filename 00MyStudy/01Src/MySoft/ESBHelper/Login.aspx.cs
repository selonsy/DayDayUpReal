using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESBHelper
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Request.Form["form-username"];
            string password = Request.Form["form-password"];

            if (username == "test" && password == "123")
            {
                Session["UserGUID"] = "456";
                Response.Redirect("Default.aspx");
            }
            else {
                Response.Redirect("loginforms/form-1/index.html");
            }
        }
    }
}