using AutoMapper;
using GAT_TaskResolutionEntity.DTO;
using GAT_TaskResolutionEntity.Models;
using GAT_TaskResolutionUtility.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GAT_TaskResolutionAPI.Controllers
{
    [Route("api/Subjects")]
    public class SubjectsController : ApiController
    {
        private readonly TaskResolutionContext _db;
        private readonly ILogger _log;

        public SubjectsController(TaskResolutionContext db, ILogger log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                _log.LogInfo("Get All");

                var data = _db.Subjects.ToList();
                
                return Ok(Mapper.Map<IEnumerable<SubjectDTO>>(data));
            }
            catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                return InternalServerError();
            }            
        }

    }
}
