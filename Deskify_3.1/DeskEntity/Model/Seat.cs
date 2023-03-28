using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeskEntity.Model
{
    [Table("seats")]
    public class Seat
    {

        [Key]
       
        public int SeatId { get; set; }

        public string SeatNumber { get; set; }

        [ForeignKey("Floor")]
        public int FloorId { get; set; }
        public Floor Floor { get; set; }
    }
}
