﻿using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connect to the demo keyspace on our cluster running at 127.0.0.1
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("demo");

            // Insert Bob
            session.Execute("insert into users (lastname, age, city, email, firstname) values ('Jones', 35, 'Austin', 'bob@example.com', 'Bob')");

            // Read Bob's information back and print to the console
            Row result = session.Execute("select * from users where lastname='Jones'").First();
            Console.WriteLine("{0} {1}", result["firstname"], result["age"]);

            // Update Bob's age and then read it back and print to the console
            session.Execute("update users set age = 36 where lastname = 'Jones'");
            result = session.Execute("select * from users where lastname='Jones'").First();
            Console.WriteLine("{0} {1}", result["firstname"], result["age"]);

            // Delete Bob, then try to read all users and print them to the console
            session.Execute("delete from users where lastname = 'Jones'");

            RowSet rows = session.Execute("select * from users");
            foreach (Row row in rows)
                Console.WriteLine("{0} {1}", row["firstname"], row["age"]);

            // Wait for enter key before exiting
            Console.ReadLine();



        }
    }
}
