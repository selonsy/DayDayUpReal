using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTest
{
    public class Redis
    {
        public static void Test()
        {
            //连接
            RedisClient redisClient = new RedisClient("127.0.0.1", 6379);    //redis服务IP和端口

            //赋值
            redisClient.Set("myKey","heheda");

            //取值
            string value = redisClient.Get<string>("myKey");

            Console.WriteLine(value);
            Console.ReadKey();
        }
    }
}
