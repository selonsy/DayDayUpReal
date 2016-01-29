using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBHelper.Services
{
    class ESBCommonService
    {

        /// <summary>
        /// 功能：获取采用Post方式Send过来的值
        /// </summary>
        /// <returns></returns>
        public static string GetPostStr() 
        {
            //    Dim b(Request.InputStream.Length - 1) As Byte
            //    Request.InputStream.Read(b, 0, Request.InputStream.Length)
            //    Return SqlInjectionUtil.ReplaceKeywords(System.Text.Encoding.UTF8.GetString(b))
            return "";
        }
    }
}
