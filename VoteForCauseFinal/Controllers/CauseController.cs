using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VoteForCauseFinal.Models.Domain;
using VoteForCauseFinal.Models.ViewModels;
using VoteForCauseFinal.Repositories;

namespace VoteForCauseFinal.Controllers
{
    public class CauseController : Controller
    {
        private readonly ICausePostRepository causePostRepository;
        private readonly ICausePostSignRepository causePostSignRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICausePostCommentRepository causePostCommentRepository;

        //inject signin/user manager for signing the cause
        public CauseController(ICausePostRepository causePostRepository, 
            ICausePostSignRepository causePostSignRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ICausePostCommentRepository causePostCommentRepository)
        {
             
            this.causePostRepository = causePostRepository;
            this.causePostSignRepository = causePostSignRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.causePostCommentRepository = causePostCommentRepository;
        }



        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var signed = false;

            var causePost = await causePostRepository.GetByUrlHandleAsync(urlHandle);
            var causeDetailsViewModel = new CauseDetailsViewModel();




            if (causePost != null)
            {
                var totalSigns = await causePostSignRepository.GetTotalSigns(causePost.Id);

                if (signInManager.IsSignedIn(User))
                {
                    //get the signitures for this user
                    var signsForCause = await causePostSignRepository.GetSignsForCauseForUser(causePost.Id);

                    var userId = userManager.GetUserId(User);

                    if(userId != null)
                    {
                       var signFromuser = signsForCause.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                       signed = signFromuser != null;

                    
                    }


                }

                //get comments for post

                var causeCommentsDomainModel = await causePostCommentRepository.GetCommentsByCauseIdAsync(causePost.Id);

                var causeCommentsForView = new List<CauseComment>();



                foreach (var causeComment in causeCommentsDomainModel)
                {
                    string userEmail = (await userManager.FindByIdAsync(causeComment.UserId.ToString())).UserName;
                    string username = GetUsernameFromEmail(userEmail);

                    causeCommentsForView.Add(new CauseComment
                    {
                        Description = causeComment.Description,
                        DateAdded = causeComment.DateAdded,
                        Username = username
                    });
                }



                causeDetailsViewModel = new CauseDetailsViewModel
                
                {
                    Id = causePost.Id,
                    Description = causePost.Description,
                    ShortDescription = causePost.ShortDescription,
                    Author = causePost.Author,
                    FeaturedImageUrl = causePost.FeaturedImageUrl,
                    Heading = causePost.Heading,
                    PublishedDate = causePost.PublishedDate,
                    UrlHandle = causePost.UrlHandle,
                    Visible = causePost.Visible,
                    Tags = causePost.Tags,
                    TotalSigns = totalSigns,
                    Signed = signed,
                    Comments = causeCommentsForView,
                };



            }


            return View(causeDetailsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(CauseDetailsViewModel causeDetailsViewModel)
        {
            if(signInManager.IsSignedIn(User))
            {
                var domainModel = new CausePostComment
                {
                    CausePostId = causeDetailsViewModel.Id,
                    Description = causeDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };

                await causePostCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Index", "Cause", 
                    new {urlHandle = causeDetailsViewModel.UrlHandle});
            }

            return View();


        }

        private static string GetUsernameFromEmail(string email)
        {
            int atIndex = email.IndexOf('@');
            if (atIndex == -1)
            {
                return email; // Return the original input if there's no '@' symbol.
            }
            return email.Substring(0, atIndex);
        }


        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            var searchResults = await causePostRepository.SearchCauses(query);
            return View("SearchResults", searchResults.ToList());
        }



    }
}
