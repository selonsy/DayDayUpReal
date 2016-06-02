using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCSharpStudy
{
    /// <summary>
    /// 线程测试类
    /// </summary>
    public class ThreadTest
    {
        //通用测试方法
        public static void Test()
        {
            ThreadTest test = new ThreadTest();
        }

        public void PrintHaHa()
        {
            Console.WriteLine("HaHa");
        }

        private int __index = 100;
        public int Index
        {
            get
            {
                return __index;
            }

            set
            {
                __index = value;
            }
        }

        //public static void PrintHaHa(string str)
        //{
        //    Console.WriteLine(str);
        //    Console.ReadKey();
        //}

        public ThreadTest()
        {
            Thread thread = new Thread(new ThreadStart(PrintHaHa));
            thread.Start();
            if (thread.IsAlive)
            {
                Console.WriteLine("还活着，但马上要死了！");
                thread.Abort();
                //thread.Sleep(1000);
                Thread.Sleep(1000);     //不能设置为实例
                //thread.Priority = ThreadPriority.Normal; //设置优先级
                //thread.Suspend();     //挂起线程，已过时
                //thread.Resume();      //恢复线程，已过时
            }
            else
            {
                Console.WriteLine("已经挂了！");
            }
        }
    }
}
