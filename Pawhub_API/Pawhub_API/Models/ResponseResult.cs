using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawhub_API.Models
{
    public class ResponseResult<T>
    {
        public bool Succeed {get;set;}
        public T Result {get; set;}
        public List<string> Messages { get; set;}
        public List<string> Errors { get; set; }
        }
    }
