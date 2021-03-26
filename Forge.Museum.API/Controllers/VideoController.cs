using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Forge.Museum.API.Controllers
{
    public class VideoController : ApiController
    {
        [HttpGet, Route("api/video")]
        public HttpResponseMessage GetVideoContent()
        {
            var httpResponse = Request.CreateResponse();
            httpResponse.Content = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteContentToStream);
            return httpResponse;
        }
        public async void WriteContentToStream(Stream outputStream, HttpContent content, TransportContext transportContext)
        {
            try
            {
                var filePath = HttpContext.Current.Server.MapPath("~/Content/Images/jojo.mp4");
                //here set the size of buffer, you can set any size  
                int bufferSize = 1000;
                byte[] buffer = new byte[bufferSize];
                //here we re using FileStream to read file from server//  
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    int totalSize = (int)fileStream.Length;
                    /*here we are saying read bytes from file as long as total size of file 

                    is greater then 0*/
                    while (totalSize > 0)
                    {
                        int count = totalSize > bufferSize ? bufferSize : totalSize;
                        //here we are reading the buffer from orginal file  
                        int sizeOfReadedBuffer = fileStream.Read(buffer, 0, count);
                        //here we are writing the readed buffer to output//  
                        await outputStream.WriteAsync(buffer, 0, sizeOfReadedBuffer);
                        //and finally after writing to output stream decrementing it to total size of file.  
                        totalSize -= sizeOfReadedBuffer;
                    }
                }
            }
            catch
            {
                return;
            }
            finally
            {
                outputStream.Close();
            }



            ////path of file which we have to read//  
            //var filePath = HttpContext.Current.Server.MapPath("~/Content/Images/lecture.mp4");
            ////here set the size of buffer, you can set any size  
            //var buffer = new byte[65536];
            ////here we re using FileStream to read file from server//  
            //using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            //{
            //    int totalSize = (int)fileStream.Length;
            //    int bytesRead = 1;

            //    while (totalSize > 0 && bytesRead >0)
            //    {
            //        bytesRead = fileStream.Read(buffer, 0, bytesRead);
            //        await outputStream.WriteAsync(buffer, 0, bytesRead);
            //        totalSize -= bytesRead;
            //    }
            //}
        }


        // GET: api/Video/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Video
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Video/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Video/5
        public void Delete(int id)
        {
        }
    }
}
