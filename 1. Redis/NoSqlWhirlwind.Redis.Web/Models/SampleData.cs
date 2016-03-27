using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoSqlWhirlwind.Redis.Web.Models
{
    public static class SampleData
    {
        private static IList<Product> products = new List<Product>
        {
            new Product
            {
                Id = 100,
                ProductImageUrl = "~/Content/images/uranium-ore.jpg",
                Name = "Uranium Ore",
                Price = 39.95M,
                Description = "Radioactive sample of uranium ore. The ore sample material is Naturally Occurring Radioactive Materials (NORM). Counts Per Minute (CPM) activity rate listed on the label is determined using a GCA-07W Digital Geiger Counter with an NRC certification. Activity level includes all radiation types: alpha, beta and gamma. Uranium Ore samples are useful for testing Geiger Counters."               
            },
            new Product
            {
                Id = 200,
                ProductImageUrl = "~/Content/images/huge-ships.jpg",
                Name = "Book: How to Avoid Huge Ships",
                Price = 113.74M,
                Description = "1993 paperback book on how to avoid huge ships.99 pages with instructions, diagrams and charts. last page has drawing drawn on it. otherwise, clean edgewear."
            },
            new Product
            {
                Id = 300,
                ProductImageUrl = "~/Content/images/three-wolf.jpg",
                Name = "Three Wolf Moon Short Sleeve Tee",
                Price = 44.99M,
                Description = "100% Cotton. Made in US. Machine Wash. 100% cotton. Made in USA. Exceptional artwork on a tee shirt. Machine Wash."
            },

            };

        public static Dictionary<long, Product> Products = new Dictionary<long, Product>
        {
            { 100, products[0] },
            { 200, products[1] },
            { 300, products[2] }
        };


    }
}