using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpStudy
{
    public class CSharp基础知识
    {

        public static void Test()
        {
            UseParams(1, 2, 3);
            UseParams2(1, 'a', "test");
            // An array of objects can also be passed, as long as
            // the array type matches the method being called.
            int[] myarray = new int[3] { 10, 11, 12 };
            UseParams(myarray);
        }

        #region 组元(Tuple)

        /// <summary>
        /// 组元:多用于方法的返回值。
        /// 如果一个函数返回多个类型，这样就不再用out/ref等输出参数了，而是可以直接定义一个tuple类型就可以了,非常方便。
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

        #region params
        public static void UseParams(params int[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(list[i]);
            }
            Console.WriteLine();
        }
        public static void UseParams2(params object[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(list[i]);
            }
            Console.WriteLine();
        }
        #endregion

        #region 数组

        /// <summary>
        /// 数组练习
        /// </summary>
        public static void MyArray()
        {

            #region 一维数组

            //声明一维数组，没有初始化，等于null 
            int[] intArray1;

            //初始化已声明的一维数组 
            intArray1 = new int[3];  //数组元素的默认值为0 
            intArray1 = new int[3] { 1, 2, 3 };
            intArray1 = new int[] { 1, 2, 3 };

            //声明一维数组，同时初始化 
            int[] intArray2 = new int[3] { 1, 2, 3 };
            int[] intArray3 = new int[] { 4, 3, 2, 1 };
            int[] intArray4 = { 1, 2, 3, 4 };
            string[] strArray1 = new string[] { "One", "Two", "Three" };
            string[] strArray2 = { "This", "is", "an", "string", "Array" };

            #endregion

            #region 二维数组

            //声明二维数组，没有初始化 
            short[,] sArray1;

            //初始化已声明的二维数组 
            sArray1 = new short[2, 2];
            sArray1 = new short[2, 2] { { 1, 1 }, { 2, 2 } };
            sArray1 = new short[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            //声明二维数组，同时初始化 
            short[,] sArray2 = new short[1, 1] { { 100 } };
            short[,] sArray3 = new short[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            short[,] sArray4 = { { 1, 1, 1 }, { 2, 2, 2 } };

            //声明三维数组，同时初始化 
            byte[,,] bArray1 = { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };

            #endregion

            #region 交错数组

            //声明交错数组，没有初始化 
            int[][] JagIntArray1;

            //初始化已声明的交错数组 
            JagIntArray1 = new int[2][] {
                new int[]{1,2},
                new int[]{3,4,5,6}
             };
            JagIntArray1 = new int[][]{
                new int[]{1,2}, 
                // new int[]{3,4,5}, 
                intArray2 //使用int[]数组变量 
            };

            //声明交错数组，同时初始化 
            int[][] JagIntArray2 = {
                new int[]{1,1,1},
                new int[]{2,2}, 
                //intArray1 
             };

            #endregion

            #region 数组的常见属性

            //数组名.Length ：返回一个整数，该整数表示该数组的所有维数中元素的总数。
            //数组名.Rank ：返回一个整数，该整数表示该数组的维数。
            //数组名.GetLength（int dimension） ：返回一个整数，该整数表示该数组的指定维(由参数dimension指定，维度从零开始)中的元素个数。
            Console.WriteLine(sArray4.Length);
            Console.WriteLine(sArray4.Rank);
            Console.WriteLine(sArray4.GetLength(1));
            Console.ReadKey();

            #endregion

            #region 数组的遍历 

            int[] numbers = { 4, 5, 6, 1, 2, 3, -2, -1, 0 };
            foreach (int i in numbers)
            {
                Console.WriteLine(i);  //值为4,5,6,1,2,3，-2，-1,0
            }

            int[,] numbers1 = new int[3, 2] { { 9, 99 }, { 3, 33 }, { 5, 55 } };
            foreach (int i in numbers1)
            {
                Console.Write("{0} ", i); //值为9 99 3 33 5 55
            }

            #endregion
        }

        #endregion
    }
}
