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
using blastic.pawhub.api.Models.Helpers;
using System.Configuration;

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
        public IHttpActionResult Get()
        {
            using (var reportsService = new ReportsService())
            {
                var reports = reportsService.Get(null, -1, 0);

                return Ok(reports);
            }
        }

        /// <summary>
        /// Gets a specific Report by Id
        /// </summary>
        /// <returns>Report object</returns>
        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/{id:regex(^[0-9a-fA-F]{24}$)}")]
        public IHttpActionResult Get(string id)
        {
            using (var reportsService = new ReportsService())
            {
                var report = reportsService.GetById(id);
                if (report == null) throw new Exception("Report does not exist");
                //Verifica que el usuario exista. De ahí toma el userName para armar en el objeto
                var user = new UsersService().GetById(report._userId);
                if (user == null) throw new Exception("User report does not exist");
                report.userName = user.uname;
                return Ok(report);
            }
        }

        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        [NotImplementedExceptionFilter]
        public IHttpActionResult Get(int pageNumber)
        {
            IEnumerable<Report> reports = null;
            using (var reportsService = new ReportsService())
            {
                //TODO parametrizar el pageSize
                reports = reportsService.Get(null, pageNumber, 20);
            }
            return Ok(reports);
        }

        /// <summary>
        /// Register a new Report
        /// </summary>
        /// <returns>Id of the new report</returns>
        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/")]
        public IHttpActionResult Post([FromBody] Report value)
        {
            if (value.detail == null)
            {
                return BadRequest("Report Detail not supplied");
            }
            using (var reportsService = new ReportsService())
            {
                value._id = null;
                reportsService.Save((Report)value);
            }

            return Ok((Report)value);
        }

        /// <summary>
        /// Update the information of the specified Report
        /// </summary>
        /// <returns>Succeed status</returns>
        [HttpPut]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/")]
        public IHttpActionResult Put(Report value)
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
                    return Ok(report);
                }
                else
                {
                    return BadRequest("Report could not be found");
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
        public IHttpActionResult Delete(string id)
        {
            bool succeed;
            using (var reportsService = new ReportsService())
            {
                succeed = reportsService.Delete(id);
            }

            return Ok(succeed);
        }

        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        [HttpGet]
        [Route("lnf/reports/{type}")]
        [NotImplementedExceptionFilter]
        public IHttpActionResult ReportsType(string type)
        {
            return ReportsType(type, 0);
        }

        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/{type}/page/{pageNumber:int:min(1)?}")]
        public IHttpActionResult ReportsType(string type, int pageNumber)
        {
            using (var reportsService = new ReportsService())
            {
                //TODO Parametrizar el pageSize
                var reports = reportsService.Get(type, pageNumber, 20);

                return Ok(reports);
            }
        }

        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/user/{id}/{pageNumber:int:min(1)?}")]
        public IHttpActionResult GetByUserId(string id, int pageNumber = 1)
        {
            
            IEnumerable<Report> reports;
            using (var reportsService = new ReportsService())
            {
                reports = reportsService.GetByUserId(id, pageNumber);
            }

            return Ok(reports);
        }

        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/comment/{id}")]
        public IHttpActionResult Comment(string id, [FromBody]Comment comment)
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

            //Perform push call
            string deviceId = ConfigurationManager.AppSettings.Get("GCMApi");
            if (string.IsNullOrEmpty(deviceId)) { return InternalServerError(new  Exception("No GCMApi configured")); }

            var data = new
            {
                registration_ids = new string[] { "001122334455" },
                data = result
            };
            string response = new PushServiceHelper().SendGCMNotification(deviceId, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            
            return Ok(result);
        }

        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/setAlert/{id}")]
        public IHttpActionResult SetAlert(string id, [FromBody]UserAlert userAlert)
        {
            UserAlert result;

            using (var reportsService = new ReportsService())
            {
                result = reportsService.SetAlert(id, userAlert);
            }

            return Ok(userAlert);
        }

        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("lnf/reports/setViewed/{id}")]
        public IHttpActionResult SetViewed(string id, [FromBody]string userId)
        {
            long result;

            using (var reportsService = new ReportsService())
            {
                result = reportsService.SetView(id, userId);
            }

            return Ok(result);
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
