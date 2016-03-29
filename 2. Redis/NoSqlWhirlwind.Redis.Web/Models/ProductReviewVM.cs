using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoSqlWhirlwind.Redis.Web.Models
{
    public class ProductReviewVM : Review
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
    }
}