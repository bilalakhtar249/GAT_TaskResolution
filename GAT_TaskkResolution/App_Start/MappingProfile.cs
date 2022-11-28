using AutoMapper;
using GAT_TaskkResolution.DTO;
using GAT_TaskkResolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAT_TaskkResolution.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDTO>();
        }
    }
}