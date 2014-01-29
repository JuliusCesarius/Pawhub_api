using blastic.pawhub.service.core;
using Pawhub_API.Models;
using Pawhub_API.Models.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace blastic.pawhub.api.Controllers
{
    public class PicsController : ApiController
    {
        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("pics/{size}/{density:int?}/{id?}")]
        public HttpResponseMessage Get(string size, int density, string id)
        {
            using (var picturesService = new PicturesService())
            {
                FileStream fileStream = null;
                HttpResponseMessage result ;
                using (picturesService.GetImageStream(size, density, id, out fileStream)){
                    if (fileStream.CanRead)
                    {
                        int length = (int)fileStream.Length;
                        byte[] buffer = new byte[length];
                        fileStream.Read(buffer, 0, length);

                        result = new HttpResponseMessage(HttpStatusCode.OK);
                        result.Content = new ByteArrayContent(buffer);
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                        return result;
                    }
                    else
                    {
                        var content = new ResponseResult<string>
                        {
                            Errors = new List<string>() { "Image not found" },
                            Result = null,
                            Succeed = true
                        };
                        result = new HttpResponseMessage(HttpStatusCode.NotFound);
                        result.Content = new ObjectContent<ResponseResult<string>>(content, new JsonMediaTypeFormatter());
                        return result;
                    }
                }
            }

        }

        [HttpPost]
        [NotImplementedExceptionFilter]
        public ResponseResult<string> Post()
        {
            using (var picturesService = new PicturesService())
            {
                //var fileStream = picturesService.GetImageStream(size, density, id);
                //int length = (int)fileStream.Length;
                //byte[] buffer = new byte[length];
                //fileStream.Read(buffer, 0, length)
                var picture = new models.Picture {_userId="52da1001ba2619186cf0306d", path="c:\\temp"};
                var pic = picturesService.Save(picture);

                return new ResponseResult<string>
                {
                    Messages = new List<string>() { "OK" },
                    Result = picture._id,
                    Succeed = true
                };
            }
        }
    }
}
