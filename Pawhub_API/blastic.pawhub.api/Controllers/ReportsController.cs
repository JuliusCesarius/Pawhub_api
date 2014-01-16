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
using blastic.pawhub.service.lostAndFound;
using blastic.pawhub.models;

namespace Pawhub_API.Controllers
{
    public class ReportsController : ApiController
    {
        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        /// 
        [NotImplementedExceptionFilter]
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
        [NotImplementedExceptionFilter]
        public ResponseResult<Report> Get(string id)
        {
            using (var reportsService = new ReportsService())
            {
                var report = reportsService.GetById(ObjectId.Parse(id));

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
        public ResponseResult<IEnumerable<Report>> Get(short pageNumber)
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
        [System.Web.Http.AcceptVerbs("POST")]
        [NotImplementedExceptionFilter]
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
        [NotImplementedExceptionFilter]
        public ResponseResult<Report> Put(Report value)
        {
            Report report;
            var response = Request.CreateResponse<Report>(HttpStatusCode.Created, value);
            using (var reportsService = new ReportsService())
            {
                report = reportsService.GetById(ObjectId.Parse(value._id));
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
        [NotImplementedExceptionFilter]
        public ResponseResult<bool> Delete(string id)
        {
            bool succeed;
            using (var reportsService = new ReportsService())
            {
                succeed = reportsService.Delete(ObjectId.Parse(id));
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
        [Route("lnf/reports/{type}/page/{pageNumber}")]
        public ResponseResult<IEnumerable<Report>> ReportsType(string type, short pageNumber)
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

        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/setAlert/{id}/{userId}/{active}")]
        public ResponseResult<bool> SetAlert(string id, string userId, bool active)
        {
            bool succeed;
            using (var reportsService = new ReportsService())
            {
                succeed = reportsService.Delete(ObjectId.Parse(id));
            }

            return new ResponseResult<bool>
            {
                Messages = new List<string>() { "Metodo aún no implementado" },
                Result = active,
                Succeed = succeed
            };
        }

        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/comment/{id}")]
        public ResponseResult<Comment> SetAlert(string id, [FromBody]Comment comment)
        {
            bool succeed;
            using (var reportsService = new ReportsService())
            {
                succeed = reportsService.Delete(ObjectId.Parse(id));
                comment.date = DateTime.UtcNow;
            }

            return new ResponseResult<Comment>
            {
                Messages = new List<string>() { "Metodo aún no implementado" },
                Result = comment,
                Succeed = succeed
            };
        }

        //[HttpPost]
        //[NotImplementedExceptionFilter]
        //[Route("lnf/reports/comments/{id}")]
        //public ResponseResult<Comment> SetAlert(string id, [FromBody]Comment comment)
        //{
        //    bool succeed;
        //    using (var reportsService = new ReportsService())
        //    {
        //        succeed = reportsService.Delete(ObjectId.Parse(id));
        //        comment.date = DateTime.UtcNow;
        //    }

        //    return new ResponseResult<Comment>
        //    {
        //        Messages = new List<string>() { "Metodo aún no implementado" },
        //        Result = comment,
        //        Succeed = succeed
        //    };
        //}

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
