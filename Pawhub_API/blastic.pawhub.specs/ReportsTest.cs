using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using blastic.pawhub.models.LostAndFound;
using System.Collections.Generic;
using System.Threading.Tasks;
using blastic.pawhub.models.enums;
using blastic.pawhub.models;
using blastic.pawhub.service.core;

namespace blastic.pawhub.specs
{
    [TestClass]
    public class ReportsTest
    {
        private HttpClient client = new HttpClient();

        public ReportsTest()
        {
            client.BaseAddress = new Uri("http://localhost:65073/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TestMethod]
        public void RenameField()
        {
            using (var reportsService = new ReportsService())
            {
                //var fieldsAffected = reportsService.Upgrade("Reports", "adress", "address");

            }
        }

        [TestMethod]
        public void RenameField(bool areYouSure)
        {
            using (var reportsService = new ReportsService())
            {
                reportsService.DropCollection();
            }
        }

        [TestMethod]
        public async Task Get()
        {
            HttpResponseMessage response = client.GetAsync("lnf/reports").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);

            var reportsString = await response.Content.ReadAsStringAsync();
            var reports = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(reportsString, new[] { new { _id = "" } });
            //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
            Assert.IsTrue(reports != null);
            Assert.IsTrue(reports.Length > 0);
        }

        [TestMethod]
        public async Task GetById()
        {
            HttpResponseMessage response = client.GetAsync("lnf/reports/52f80aeaba26192a08a5d883").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);

            var reportsString = await response.Content.ReadAsStringAsync();
            var report = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(reportsString, new { _id = "" });
            //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
            Assert.IsTrue(report != null);
            Assert.IsTrue(report._id == "52f80aeaba26192a08a5d883");
        }

        [TestMethod]
        public async Task GetPaged()
        {
            HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);

            var reportsString = await response.Content.ReadAsStringAsync();
            var reports = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(reportsString, new[] { new { _id = "" } });
            //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
            Assert.IsTrue(reports != null);
            Assert.IsTrue(reports.Length > 0);
        }

        [TestMethod]
        public async Task Post()
        {
            var report = newReport("abuse");
            HttpResponseMessage response = client.PostAsJsonAsync("lnf/reports/", report).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);

