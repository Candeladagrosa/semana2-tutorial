using System;
using System.ComponentModel.DataAnnotations;

namespace semana2_tutorial.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Genre { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.00, 1000000.00, ErrorMessage = "Price must be between $0.00 and $1,000,000.00")]
        public decimal Price { get; set; }
    }
}