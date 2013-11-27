using blastic.pawhub.models.LostAndFound;
using blastic.pawhub.service;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Pawhub_API.Controllers
{
    public class ReportsController : ApiController
    {
        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        public IEnumerable<Report> Get()
        {
            using (var lostAndFoundService = new LostAndFoundService())
            {
                var reports = lostAndFoundService.GetReports(-1);
                return reports;
            }
        }

        /// <summary>
        /// Gets a specific Report by Id
        /// </summary>
        /// <returns>Report object</returns>
        public Report Get(string id)
        {
            using (var lostAndFoundService = new LostAndFoundService())
            {
                var report = lostAndFoundService.GetReportById(id);
                return report;
            }
        }

        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        public IEnumerable<Report> Get(short pageSize)
        {
            var Reports = new List<Report>();
            for (var i = 0; i < pageSize; i++)
            {
                Reports.Add(newReport());
            }
            return Reports;
        }

        /// <summary>
        /// Register a new Report
        /// </summary>
        /// <returns>Id of the new report</returns>
        public void Post(Report value)
        {
        }

        /// <summary>
        /// Update the information of the specified Report
        /// </summary>
        /// <returns>Succeed status</returns>
        public void Put(string id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Delete the specified Report
        /// </summary>
        /// <returns>Succeed status</returns>
        public void Delete(string id)
        {
        }

        /// <summary>
        /// Gets all the reports paged. If the page is not supplied, it will return the page 0
        /// </summary>
        /// <returns>Collection of Reports</returns>
        [HttpGet]
        public IEnumerable<Report> ReportsType(string type)
        {
            return ReportsType(type, null);
        }

        [HttpGet]
        public IEnumerable<Report> ReportsType(string type, short pageSize)
        {
            var Reports = new List<Report>();
            for (var i = 0; i < pageSize; i++)
            {
                Reports.Add(newReport());
            }
            return Reports;
        }

        [HttpGet]
        public IEnumerable<Report> ReportsType(string type, string id)
        {
            return new Report[] { newReport() };
        }

        private Report newReport()
        {
            return new Report { _id = ObjectId.GenerateNewId().ToString(), detail = new Abuse { name = "Abuso", dateEvent = DateTime.Now } };
        }
    }
}
