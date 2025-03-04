﻿using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.Application.Customers.DTOs
{
    public class CustomerRequestDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
