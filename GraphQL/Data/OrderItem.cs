using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eshop.GraphQL.Data
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order? Order { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Required]
        public int? Count { get; set; }

        // public ICollection<Product> Products { get; set; } =
        //     new List<Product>();
    }
}