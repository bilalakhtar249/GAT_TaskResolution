using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAT_TaskResolutionEntity.DTO
{
    public class StudentDTO
    {
        public string Number { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<SubjectDTO> Subjects { get; set; }
    }
}