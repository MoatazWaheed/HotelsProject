﻿using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdRooms { get; set; }

        [Required]
        public int IHotels { get; set; }

        [Required]
        public int IdRoomsDetails { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
