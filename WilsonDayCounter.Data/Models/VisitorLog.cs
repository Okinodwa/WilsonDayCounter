using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WilsonDayCounter.Data.Models
{
    public class VisitorLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTime DateEntered { get; }
    }
}
