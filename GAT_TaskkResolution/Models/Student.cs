using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GAT_TaskkResolution.Models
{
    public class Student
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        IQueryable<Subject> Subjects { get; set; }
    }
}