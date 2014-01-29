using blastic.pawhub.models.Register;
using blastic.pawhub.service;
using blastic.pawhub.service.core;
using Pawhub_API.Models;
using Pawhub_API.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace blastic.pawhub.api.Controllers
{
    public class SignUpController : ApiController
    {
        // GET api/signup
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }   

        // GET api/signup/5
        public string Get(int id)
        {
            return "value";
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [NotImplementedExceptionFilter]
        public ResponseResult<BasicUser> Post([FromBody] BasicUser value)
        {
            using (var BasicUsersService = new BasicUsersService())
            {
                BasicUsersService.Save(value);
            }

            return new ResponseResult<BasicUser>
            {
                Messages = new List<string>() { "OK" },
                Result = value,
                Succeed = true
            };
        }

    }
}
