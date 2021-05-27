using System.Net;

namespace MediaData.Models.Response
{
    public class AlbumIndexResponse : BaseResponse
    {

        public AlbumIndexResponse() : base() { }
        public AlbumIndexResponse(HttpStatusCode statusCode) : base(statusCode)
        {

        }

        public Index Data { get; set; }
    }

    public class Index
    { 
        public string IndexUrl { get; set; }
        public string DashboardUrl { get; set; }

    }
}
