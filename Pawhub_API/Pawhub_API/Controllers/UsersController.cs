using blastic.pawhub.models.Register;
using blastic.pawhub.service;
using Pawhub_API.Models;
using Pawhub_API.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pawhub_API.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/users/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/users
        [System.Web.Http.AcceptVerbs("POST")]
        [NotImplementedExceptionFilter]
        public ResponseResult<BasicUser> Post([FromBody] BasicUser value)
        {
            using (var lostAndFoundService = new LostAndFoundService())
            {
                lostAndFoundService.SaveBasicUser(value);
            }

            return new ResponseResult<BasicUser>
            {
                Messages = new List<string>() { "OK" },
                Result = value,
                Succeed = true
            };
        }

        // PUT api/users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        //// DELETE api/users/5
        //public void Delete(int id)
        //{
        //}
    }
}
