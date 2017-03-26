using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace OnlineSatisProje.Web.Areas.Satici.CustomActions
{
    public class ImageResult : ActionResult
    {
        public ImageResult(Stream imageStream, string contentType)
        {
            if (null == imageStream)
                throw new ArgumentNullException(nameof(imageStream));
            if (null == contentType)
                throw new ArgumentNullException(nameof(contentType));
            ImageStream = imageStream;
            ContentType = contentType;
        }

        public Stream ImageStream { get; set; }
        public string ContentType { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (null == context)
                throw new ArgumentNullException(nameof(context));
            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
            var buffer = new byte[4096];
            while (true)
            {
                var read = ImageStream.Read(buffer, 0, buffer.Length);
                if (read == 0)
                    break;

                response.OutputStream.Write(buffer, 0, read);
            }
            response.End();
        }
    }
}