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
 
        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("users/{id:regex(^[0-9a-fA-F]{24}$)}")]
        public ResponseResult<User> Get(string id)
        {
            User user=null;
            using (var usersService = new UsersService())
            {
                user = usersService.GetById(id);
            }

            return new ResponseResult<User>
            {
                Messages = new List<string>() { "OK" },
                Result = user,
                Succeed = true
            };
        }

        [HttpPost]
        [NotImplementedExceptionFilter]
        [Route("users")]
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

        [HttpPut]
        [NotImplementedExceptionFilter]
        [Route("users")]
        public ResponseResult<User> Put([FromBody] User value)
        {
            using (var usersService = new UsersService())
            {
                usersService.Update(value);
            }

            return new ResponseResult<User>
            {
                Messages = new List<string>() { "OK" },
                Result = value,
                Succeed = true
            };
        }
    }
}
