using blastic.pawhub.models;
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
using blastic.pawhub.api.Controllers;
using blastic.pawhub.service.core;

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
        public ResponseResult<User> Post([FromBody] User value)
        {
            using (var usersService = new UsersService())
            {
                usersService.Save(value);
            }

            return new ResponseResult<User>
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
