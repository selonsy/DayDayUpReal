using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Text.RegularExpressions;

namespace MyAlgorithmStudy
{
    /// <summary>
    /// 字符串相关
    /// </summary>
    public class StringTest
    {
        //通用测试方法
        public static void Test()
        {
            string addStr = addString("10", 3, '0');
            Console.WriteLine(addStr);

            Console.WriteLine("OK！测试结束~");
        }

        /// <summary>
        /// 要求返回字符串的长度至少为minLength,不够的用padChar在前面附加
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="padChar">用来附加的字符</param>
        /// <returns>返回更改后的字符串(可能没有变化)</returns>
        private static string addString(String str, int minLength, char padChar)
        {
            string strResult = "";
            int length = str.Length;
            if (length >= minLength)
            {
                return str;
            }
            else
            {
                for (int i = 0; i < minLength - length; i++)
                {
                    strResult += padChar;
                }
            }
            return strResult + str;
        }

        /// <summary>
        /// 记录字符串中字符的出现次数，并按从高到低进行排序输出
        /// </summary>
        public static void Count()
        {
            string input = "bbacccddddddddddddffff";
            Dictionary<char, int> dic = new Dictionary<char, int>();

            for (int i = 0; i < input.Length; i++)
            {
                char cc = input[i];
                if (!dic.ContainsKey(cc))
                {
                    dic.Add(cc, 1);
                }
                else
                {
                    dic[input[i]] = dic[cc] + 1;
                }
            }

            var dicSort = from objDic in dic orderby objDic.Value descending select objDic;

            foreach (KeyValuePair<char, int> item in dicSort)
            {
                Console.WriteLine("{0}出现{1}次", item.Key, item.Value);
            }

            Console.ReadKey();

        }

        /// <summary>
        /// 计算1-2+3-4+5....+n的值
        /// </summary>
        public static void Count1()
        {
            //参数校验。。。
            int n = Convert.ToInt32(Console.ReadLine());

            int sum = 0;
            List<int> array = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                if (i % 2 == 0)
                {
                    array.Add(-i);
                }
                else
                {
                    array.Add(i);
                }
            }

            for (int j = 0; j < array.Count; j++)
            {
                sum += array[j];
            }

            Console.WriteLine("结果为：{0}", sum);
        }

        /// <summary>
        /// 替换字符串格式 
        /// 如：-name Aric -age 21 -address "四川"
        /// 替换为： [-name Aric,-age 21,-address "四川"]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// 备注：没有处理双引号的情况
        public static string ReplaceParams(string str)
        {
            string strResult = "[";
            string[] strArray = str.Trim().Split('-');   //注意： '-'有三个，那么返回的数组元素有四个，第一个是空字符。
            for (int i = 0; i < strArray.Length; i++)
            {
                //strArray[i] = strArray[i].Replace("\\s+"," ");
                strArray[i] = Regex.Replace(strArray[i], "\\s+", " ", RegexOptions.IgnoreCase);
            }

            for (int j = 0; j < strArray.Length; j++)
            {
                if (strArray[j] != "")
                {
                    strResult = strResult + '-' + strArray[j].Trim() + ",";
                }
            }

            return strResult.Substring(0, strResult.Length - 1) + "]";
        }

        
    }

    //   <script type = "text/javascript" >
    //   function MyTest()
    //   {
    //       var listItem =[1, 3, 4, 6, 7, 9, 10];
    //       var key = 4;
    //       //alert(key+'在数组中第'+BinarySerch(key,listItem)+'个位置!');

    //       var strDouble = "aywyert2dd";
    //       var strDouble1 = "wodehka"

    //       var strOutput = FindFirstDoubleStr(strDouble1);
    //       alert(strOutput);

    //       //数组测试 
    //       //myJsTest();
    //   }
    //   //二分折半查找
    //   function BinarySerch(key, array)
    //   {
    //       var mValue, mNum;
    //       var mArray = array;
    //       for (var i = 0; i <= array.length / 2; i++)
    //       {
    //           mNum = Math.floor(mArray.length / 2);
    //           mValue = mArray[mNum];
    //           if (mValue == key)
    //           {
    //               return i;
    //           }
    //           else if (mValue > key)
    //           {
    //               mArray = mArray.slice(0, mNum);
    //           }
    //           else if (mValue < key)
    //           {
    //               mArray = mArray.slice(mNum + 1, mArray.length);
    //           }
    //       };
    //       return -1;
    //   }

    //   //找到首先出现两次的字符
    //   function FindFirstDoubleStr(str)
    //   {
    //       var strArray = str.split("");
    //       var length = strArray.length;
    //       for (var i = 0; i < length; i++)
    //       {
    //           for (var j = i + 1; j < length - i; j++)
    //           {
    //               if (strArray[i] == strArray[j])
    //               {
    //                   return strArray[i];
    //               }
    //           }
    //       }
    //       return "没有出现过两次的字符！";
    //   }




}
