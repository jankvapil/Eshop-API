using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eshop.GraphQL.Data
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public System.DateTime? OrderDate { get; set; }

        public ICollection<UserOrder> UserOrders { get; set; } =
          new List<UserOrder>();
    }
}