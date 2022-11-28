using AutoMapper;
using GAT_TaskkResolution.DTO;
using GAT_TaskkResolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GAT_TaskkResolution.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly TaskResolutionContext db;

        public StudentsController()
        {
            db = new TaskResolutionContext();
        }

        public IHttpActionResult Get()
        {
            var data = db.Students.ToList();

            //Mapper.Map<IEnumerable<StudentDTO>>(data);

            return Ok(Mapper.Map<IEnumerable<StudentDTO>>(data));
        }

    }
}
