using Microsoft.AspNetCore.Mvc;
using VoteForCauseFinal.Data;
using VoteForCauseFinal.Models.ViewModels;
using VoteForCauseFinal.Models.Domain;
using Microsoft.EntityFrameworkCore;
using VoteForCauseFinal.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace VoteForCauseFinal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;
        public AdminTagsController(ITagRepository tagRepository)
        {
           this.tagRepository = tagRepository;
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest) 
        {
            ValidateAddTaskRequest(addTagRequest);
            if (ModelState.IsValid == false)
            {
                return View();
            }

            //Mapping addtagrequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);
            
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List() 
        {

            //user Dbcontext to read tags
            var tags = await tagRepository.GetAllAsync();


            return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        { 
            var tag = await tagRepository.GetAsync(id);

            if (tag != null) 
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                    
                };

                return View(editTagRequest);
            }

            return View(null); 
        }

        [HttpPost]
        public async  Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                return Json(new { message = "Tag updated successfully", status = "success" });

            }
            else
            {
                return Json(new { message = "Error updating tag", status = "error" });
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //show success deletion
                return RedirectToAction("List");
            }


            //show error attempt
            return RedirectToAction("Edit", new
            {
                id = editTagRequest.Id
            });
        }


        private void ValidateAddTaskRequest (AddTagRequest addTagRequest)
        {
            if(addTagRequest.Name != null && addTagRequest.DisplayName != null)
            {
                if(addTagRequest.Name == addTagRequest.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be identical!");
                }
            }
        }
    }
}
