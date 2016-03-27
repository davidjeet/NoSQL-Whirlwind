using System.ComponentModel;

namespace NoSqlWhirlwind.Redis.Web.Models
{
    public class Review
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public byte Rating { get; set; }
        [DisplayName("Review")]
        public string ReviewText { get; set; }
    }
}