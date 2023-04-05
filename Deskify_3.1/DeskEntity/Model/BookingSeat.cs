using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeskEntity.Model
{
    [Table("bookingSeats")]
    public class BookingSeat
    {
        [Key]
        public int BookingSeatId { get; set; }
        public int SeatStatus { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public string SeatShiftTime { get; set; }
        public TimeZone ShiftStart { get; set; }
        public TimeZone ShiftEnd { get; set; }
        public string bookingrequesttype { get; set; }

        [ForeignKey("Seat")]
        public int SeatId { get; set; }
        public Seat Seat { get; set; }  

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
