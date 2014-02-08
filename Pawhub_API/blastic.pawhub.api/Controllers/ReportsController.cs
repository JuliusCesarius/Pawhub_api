using blastic.pawhub.models.LostAndFound;
using blastic.pawhub.service;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using Pawhub_API.Models;
using AutoMapper;
using Pawhub_API.Models.Filters;
using blastic.pawhub.service.core;
using blastic.pawhub.models;

namespace Pawhub_API.Controllers
{
    public class ReportsController : ApiController
    {
        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports")]
        public ResponseResult<IEnumerable<Report>> Get()
        {
            using (var reportsService = new ReportsService())
            {
                var reports = reportsService.Get(null, -1, 0);

                return new ResponseResult<IEnumerable<Report>>
                {
                    Messages = new List<string>() { "OK" },
                    Result = reports,
                    Succeed = true
                };
            }
        }

        /// <summary>
        /// Gets a specific Report by Id
        /// </summary>
        /// <returns>Report object</returns>
        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/{id:regex(^[0-9a-fA-F]{24}$)}")]
        public ResponseResult<Report> Get(string id)
        {
            using (var reportsService = new ReportsService())
            {                
                var report = reportsService.GetById(id);
                //Verifica que el usuario exista. De ahí toma el userName para armar en el objeto
                var user = new UsersService().GetById(report._userId);
                if (user == null)
                {
                    throw new Exception("User report does not exist");
                }
                report.userName = user.uname;
                return new ResponseResult<Report>
                {
                    Messages = new List<string>() { "OK" },
                    Result = report,
                    Succeed = true
                };
            }
        }

        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        [NotImplementedExceptionFilter]
        public ResponseResult<IEnumerable<Report>> Get(int pageNumber)
        {
            IEnumerable<Report> reports = null;
            using (var reportsService = new ReportsService())
            {
                //TODO parametrizar el pageSize
                reports = reportsService.Get(null, pageNumber, 20);
            }
            return new ResponseResult<IEnumerable<Report>>
            {
                Messages = new List<string>() { "OK" },
                Result = reports,
                Succeed = true
            };
        }

        /// <summary>
        /// Register a new Report
        /// </summary>
        /// <returns>Id of the new report</returns>
        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/")]
        public ResponseResult<Report> Post([FromBody] Report value)
        {
            using (var reportsService = new ReportsService())
            {
                reportsService.Save((Report)value);
            }

            return new ResponseResult<Report>
            {
                Messages = new List<string>() { "OK" },
                Result = value,
                Succeed = true
            };
        }

        /// <summary>
        /// Update the information of the specified Report
        /// </summary>
        /// <returns>Succeed status</returns>
        [HttpPut]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/")]
        public ResponseResult<Report> Put(Report value)
        {
            Report report;
            var response = Request.CreateResponse<Report>(HttpStatusCode.Created, value);
            using (var reportsService = new ReportsService())
            {
                report = reportsService.GetById(value._id);
                if (report != null)
                {
                    report = Mapper.Map(value, report);
                    reportsService.Update(report);
                    return new ResponseResult<Report>
                    {
                        Messages = new List<string>() { "OK" },
                        Result = report,
                        Succeed = true
                    };
                }
                else
                {
                    return new ResponseResult<Report>
                    {
                        Result = value,
                        Succeed = false,
                        Errors = new List<string>() { "Report could not be found" }
                    };
                }
            }

        }

        /// <summary>
        /// Delete the specified Report
        /// </summary>
        /// <returns>Succeed status</returns>
        [HttpDelete]
        [Route("lnf/reports/{id:regex(^[0-9a-fA-F]{24}$)}")]
        [NotImplementedExceptionFilter]
        public ResponseResult<bool> Delete(string id)
        {
            bool succeed;
            using (var reportsService = new ReportsService())
            {
                succeed = reportsService.Delete(id);
            }

            return new ResponseResult<bool>
            {
                Messages = new List<string>() { "OK" },
                Result = succeed,
                Succeed = succeed
            };
        }

        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        [HttpGet]
        [Route("lnf/{controller}/{type}")]
        [NotImplementedExceptionFilter]
        public ResponseResult<IEnumerable<Report>> ReportsType(string type)
        {
            return ReportsType(type, 0);
        }

        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/{type}/page/{pageNumber:int:min(1)?}")]
        public ResponseResult<IEnumerable<Report>> ReportsType(string type, int pageNumber)
        {
            using (var reportsService = new ReportsService())
            {
                //TODO Parametrizar el pageSize
                var reports = reportsService.Get(type, pageNumber, 20);

                return new ResponseResult<IEnumerable<Report>>
                {
                    Messages = new List<string>() { "OK" },
                    Result = reports,
                    Succeed = true
                };
            }
        }

        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/user/{id}/{pageNumber:int:min(1)?}")]
        public ResponseResult<IEnumerable<Report>> GetByUserId(string id, int pageNumber=1)
        {
            
            IEnumerable<Report> reports;
            using (var reportsService = new ReportsService())
            {
                reports = reportsService.GetByUserId(id, pageNumber);
            }

            return new ResponseResult<IEnumerable<Report>>
            {
                Result = reports,
                Succeed = true
            };
        }

        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/comment/{id}")]
        public ResponseResult<Comment> Comment(string id, [FromBody]Comment comment)
        {
            Comment result;
            if (comment == null || string.IsNullOrWhiteSpace(comment._userId) || string.IsNullOrWhiteSpace(comment.content))
            {
                throw new Exception("Information incompleted");
            }
            using (var reportsService = new ReportsService())
            {
                result = reportsService.Comment(id, comment);
                comment.date = DateTime.UtcNow;
            }

            return new ResponseResult<Comment>
            {
                Result = result,
                Succeed = true
            };
        }

        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/setAlert/{id}")]
        public ResponseResult<UserAlert> SetAlert(string id, [FromBody]UserAlert userAlert)
        {
            UserAlert result;

            using (var reportsService = new ReportsService())
            {
                result = reportsService.SetAlert(id, userAlert);
            }

            return new ResponseResult<UserAlert>
            {
                Result = result,
                Succeed = true
            };
        }

        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/setViewed/{id}")]
        public ResponseResult<long> SetViewed(string id, [FromBody]string userId)
        {
            long result;

            using (var reportsService = new ReportsService())
            {
                result = reportsService.SetView(id, userId);
            }

            return new ResponseResult<long>
            {
                Result = result,
                Succeed = true
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private Report newReport()
        {
            return new Report { _id = ObjectId.GenerateNewId().ToString(), detail = new Resque { name = "Abuso", date = DateTime.Now } };
        }
    }
}
