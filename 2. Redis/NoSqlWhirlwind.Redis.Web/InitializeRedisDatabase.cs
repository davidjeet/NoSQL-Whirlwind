using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoSqlWhirlwind.Redis.Web.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace NoSqlWhirlwind.Redis.Web
{
    public static class InitializeRedisDatabase
    {
        public static void Seed()
        {
            using (IRedisClient client = new RedisClient())
            {
                var productsClient = client.Lists["urn:product"];
                productsClient.Clear();
            }

            using (IRedisClient client = new RedisClient())
            {
                IRedisTypedClient<Product> productsClient = client.As<Product>();
                IEnumerable<Product> products = SampleData.Products.Values.AsEnumerable<Product>();
                productsClient.StoreAll(products);
            }
        }
    }
}