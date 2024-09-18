using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoteForCauseFinal.Models.Domain;
using VoteForCauseFinal.Models.ViewModels;
using VoteForCauseFinal.Repositories;

namespace VoteForCauseFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CausePostSignController : ControllerBase
    {
        private readonly ICausePostSignRepository causePostSignRepository;

        public CausePostSignController(ICausePostSignRepository causePostSignRepository)
        {
            this.causePostSignRepository = causePostSignRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task< IActionResult> AddSign([FromBody] AddSignRequest addSignRequest)
        {
            var model = new CausePostSign
            {
                CausePostId = addSignRequest.CausePostId,
                UserId = addSignRequest.UserId,
            };


            await causePostSignRepository.AddSignForCause(model);

            return Ok();
        }

        [HttpGet]
        [Route("{causePostId:Guid}/totalSigns")]
        public async Task<IActionResult> GetTotalSignsForCause([FromRoute] Guid causePostId) 
        {
           var totalSigns = await causePostSignRepository.GetTotalSigns(causePostId);
        
            return Ok(totalSigns);
        
        }


    }
}
