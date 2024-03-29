﻿using blastic.pawhub.models.LostAndFound;
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

namespace Pawhub_API.Controllers
{
    public class ReportsController : ApiController
    {
        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        /// 
        [System.Web.Http.AcceptVerbs("POST", "GET")]
        [NotImplementedExceptionFilter]
        public ResponseResult<IEnumerable<Report>> Get()
        {
            using (var lostAndFoundService = new LostAndFoundService())
            {
                var reports = lostAndFoundService.GetReports(null, -1, 0);

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
            using (var lostAndFoundService = new LostAndFoundService())
            {
                var report = lostAndFoundService.GetReportById(ObjectId.Parse(id));

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
            using (var lostAndFoundService = new LostAndFoundService())
            {
                //TODO parametrizar el pageSize
                reports = lostAndFoundService.GetReports(null, pageNumber, 20);
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
        [System.Web.Http.AcceptVerbs("POST")]
        [NotImplementedExceptionFilter]
        public ResponseResult<Report> Post([FromBody] Report value)
        {
            using (var lostAndFoundService = new LostAndFoundService())
            {
                lostAndFoundService.SaveReport((Report)value);
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
        [System.Web.Http.AcceptVerbs("PUT")]
        [NotImplementedExceptionFilter]
        public ResponseResult<Report> Put(Report value)
        {
            Report report;
            var response = Request.CreateResponse<Report>(HttpStatusCode.Created, value);
            using (var lostAndFoundService = new LostAndFoundService())
            {
                report = lostAndFoundService.GetReportById(ObjectId.Parse(value._id));
                if (report != null)
                {
                    report = Mapper.Map(value, report);
                    lostAndFoundService.UpdateReport(report);
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
            using (var lostAndFoundService = new LostAndFoundService())
            {
                succeed = lostAndFoundService.Delete(ObjectId.Parse(id));
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
            using (var lostAndFoundService = new LostAndFoundService())
            {
                //TODO Parametrizar el pageSize
                var reports = lostAndFoundService.GetReports(type, pageNumber, 20);

                return new ResponseResult<IEnumerable<Report>>
                {
                    Messages = new List<string>() { "OK" },
                    Result = reports,
                    Succeed = true
                };
            }
        }

        //[HttpGet]
        //public ResponseResult<IEnumerable<Report>> ReportsType(string type, string id)
        //{
        //    return new Report[] { newReport() };
        //}

        private Report newReport()
        {
            return new Report { _id = ObjectId.GenerateNewId().ToString(), detail = new Resque { name = "Abuso", date = DateTime.Now } };
        }
    }
}
