using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeskEntity.Model
{
    [Table("receptionists")]
    public class Receptionist
    {

        [Key]
        public int ReceptionistID { get; set; }
        public string RName { get; set; }
     
        public string REmail { get; set; }
       
        public string RPassword { get; set; }

        [ForeignKey("BookingSeat")]
        public int SeatId { get; set; }
        public BookingSeat BookingSeat { get; set; }
    }
}
