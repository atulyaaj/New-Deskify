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
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
