using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpStudy
{
    class HashTable
    {
            static void Main(string[] args)
            {
                // 创建一个Hashtable实例
                Hashtable ht = new Hashtable();

                // 添加keyvalue键值对
                ht.Add("A", "1");
                ht.Add("B", "2");
                ht.Add("C", "3");
                ht.Add("D", "4");

                // 遍历哈希表
                foreach (DictionaryEntry de in ht)
                {
                    Console.WriteLine("Key -- {0}; Value --{1}.", de.Key, de.Value);
                }

                // 哈希表排序
                ArrayList akeys = new ArrayList(ht.Keys);
                akeys.Sort();
                foreach (string skey in akeys)
                {
                    Console.WriteLine("{0, -15} {1, -15}", skey, ht[skey]);

                }

                // 判断哈希表是否包含特定键,其返回值为true或false
                if (ht.Contains("A"))
                    Console.WriteLine(ht["A"]);

                // 给对应的键赋值
                ht["A"] = "你好";

                // 移除一个keyvalue键值对
                ht.Remove("C");

                // 移除所有元素
                ht.Clear();

                // 此处将不会有任何输出
                Console.WriteLine(ht["A"]);
                Console.ReadKey();
            }
        }    
}
