using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using static System.Console;

namespace Redis.SimpleDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ////"localhost:6379"

            using (IRedisNativeClient client = new RedisClient())
            {
                client.Set("urn:messages:1", Encoding.UTF8.GetBytes("Hello C# World!"));
            }

            using (IRedisNativeClient client = new RedisClient())
            {
                var result = Encoding.UTF8.GetString(client.Get("urn:messages:1"));
                WriteLine($"Message: {result}");
            }

            ReadLine();
        }
    }
}
