using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Project.Server.Providers
{
    public class UploadMultipartFormProvider : MultipartFormDataStreamProvider
    {
        public UploadMultipartFormProvider(string rootPath) : base(rootPath)
        {
        }

        public UploadMultipartFormProvider(string rootPath, int bufferSize) : base(rootPath, bufferSize)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers != null && headers.ContentDisposition != null)
            {
                return headers.ContentDisposition.FileName.TrimEnd('"').TrimStart('"');
            }
            return base.GetLocalFileName(headers);
        }
    }

    public class MimeMultipart : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }
    }


    public class FormDataStreamer : MultipartFormDataStreamProvider
    {
        public FormDataStreamer(string rootPath) : base(rootPath)
        {
        }

        public FormDataStreamer(string rootPath, int bufferSize) : base(rootPath, bufferSize)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var srcFileName = headers.ContentDisposition.FileName.Replace("\"", "");
            return Guid.NewGuid() + Path.GetExtension(srcFileName);
        }
    }
}