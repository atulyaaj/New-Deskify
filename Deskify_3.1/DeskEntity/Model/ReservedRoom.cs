using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeskEntity.Model
{
    [Table("reservedRooms")]
    public class ReservedRoom
    {
        [Key]
        public int ReservedRoomId { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [ForeignKey("BookingRoom")]
        public int BookingRoomId { get; set; }
        public BookingRoom BookingRoom { get; set; }
    }
}
