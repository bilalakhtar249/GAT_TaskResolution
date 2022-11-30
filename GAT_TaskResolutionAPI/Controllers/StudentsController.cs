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
    //[EnableCors(origins: "http://localhost:50485/", headers: "*", methods: "*")]
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
            if (string.IsNullOrEmpty(Number))
                return Content(HttpStatusCode.BadRequest, "empty or null parameter");

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

                var data = _db.Students.Include(x => x.Subjects).ToList();
                
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
            if (model == null)
                return Content(HttpStatusCode.BadRequest, "empty or null parameter");
            
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "invalid parameter values");

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
            if (model == null)
                return Content(HttpStatusCode.BadRequest, "empty or null parameter");

            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "invalid parameter values");

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
        public IHttpActionResult Delete(string Number)
        {
            if (string.IsNullOrEmpty(Number))
                return Content(HttpStatusCode.BadRequest, "empty or null parameter");

            try
            {
                _log.LogInfo("Delete: " + Number);

                var dbObject = _db.Students.Where(x => x.Number == Number).Include(x => x.Subjects).FirstOrDefault();
                if (dbObject != null)
                {
                    _db.Students.Remove(dbObject);
                    _db.SaveChanges();

                    return Ok(Mapper.Map<StudentDTO>(dbObject));
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

        [Route("api/Students/{StudentNumber}/AddSubject/{SubjectCode}")]
        [HttpPost]
        public IHttpActionResult AddSubjects(string StudentNumber, string SubjectCode)
        {
            if (string.IsNullOrEmpty(StudentNumber) || string.IsNullOrEmpty(SubjectCode))
                return Content(HttpStatusCode.BadRequest, "empty or null parameter");

            try
            {
                _log.LogInfo("AddSubject: " + SubjectCode);

                var student = _db.Students.Where(x => x.Number == StudentNumber).Include(x => x.Subjects).FirstOrDefault();

                if(student != null)
                { 
                    if(!student.Subjects.Any(x => x.Code == SubjectCode)) //Not already part of subject collection
                    {
                        var subject = _db.Subjects.FirstOrDefault(x => x.Code == SubjectCode);
                        if (subject != null)
                            student.Subjects.Add(subject);
                        else
                            return Content(HttpStatusCode.NotFound, "Subject not found");
                    }
                    else
                    {
                        return Content(HttpStatusCode.Conflict, "A subject with same code already exists");
                    }
                    
                    _db.SaveChanges();
                    return Ok(SubjectCode);
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

        [Route("api/Students/{StudentNumber}/DeleteSubject/{SubjectCode}")]
        [HttpDelete]
        public IHttpActionResult DeleteSubject(string StudentNumber, string SubjectCode)
        {
            if (string.IsNullOrEmpty(StudentNumber) || string.IsNullOrEmpty(SubjectCode))
                return Content(HttpStatusCode.BadRequest, "empty or null parameter");

            try
            {
                _log.LogInfo("DeleteSubject: " + SubjectCode);

                var student = _db.Students.Where(x => x.Number == StudentNumber).Include(x => x.Subjects).FirstOrDefault();

                if (student != null)
                {
                    var subject = student.Subjects.FirstOrDefault(x => x.Code == SubjectCode);

                    if (subject != null && student.Subjects.Any(x => x.Code == subject.Code))
                        student.Subjects.Remove(subject);
                    else
                        return Content(HttpStatusCode.NotFound, "Subject not found");

                    _db.SaveChanges();
                    return Ok(SubjectCode);
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
