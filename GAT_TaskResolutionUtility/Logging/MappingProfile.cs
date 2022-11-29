using AutoMapper;
using GAT_TaskResolutionEntity.DTO;
using GAT_TaskResolutionEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAT_TaskResolutionUtility.Logging
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();
            CreateMap<Subject, SubjectDTO>();
            CreateMap<SubjectDTO, Subject>();            
        }
    }
}