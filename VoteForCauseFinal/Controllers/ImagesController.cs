using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VoteForCauseFinal.Repositories;

namespace VoteForCauseFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        public async Task<IActionResult> UploadAsync (IFormFile file)
        {
            //call repo
            var imageURL = await imageRepository.UploadAsync (file);

            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult (new { link = imageURL });

        }
        
    }
}
