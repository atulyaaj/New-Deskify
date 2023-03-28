using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeskEntity.Model
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        [ForeignKey("LoginTable")]
        public int EmployeeID { get; set; }

        public LoginTable LoginTable { get; set; }

        public string EmployeeName { get; set; }

        public int EmployeeNumber { get; set; }

        public string Role { get; set; }

        public byte[] Image { get; set; }

        [StringLength(12, MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "char(1)")]
        public char Gender { get; set; }

        public string SecurityQuestion { get; set; }
    }
}
