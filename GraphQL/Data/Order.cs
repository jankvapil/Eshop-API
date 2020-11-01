using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eshop.GraphQL.Data
{
    public class Order
    {
        public int Id { get; set; }

        // [Required]
        // [StringLength(200)]
        // public string? FirstName { get; set; }

        [Required]
        public System.DateTime? OrderDate { get; set; }

        // [Required]
        // [StringLength(200)]
        // public string? UserName { get; set; }

        // [StringLength(256)]
        // public string? EmailAddress { get; set; }

        // public ICollection<SessionAttendee> SessionsAttendees { get; set; } =
        //     new List<SessionAttendee>();
    }
}