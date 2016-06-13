using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace MyCSharpStudy
{
    class Route
    {
        public static void RegisterRoutes()
        {
            //定义路由            
            RouteTable.Routes.MapPageRoute("twolayer", "{service}/{action}.html", "~/func/view.aspx");
            RouteTable.Routes.MapPageRoute("genericapi", "{service}/{action}.json", "~/func/func.aspx");
            RouteTable.Routes.MapPageRoute("default", "", "~/func/view.aspx");


            //取出路由中的数据
            RouteData routedata = new RouteData();
            routedata.Values["service"].ToString();
            routedata.Values["action"].ToString();


            //可以根据路由中的service以及action数据，来反射调用对应的方法。
        }

    }
}
