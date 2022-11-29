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

                var student = _db.Students.Where(x => x.Number == model.Number).Include(x => x.Subjects).FirstOrDefault();
                if (student != null)
                {
                    DbEntityEntry<Student> stud = _db.Entry(student);
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

        [Route("api/Students/{StudentNumber}/AddSubjects")]
        [HttpPost]
        public IHttpActionResult AddSubjects(string StudentNumber, ICollection<SubjectDTO> model)
        {
            try
            {
                _log.LogInfo("AddSubjects: " + model.Count());

                var student = _db.Students.Where(x => x.Number == StudentNumber).Include(x => x.Subjects).FirstOrDefault();

                if(student != null)
                { 
                    foreach(var item in model)
                    {
                        if(!student.Subjects.Any(x => x.Code == item.Code)) //Not already part of subject collection
                        {
                            var subject = _db.Subjects.FirstOrDefault(x => x.Code == item.Code);
                            if (subject != null)
                                student.Subjects.Add(subject);
                        }
                    }
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

        [Route("api/Students/{StudentNumber}/DeleteSubjects")]
        [HttpDelete]
        public IHttpActionResult DeleteSubjects(string StudentNumber, ICollection<SubjectDTO> model)
        {
            try
            {
                _log.LogInfo("DeleteSubjects: " + model.Count());

                var student = _db.Students.Where(x => x.Number == StudentNumber).Include(x => x.Subjects).FirstOrDefault();

                if (student != null)
                {
                    foreach (var item in model)
                    {
                        var subject = student.Subjects.FirstOrDefault(x => x.Code == item.Code);
                        if (subject != null)
                            student.Subjects.Remove(subject);
                    }
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
