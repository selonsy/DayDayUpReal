using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAlgorithmStudy
{
    class Test
    {
        /// <summary>
        /// 求值，递归
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int MyTest(int n)
        {
            if (n < 2)
            {
                return 1;
            }
            else
            {
                return n * MyTest(--n);
            }
        }

        /// <summary>
        /// 1+x+x^2+x^3+...+x^n
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int MyTest1(int x, int n)
        {
            int temp = x + 1;
            for (int i = 0; i < n - 1; i++)
            {
                temp = temp * x + 1;
            }
            return temp;
        }
    }
}
