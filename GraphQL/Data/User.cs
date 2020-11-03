using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eshop.GraphQL.Data
{
    public class User
    {
        public System.Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        [Required]
        [StringLength(200)]
        public string? Email { get; set; }

        public ICollection<UserOrder> UserOrders { get; set; } =
            new List<UserOrder>();
    }
}