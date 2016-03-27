using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoSqlWhirlwind.Redis.Web.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace NoSqlWhirlwind.Redis.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.RecentReviews = this.RecentReviews();
            return View(SampleData.Products.Values.AsEnumerable());
        }

        [HttpGet]
        public ActionResult AddReview(long Id)
        {
            ProductReviewVM vm = new ProductReviewVM
            {
                ProductId = Id,
                ParentId = Id,
                Product = SampleData.Products[Id],
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddReview(Review review)
        {
            // Redis add review to queue here:
            using (var client = new RedisClient())
            {
                IRedisTypedClient<Review> reviewClient = client.As<Review>();
                IRedisList<Review> reviewsForProduct = reviewClient.Lists["urn:reviews:" + review.ParentId];
                IRedisList<Review> recentReviews = reviewClient.Lists["urn:recentreviews"];

                reviewsForProduct.Push(review); // review per product

                recentReviews.Enqueue(review);  // reviews of last 5 for all reviews
                while (recentReviews.Count > 5)
                {
                    recentReviews.Dequeue();
                }
            }

            return RedirectToAction("AddReview", "Home", new { Id = review.ParentId });
        }

        [HttpGet]
        public ActionResult AllReviewsPerProduct(long Id)
        {
            IRedisList<Review> reviewsForProduct;
            using (var client = new RedisClient())
            {
                IRedisTypedClient<Review> reviewClient = client.As<Review>();
                reviewsForProduct = reviewClient.Lists["urn:reviews:" + Id];
            }
            ViewBag.ProductName = SampleData.Products[Id].Name;
            return View(reviewsForProduct);
        }


        private IEnumerable<Review> RecentReviews()
        {
            IRedisList<Review> recentReviews;
            using (var client = new RedisClient())
            {
                IRedisTypedClient<Review> reviewClient = client.As<Review>();
                recentReviews = reviewClient.Lists["urn:recentreviews"];
            }

            return recentReviews;
        }


    }
}