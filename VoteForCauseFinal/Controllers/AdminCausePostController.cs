using Microsoft.AspNetCore.Mvc;
using VoteForCauseFinal.Models.ViewModels;
using VoteForCauseFinal.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using VoteForCauseFinal.Models.Domain;
using VoteForCauseFinal.Data;
using Microsoft.AspNetCore.Authorization;

namespace VoteForCauseFinal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCausePostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly ICausePostRepository causePostRepository;

        public AdminCausePostController(ITagRepository tagRepository, ICausePostRepository causePostRepository) 
        {
            this.tagRepository = tagRepository;
            this.causePostRepository = causePostRepository;
        }



       [HttpGet]
       public async Task<IActionResult> Add()
        {
            //get tags from repository
            var tags = await tagRepository.GetAllAsync();

            var model = new AddCausePostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCausePostRequest addCausePostRequest)
        {
            //Map view model to domail model
            var causePost = new CausePost
            {
                Heading = addCausePostRequest.Heading,
                ShortDescription = addCausePostRequest.ShortDescription,
                Description = addCausePostRequest.Description,
                FeaturedImageUrl = addCausePostRequest.FeaturedImageUrl,
                UrlHandle = addCausePostRequest.UrlHandle,
                PublishedDate = addCausePostRequest.PublishedDate,
                Author = addCausePostRequest.Author,
                Visible = addCausePostRequest.Visible,

            };

            //Map tags from the selected tags
            var selectedTags = new List<Tag>();
            foreach(var selectedTagId in addCausePostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
               var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            //mapping the tags to the domain model
            causePost.Tags = selectedTags;

            await causePostRepository.AddAsync(causePost);


            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //call the repo
            var causePosts = await causePostRepository.GetAllAsync();

            return View(causePosts);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            //retrieve fro repo
            var causePost = await causePostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();
            

            if (causePost != null)
            {
                //map domain model in the view model
                var model = new EditCausePostRequest
                {
                    Id = causePost.Id,
                    ShortDescription = causePost.ShortDescription,
                    Heading = causePost.Heading,
                    Description = causePost.Description,
                    FeaturedImageUrl = causePost.FeaturedImageUrl,
                    UrlHandle = causePost.UrlHandle,
                    PublishedDate = causePost.PublishedDate,
                    Author = causePost.Author,
                    Visible = causePost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()

                    }),
                    SelectedTags = causePost.Tags.Select(x => x.Id.ToString()).ToArray()

                };

                return View(model);
            }

            //pass the data to view
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCausePostRequest editCausePostRequest)
        {
            //map view model back to domain model
            var causePostDomainModel = new CausePost
            {
                Id = editCausePostRequest.Id,
                Heading = editCausePostRequest.Heading,
                ShortDescription = editCausePostRequest.ShortDescription,
                Description = editCausePostRequest.Description,
                FeaturedImageUrl = editCausePostRequest.FeaturedImageUrl,
                UrlHandle = editCausePostRequest.UrlHandle,
                PublishedDate = editCausePostRequest.PublishedDate,
                Author = editCausePostRequest.Author,
                Visible = editCausePostRequest.Visible,

            };

            //mapping tags
            var selectedTags = new List<Tag>();
            foreach(var selectedTag in editCausePostRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                    var foundtag = await tagRepository.GetAsync(tag);

                    if(foundtag != null)
                    {
                        selectedTags.Add(foundtag);
                    }
                }
            }

            causePostDomainModel.Tags = selectedTags;

            //submit info to repo to update
            var updatedCause = await causePostRepository.UpdateAsync(causePostDomainModel);    

            if(updatedCause != null)
            {
                //show success
                return RedirectToAction("Edit");
            }

            //show error
            return RedirectToAction("Edit");
        }

        [HttpPost] 
        public async Task<IActionResult> Delete(EditCausePostRequest editCausePostRequest)
        {
            //tell repo to delete this post and tags
            var deletedCause = await causePostRepository.DeleteAsync(editCausePostRequest.Id);

            if(deletedCause != null)
            {
                //show success
                return RedirectToAction("List");
            }


            //show error
            return RedirectToAction("Edit", new { id = editCausePostRequest.Id });
        }
    }
}
