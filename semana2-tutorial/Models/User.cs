﻿using System.ComponentModel.DataAnnotations;

namespace semana2_tutorial.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
