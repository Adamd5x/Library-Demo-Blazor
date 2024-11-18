using Library.Abstracts.Core;
using Library.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route ("api/stat")]
    [ApiController]
    public class StatController(IStatCoreService coreService) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType (StatusCodes.Status200OK, Type = typeof (StatDto))]
        public async Task<IActionResult> GetStat ()
        {
            var result = await coreService.GetStatAsync();
            return Ok (result);
        }
    }
}
