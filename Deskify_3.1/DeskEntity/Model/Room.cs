using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeskEntity.Model
{
    [Table("rooms")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }

        [ForeignKey("Floor")]
        public int FloorId { get; set; }

        public Floor Floor { get; set; }
    }
}
