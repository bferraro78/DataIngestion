using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicData.Models.Response;
using MusicData.Services;
using System.Threading.Tasks;


namespace MusicData.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusicDataController : ControllerBase
    {
        private readonly ILogger<MusicDataController> _logger;
        private readonly IMusicDataServiceFacade _serviceFacade;

        public MusicDataController(IMusicDataServiceFacade serviceFacade, ILogger<MusicDataController> logger)
        {
            _logger = logger;
            _serviceFacade = serviceFacade;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<AlbumIndexResponse> GetAlbumIndex()
        {
            var response =_serviceFacade.GetAlbumIndex();
            return Task.FromResult(response);
        }
    }
}