            var reportsString = await response.Content.ReadAsStringAsync();
            var reports = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(reportsString, new { _id = "" });
            //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
            Assert.IsTrue(reports != null);
        }

        //[TestMethod]
        //public async Task GetPaged()
        //{
        //    HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //    Assert.IsNotNull(response.Content);

        //    var reportsString = await response.Content.ReadAsStringAsync();
        //    var reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(reportsString);
        //    //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
        //    Assert.IsTrue(reports != null);
        //    Assert.IsTrue(reports.Count > 0);
        //}

        //[TestMethod]
        //public async Task GetPaged()
        //{
        //    HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //    Assert.IsNotNull(response.Content);

        //    var reportsString = await response.Content.ReadAsStringAsync();
        //    var reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(reportsString);
        //    //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
        //    Assert.IsTrue(reports != null);
        //    Assert.IsTrue(reports.Count > 0);
        //}

        //[TestMethod]
        //public async Task GetPaged()
        //{
        //    HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //    Assert.IsNotNull(response.Content);

        //    var reportsString = await response.Content.ReadAsStringAsync();
        //    var reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(reportsString);
        //    //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
        //    Assert.IsTrue(reports != null);
        //    Assert.IsTrue(reports.Count > 0);
        //}

        //[TestMethod]
        //public async Task GetPaged()
        //{
        //    HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //    Assert.IsNotNull(response.Content);

        //    var reportsString = await response.Content.ReadAsStringAsync();
        //    var reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(reportsString);
        //    //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
        //    Assert.IsTrue(reports != null);
        //    Assert.IsTrue(reports.Count > 0);
        //}

        //[TestMethod]
        //public async Task GetPaged()
        //{
        //    HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //    Assert.IsNotNull(response.Content);

        //    var reportsString = await response.Content.ReadAsStringAsync();
        //    var reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(reportsString);
        //    //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
        //    Assert.IsTrue(reports != null);
        //    Assert.IsTrue(reports.Count > 0);
        //}

        //[TestMethod]
        //public async Task GetPaged()
        //{
        //    HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //    Assert.IsNotNull(response.Content);

        //    var reportsString = await response.Content.ReadAsStringAsync();
        //    var reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(reportsString);
        //    //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
        //    Assert.IsTrue(reports != null);
        //    Assert.IsTrue(reports.Count > 0);
        //}

        //[TestMethod]
        //public async Task GetPaged()
        //{
        //    HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //    Assert.IsNotNull(response.Content);

        //    var reportsString = await response.Content.ReadAsStringAsync();
        //    var reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(reportsString);
        //    //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
        //    Assert.IsTrue(reports != null);
        //    Assert.IsTrue(reports.Count > 0);
        //}

        //[TestMethod]
        //public async Task GetPaged()
        //{
        //    HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //    Assert.IsNotNull(response.Content);

        //    var reportsString = await response.Content.ReadAsStringAsync();
        //    var reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(reportsString);
        //    //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
        //    Assert.IsTrue(reports != null);
        //    Assert.IsTrue(reports.Count > 0);
        //}

        //[TestMethod]
        //public async Task GetPaged()
        //{
        //    HttpResponseMessage response = client.GetAsync("lnf/reports/page/1").Result;
        //    Assert.IsTrue(response.IsSuccessStatusCode);
        //    Assert.IsNotNull(response.Content);

        //    var reportsString = await response.Content.ReadAsStringAsync();
        //    var reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(reportsString);
        //    //No puedo hacer el cast a Report porque marca error al deserializar la interfaz IReportDetail
        //    Assert.IsTrue(reports != null);
        //    Assert.IsTrue(reports.Count > 0);
        //}

        

        private Report newReport(string tipo)
        {
            IReportDetail detail = null;
            var contactInfo = new ContactInfo { address = "Conocido", email = new List<string> { "julioavilasaavedra@gmail.com" }, name = "JC", phones = new List<string> { "9999999999" } };
            switch (tipo)
            {
                case "abuse":
                    Abuse abuse = new Abuse { address = "Calle wallabe 124, La conchita", age = "2.5 yo", characteristics = "verde con motas rojas", date = DateTime.Today, name = "floppy" };
                    abuse.isAnonimous = true;
                    detail = abuse;
                    break;
                case "found":
                    Found found = new Found { address = "Calle wallabe 124, La conchita", age = "2.5 yo", characteristics = "verde con motas rojas", date = DateTime.Today, name = "floppy" };
                    found.size = "mid";
                    found.breeds = new List<int>() { 1, 2 };
                    detail = found;
                    break;
                case "lost":
                    var lost = new Lost { address = "Calle wallabe 124, La conchita", age = "2.5 yo", characteristics = "verde con motas rojas", date = DateTime.Today, name = "floppy" };
                    lost.reward = true;
                    lost.size = "large";
                    lost.breeds = new List<int>() { 1, 2 };
                    detail = lost;
                    break;
                case "resque":
                    var resque = new Resque { address = "Calle wallabe 124, La conchita", age = "2.5 yo", characteristics = "verde con motas rojas", date = DateTime.Today, name = "floppy" };
                    resque.contactInfo = contactInfo;
                    detail = resque;
                    break;
            }

            return new Report
            {
                _userId = "522e8aaf18f9bf1f64555555",
                description = "Do you see any Teletubbies in here? Do you see a slender plastic tag clipped to my shirt with my name printed on it? Do you see a little Asian child with a blank expression on his face sitting outside on a mechanical helicopter that shakes when you put quarters in it? No? Well, that's what you see at a toy store. And you must think you're in a toy store, because you're here shopping for an infant named Jeb.",
                location = new Location { lat = "09280980980", lon = "098029834092" },
                type = "dog",
                detail = detail
            };
        }
        
    }
}
