using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eshop.GraphQL.Data
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        public float? Price { get; set; }

        [Required]
        [StringLength(50)]
        public string? Type { get; set; }

        [Required]
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(500)]
        public string? ImgUrl { get; set; }
    }
}