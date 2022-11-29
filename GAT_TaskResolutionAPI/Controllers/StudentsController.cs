using AutoMapper;
using GAT_TaskkResolution.DTO;
using GAT_TaskkResolution.Models;
using GAT_TaskkResolution.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace GAT_TaskResolutionAPI.Controllers
{
    [Route("api/Students")]
    public class StudentsController : ApiController
    {
        private readonly TaskResolutionContext _db;
        private readonly ILogger _log;

        public StudentsController(TaskResolutionContext db, ILogger log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public IHttpActionResult Get(string Number)
        {
            try
            {

                var data = _db.Students.Where(x => x.Number == Number).Include(x => x.Subjects).FirstOrDefault();

                if (data != null)
                    return Ok(Mapper.Map<StudentDTO>(data));
                else
                    return Content(HttpStatusCode.NotFound, "Student not found");
            }
            catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                _log.LogInfo("Get All");

                var data = _db.Students.ToList();
                
                return Ok(Mapper.Map<IEnumerable<StudentDTO>>(data));
            }
            catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                return InternalServerError();
            }            
        }

        [HttpPost]
        public IHttpActionResult Post(StudentDTO model)
        {
            try
            {
                _log.LogInfo("Post: " + model.Number);

                var isDuplicate = _db.Students.Where(x => x.Number == model.Number).Count() > 0;

                if (!isDuplicate)
                {
                    
                    _db.Students.Add(Mapper.Map<Student>(model));
                    _db.SaveChanges();
                    return Ok(model);
                }
                else
                {
                    return Content(HttpStatusCode.Conflict, "A student with same number already exists");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return InternalServerError();
            }            
        }

        [HttpPut]
        public IHttpActionResult Put(StudentDTO model)
        {
            try
            {
                _log.LogInfo("Put: " + model.Number);

                var dbObject = _db.Students.Where(x => x.Number == model.Number).FirstOrDefault();
                if (dbObject != null)
                {
                    DbEntityEntry<Student> stud = _db.Entry(dbObject);
                    stud.CurrentValues.SetValues(model);
                    _db.SaveChanges();

                    return Ok(model);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Student not found");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return InternalServerError();
            }            
        }

        [HttpDelete]
        public IHttpActionResult Delete(StudentDTO model)
        {
            try
            {
                _log.LogInfo("Delete: " + model.Number);

                var dbObject = _db.Students.Where(x => x.Number == model.Number).Include(x => x.Subjects).FirstOrDefault();
                if (dbObject != null)
                {
                    foreach(var item in dbObject.Subjects)
                    {
                        dbObject.Subjects.Remove(item);
                    }
                    _db.Students.Remove(dbObject);
                    _db.SaveChanges();

                    return Ok(model);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Student not found");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return InternalServerError();
            }            
        }

    }
}
