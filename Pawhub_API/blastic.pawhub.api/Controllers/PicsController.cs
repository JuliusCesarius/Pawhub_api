using blastic.pawhub.models.Enums;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace blastic.pawhub.api.Controllers
{
    public class PicsController : ApiController
    {
        [HttpGet]
        [NotImplementedExceptionFilter]
        [Route("pics/{size}/{id?}")]
        public HttpResponseMessage Get(string size, string id)
        {
            using (var picturesService = new PicturesService())
            {
                FileStream fileStream = null;
                HttpResponseMessage result ;
                string rootPath = HttpContext.Current.Server.MapPath("~/data/pics");
                PicSize picSize = (PicSize)Enum.Parse(typeof(PicSize), size);
                using (picturesService.GetImageStream(rootPath, picSize,id, out fileStream))
                {
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
        [Route("pics/{type}/{id:regex(^[0-9a-fA-F]{24}$)}")]
        public async Task<HttpResponseMessage> Post(string type, string id)
        {
            string imageId = null;
            string temPath = null;
            //Checks if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent()) throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //Checks if the type is valid
            PicType picType;
            if (!Enum.TryParse<PicType>(type, out picType)) throw new HttpResponseException(HttpStatusCode.InternalServerError);

            string path = HttpContext.Current.Server.MapPath("~/data/pics");
            string tempPath = path + "\\temp";
            try
            {
                using (var picturesService = new PicturesService())
                {
                    picturesService.EnsureDirectory(path + "\\temp");
                    var provider = new MultipartFormDataStreamProvider(tempPath);
                    MultipartFileData fileData = null;


                    // Read the form data and save in the path.
                    await Request.Content.ReadAsMultipartAsync(provider);

                    if (provider.FileData.Count <= 0)
                    {
                        throw new HttpResponseException(HttpStatusCode.NoContent);
                    }

                    fileData = provider.FileData.First();
                    if (!fileData.Headers.ContentType.Equals(MediaTypeHeaderValue.Parse("image/jpeg")) && !fileData.Headers.ContentType.Equals(MediaTypeHeaderValue.Parse("image/png")) && !fileData.Headers.ContentType.Equals(MediaTypeHeaderValue.Parse("image/gif")))
                    {
                        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                    }
                    //var fileContent = provider.GetStream(provider.Contents.First(), provider.Contents.First().Headers);
                    temPath = fileData.LocalFileName;
                    imageId = picturesService.Save(picType, id, fileData.LocalFileName, path); 
                   
                    //Adds the image to the corresponding document
                    switch (picType)
                    {
                        //TODO:Implementar el codigo para los demás tipos
                        case PicType.reports:
                            using (var reportsService = new ReportsService())
                            {
                                var report = reportsService.GetById(id);
                                if(string.IsNullOrEmpty(report.picture)){
                                    report.picture = imageId;
                                }else{
                                    report.detail.pics.Add(imageId);
                                }
                                reportsService.Update(report);
                            };
                            break;
                        case PicType.users:
                            break;
                    }
                }
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                //deletes the temp file
                if (temPath != null && File.Exists(temPath))
                {
                    using (var picturesService = new PicturesService())
                    {
                        picturesService.DeleteFile(temPath);
                    }
                }
            }
            if (!string.IsNullOrEmpty(imageId))
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                imageId = Newtonsoft.Json.JsonConvert.SerializeObject(new { id = imageId });
                result.Content = new ObjectContent(imageId.GetType(), imageId, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return result;
                //return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

    }
}
