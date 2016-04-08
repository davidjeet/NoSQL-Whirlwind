using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "admin1234");
            client.Connect();

            var dict = new Dictionary<string, object>();

            string fileName = @"C:\temp\GoTCreate.cql";
            var lines = File.ReadLines(fileName);
            string cql = "";
            foreach (var cypher in lines)
            {
                if (String.IsNullOrEmpty(cypher) || cypher.StartsWith(@"//"))
                {
                    cql = "";
                    continue;
                }

                ////string cypher = "CREATE (:House {name:\"Tyrell\"});";

                cql += cypher;
                if (cypher.EndsWith(";"))
                {
                    Neo4jClient.Cypher.CypherQuery query = new Neo4jClient.Cypher.CypherQuery
                        (cql, dict, Neo4jClient.Cypher.CypherResultMode.Projection);

                    ((IRawGraphClient)client).ExecuteCypher(query);
                    Console.WriteLine(cypher);
                    cql = "";
                } 
            }

            Console.WriteLine("Import Complete!");

        }
    }
}
