using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devin
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// 判断异常是哪个异常类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool Is<T>(this Exception source) where T : Exception
        {
            if (source is T)
            {
                return true;
            }
            else if (source.InnerException != null)
            {
                return source.InnerException.Is<T>();
            }
            else
            {
                return false;
            }
        }
    }
}
