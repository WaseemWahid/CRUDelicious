using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Post
    {
        [Key] // Primary Key
        public int DishId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Must be more than 2 characters.")]
        [MaxLength(45, ErrorMessage = "Must be lass than 45 characters.")]
        public string Name { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Must be more than 2 characters.")]
        [MaxLength(45, ErrorMessage = "Must be lass than 45 characters.")]
        public string Chef { get; set; }

        [Required]
        public int Tastiness { get; set; }

        [Required]
        public int Calories { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Must be more than 10 characters.")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}