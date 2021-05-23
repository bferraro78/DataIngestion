using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MusicData.Models.Response
{
    public abstract class BaseResponse
    {
        public BaseResponse() { }
        public BaseResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
