using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GAT_TaskkResolution.Models
{
    public class Subject
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(450)]
        public string Code { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}