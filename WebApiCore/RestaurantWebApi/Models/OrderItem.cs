using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWebApi.Models
{
    [Table("OrderItems")]
    public partial class OrderItem
    {
        [Key]
        public long OrderItemID { get; set; }
        public Nullable<long> OrderID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> Quantity { get; set; }

        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
