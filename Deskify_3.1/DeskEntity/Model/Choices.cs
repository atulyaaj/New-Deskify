using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DeskEntity.Model
{
    [Table("choices")]
    public class Choices
    {
        [Key]
        public int ChoiceId { get; set; }
        public string FoodPerferences { get; set; }
        public string Data { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
