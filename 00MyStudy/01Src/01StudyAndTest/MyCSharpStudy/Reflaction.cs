using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpStudy
{
    //反射测试类
    public class ReflactionTest
    {
        //通用测试方法
        public static void Test()
        {
            ReflactionTest test = new ReflactionTest("AdoDotNetTest", "AdoDotNetTest.ThreadTest");
            test.ReflactionMethod("PrintHaHa");
            //test.ReflactionProperty_Get("Index");
            //test.ReflactionProperty_Set("Index", 24);
            //test.ReflactionProperty_Get("Index");
            Console.ReadKey();
        }

        object Instance;
        Type type;

        public ReflactionTest(string assemblyName, string className)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            type = assembly.GetType(className);
            Instance = Activator.CreateInstance(type);
        }

        public void ReflactionMethod(string methodName)
        {
            MethodInfo methodinfo = type.GetMethod(methodName);
            //MethodInfo methodinfo1 = 
            methodinfo.Invoke(Instance, null);
        }

        public void ReflactionProperty_Set(string propertyName, object value)
        {
            PropertyInfo propertyinfo = type.GetProperty(propertyName);
            propertyinfo.SetValue(Instance, value, null);
            Console.WriteLine("{0}的值已经更改成了{1}", propertyName, value);
        }

        public void ReflactionProperty_Get(string propertyName)
        {
            PropertyInfo propertyinfo = type.GetProperty(propertyName);
            var name = propertyinfo.GetValue(Instance, null);
            Console.WriteLine(name.ToString());
        }
    }

}
