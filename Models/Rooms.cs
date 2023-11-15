﻿using System.ComponentModel.DataAnnotations;
namespace Hotels.Models
{
    public class Rooms
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Images { get; set; }

        [Required]
        public int RoomNo { get; set; }
        public int IdHotel { get; set; }
    }
}
