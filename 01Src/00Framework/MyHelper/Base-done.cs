using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devin
{
    public static class SelonsyBase
    {
        //日志文件路径
        public static string LogDefaultPath = @"D:\公用文件\MyLog";

        //数据库链接
        public static string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStr"].ToString().Trim();
    }
}
