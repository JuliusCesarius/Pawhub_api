using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace blastic.pawhub.api.Models.Enums
{
    public enum ErrorCodes
    {
        Ok = HttpStatusCode.OK,
        NotFound = HttpStatusCode.NotFound,
        NotImplemented = HttpStatusCode.NotImplemented,
        RequestedRangeNotSatisfiable = HttpStatusCode.RequestedRangeNotSatisfiable,
        InternalServerError = HttpStatusCode.InternalServerError
    }
}