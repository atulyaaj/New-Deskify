using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeskEntity.Model
{
    [Table("bookingRooms")]
    public class BookingRoom
    {

        [Key]
        public int BookingRoomId { get; set; }
        public int RoomStatus { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public string MeetingHours { get; set; }
        public DateTime MeetingStart { get; set; }
        public DateTime MeetingEnd { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
       
    }
}
