using Pawhub_API.Models.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace blastic.pawhub.api.Controllers
{
    public class PicsController : ApiController
    {
        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("pics/{size:string}/{density:int?}/{id:string?}")]
        public HttpResponseMessage Get(string size, int density, string id)
        {
            Image img
            using (var picsService = new PicsService())
            {
                //TODO Parametrizar el pageSize
                var reports = reportsService.Get(type, pageNumber, 20);

            }
            Image img = GetImage(imageName, width, height);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(ms.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return result;
        }
    }
}
