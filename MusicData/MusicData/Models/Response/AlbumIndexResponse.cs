using System.Net;

namespace MusicData.Models.Response
{
    public class AlbumIndexResponse : BaseResponse
    {

        public AlbumIndexResponse() : base() { }
        public AlbumIndexResponse(HttpStatusCode statusCode) : base(statusCode)
        {

        }

        public string AlbumElasticIndex { get; set; }
    }
}
