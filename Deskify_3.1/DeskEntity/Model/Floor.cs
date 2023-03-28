using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeskEntity.Model
{
    [Table("floors")]
    public class Floor
    {

        [Key]
        
        public int FloorId { get; set; }

        public string FloorName { get; set; }
    }
}
