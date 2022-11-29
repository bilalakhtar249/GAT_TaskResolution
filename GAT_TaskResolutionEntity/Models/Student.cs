using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GAT_TaskResolutionEntity.Models
{
    public class Student
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(450)]
        public string Number { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}