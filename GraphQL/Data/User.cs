using System.ComponentModel.DataAnnotations;

namespace Eshop.GraphQL.Data
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        [Required]
        [StringLength(200)]
        public string? Email { get; set; }
    }
}