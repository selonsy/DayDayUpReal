using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpStudy
{
    class CSharp基础练习
    {

        #region 组元(Tuple)

        /// <summary>
        /// 组元:多用于方法的返回值。如果一个函数返回多个类型，这样就不在用out , ref等输出参数了.
        /// 可以直接定义一个tuple类型就可以了,非常方便。
        /// </summary>
        public static void myTuple() 
        {
            //举例一：
            Tuple<int, int> p2 = new Tuple<int, int>(10, 20);
            Console.WriteLine(p2.Item1 + p2.Item2);

            //举例二：
            //1 member 
            //第一个定义包含一个成员
            Tuple<int> test = new Tuple<int>(1);
            //2 member ( 1< n <8 ) 
            //第二个定义包含两个成员，并且使用create方法初始化
            Tuple<int, int> test2 = Tuple.Create<int, int>(1, 2);
            //8 member , the last member must be tuple type.
            //第三个定义展示了tuple最多支持8个成员，如果多于8个就需要进行嵌套。注意第8个成员很特殊，如果有8个成员，第8个必须嵌套定义tuple。
            Tuple<int, int, int, int, int, int, int, Tuple<int>> test3 = new Tuple<int, int, int, int, int, int, int, Tuple<int>>(1, 2, 3, 4, 5, 6, 7, new Tuple<int>(8));

            Console.WriteLine(test.Item1);
            Console.WriteLine(test2.Item1 + test2.Item2);
            Console.WriteLine(test3.Item1 + test3.Item2 + test3.Item3 + test3.Item4 + test3.Item5 + test3.Item6 + test3.Item7 + test3.Rest.Item1);

            //举例三：
            //2 member ,the second type is the nest type tuple.
            Tuple<int, Tuple<int>> test4 = new Tuple<int, Tuple<int>>(1, new Tuple<int>(2));
            //10 member datatype. nest the 8 parameter type.
            Tuple<int, int, int, int, int, int, int, Tuple<int, int, int>> test5 = new Tuple<int, int, int, int, int, int, int, Tuple<int, int, int>>(1, 2, 3, 4, 5, 6, 7, new Tuple<int, int, int>(8, 9, 10));

            Console.WriteLine(test4.Item1 + test4.Item2.Item1);
            Console.WriteLine(test5.Item1 + test5.Item2 + test5.Item3 + test5.Item4 + test5.Item5 + test5.Item6 + test5.Item7 + test5.Rest.Item1 + test5.Rest.Item2 + test5.Rest.Item3);
        }

        #endregion

    }
}
