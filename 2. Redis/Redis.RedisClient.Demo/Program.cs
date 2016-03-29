using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redis.Client.Demo;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using static System.Console;

namespace Redis.Client.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //NotTyped();
            StronglyTyped();
            ReadLine();
        }

        static void NotTyped()
        {
            using (IRedisClient client = new RedisClient())
            {
                var customerNames = client.Lists["urn:customernames"];
                customerNames.Clear();
                customerNames.Add("Joe");
                customerNames.Add("Mary");
                customerNames.Push("Bob");
            }

            using (IRedisClient client = new RedisClient())
            {
                var customerNames = client.Lists["urn:customernames"];
                customerNames.ToList().ForEach(c => WriteLine($"Customer: {c}"));
            }
        }

        static void StronglyTyped()
        {
            using (IRedisClient client = new RedisClient())
            {
                var customerNames = client.Lists["urn:customernames"];
                customerNames.Clear();
                customerNames.Add("Joe");
                customerNames.Add("Mary");
                customerNames.Push("Bob");

            }

            long lastId = 0;

            using (var client = new RedisClient())
            {
                IRedisTypedClient<Customer> customerClient = client.As<Customer>();
                var customer = new Customer
                {
                    Id = customerClient.GetNextSequence(),
                    Address = "123 Main St",
                    Name = "Bob Green",
                    Orders = new List<Order>
                                { new Order { OrderNumber = "AB123" } ,
                                  new Order { OrderNumber = "CD345" } }
                };

                var storedCustomer = customerClient.Store(customer);
                lastId = storedCustomer.Id;
            }

            using (var client = new RedisClient())
            {
                var customerClient = client.As<Customer>();
                var customer = customerClient.GetById(lastId);

                WriteLine($"Got customer {customer.Id} with name {customer.Name}.");
            }

        }
    }
}
