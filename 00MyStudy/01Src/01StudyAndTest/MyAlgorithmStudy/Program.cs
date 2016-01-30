using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows;

namespace MyAlgorithmStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            //string addStr = addString("10",3,'0');
            //Console.WriteLine(addStr);
            var ss= "[{\"BUGUID\":1001,\"BUName\":\"明源\",\"HierarchyCode\":0001,\"ParentGUID\":001,\"BUType\":0},{\"BUGUID\":1002,\"BUName\":\"武汉\",\"HierarchyCode\":0002,\"ParentGUID\":002,\"BUType\":0},{\"BUGUID\":1003,\"BUName\":\"武源\",\"HierarchyCode\":0003,\"ParentGUID\":003,\"BUType\":0}]";
            var tt = "{\"BUGUID\":1001,\"BUName\":\"武汉明源\",\"HierarchyCode\":0001,\"ParentGUID\":001,\"BUType\":0}";
            string resultJson=MyJson(ss);
            Console.WriteLine(resultJson);
            Console.ReadKey();
        }

        /// <summary>
        /// 要求返回字符串的长度至少为minLength,不够的用padChar在前面附加
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="padChar">用来附加的字符</param>
        /// <returns>返回更改后的字符串(可能没有变化)</returns>
        private static string addString(String str,int minLength,char padChar)
        {   
            string strResult="";
            int length = str.Length;
            if (length >= minLength)
            {
                return str;
            }
            else 
            {
                for(int i=0;i<minLength-length;i++)
                {
                    strResult+=padChar;
                }
            }
            return strResult+str;
        }

        /// <summary>
        /// 对JSON字符串进行操作
        /// </summary>
        /// <param name="strJson"></param>
        private static string MyJson(string strJson) 
        {

            JArray array = JArray.Parse(strJson);
            //JObject obj = JObject.Parse(strJson);
            
            return array[0]["BUName"].ToString();
            
        }

    }
}
