using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediaData.Models.Response;
using MediaData.Services;
using System.Threading.Tasks;


namespace MediaData.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaDataController : ControllerBase
    {
        private readonly ILogger<MediaDataController> _logger;
        private readonly IMediaDataServiceFacade _serviceFacade;

        public MediaDataController(IMediaDataServiceFacade serviceFacade, ILogger<MediaDataController> logger)
        {
            _logger = logger;
            _serviceFacade = serviceFacade;
        }

        [HttpGet]
        [Route("GetAlbumIndex")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<AlbumIndexResponse> GetAlbumIndex()
        {
            _logger.LogInformation("Getting Album Index...");
            var response =_serviceFacade.GetAlbumIndex();
            return Task.FromResult(response);
        }
    }
}
