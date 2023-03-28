using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeskEntity.Model
{
    [Table("LoginTables")]
    public class LoginTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Password { get; set; }
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Email { get; set; }
        [Required]
        public int Type { get; set; }
    }
}
