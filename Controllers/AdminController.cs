using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LloydStephanieRealty.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LloydStephanieRealty.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private IMailingListRepository mailingListRepository;
        private IBlogRepository blogRepository;
        private ICommentRepository commentRepository;
        private ITestimonyRepository testimonyRepository;
        private IWebsiteContentsRepository contentsRepository;
        private IImageModelRepository imageRepository;
        private IPropertyListingRepository propertyRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AdminController(IBlogRepository bRepository, ICommentRepository cRepository, IMailingListRepository mlRepository, IWebsiteContentsRepository contents, IImageModelRepository iRepository, ITestimonyRepository tRepository, UserManager<IdentityUser> manager, SignInManager<IdentityUser> signIn, IPropertyListingRepository pRepository)
        {
            blogRepository = bRepository;
            commentRepository = cRepository;
            mailingListRepository = mlRepository;
            contentsRepository = contents;
            imageRepository = iRepository;
            testimonyRepository = tRepository;
            propertyRepository = pRepository;
            userManager = manager;
            signInManager = signIn;
        }

        public IActionResult AdminIndex()
        {
            return View();
        }

        public IActionResult BlogIndex()
        {
            IQueryable<Blog> blogs = blogRepository.Blogs;
            return View(blogs);
        }
        public IActionResult ListingsIndex()
        {
            IQueryable<PropertyListing> listings = propertyRepository.PropertyListings;
            return View(listings);
        }
        public IActionResult TestimonyIndex()
        {
            IQueryable<Testimony> testimonies = testimonyRepository.Testimonies;
            return View(testimonies);
        }

        public IActionResult WebsiteContentsIndex()
        {
            return View();
        }

        public IActionResult AboutUsAdmin()
        {
            ViewData["AboutUsHeader"] = contentsRepository.Content.AboutUsHeader;
            ViewData["AboutUsPara"] = contentsRepository.Content.AboutUsParagraph;
            return View();
        }

        public IActionResult CompanyDetailsAdmin()
        {
            ViewData["email"] = contentsRepository.Content.CompanyEmailAddress;
            ViewData["phonenumber"] = contentsRepository.Content.CompanyPhoneNumber;
            ViewData["address"] = contentsRepository.Content.CompanyHeadquarters;
            return View();
        }

        public IActionResult HomePageAdmin()
        {
            ViewData["talkToUsText"] = contentsRepository.Content.HomePageTalkToUsText;
            ViewData["meetUsText"] = contentsRepository.Content.HomePageMeetUsText;
            ViewData["connectWithUsText"] = contentsRepository.Content.HomePageConnectWithUsText;
            return View();
        }

        public IActionResult MailingListIndex()
        {
            IQueryable<MailingListUser> mailingListUsers = mailingListRepository.Users;
            return View(mailingListUsers);
        }

        public IActionResult AddBlog()
        {
            return View();
        }
        public IActionResult AddListing()
        {
            return View();
        }

        public IActionResult AddTestimony()
        {
            return View();
        }

        public IActionResult SendEmailToAll()
        {
            return View();
        }

        public IActionResult SendEmailToSellers()
        {
            return View();
        }

        public IActionResult SendEmailToBuyers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(Blog blog)
        {
            ImageModel image = blog.Image;
            int imageID = await imageRepository.AddImageAsync(image);
            blog.DateOfPost = DateTime.Now;
            blog.ImageID = imageID;
            blogRepository.AddBlog(blog);

            return RedirectToAction("BlogIndex");
        }

        public IActionResult EditBlog(int id)
        { 
            Blog blog = new Blog();
            IQueryable<Blog> blogs = blogRepository.Blogs;
            foreach(Blog b in blogs)
            {
                if(id == b.ID)
                {
                    blog = b;
                    break;
                }
            }
            
            foreach(ImageModel i in imageRepository.Images)
            {
                if(blog.ImageID == i.ImageId)
                {
                    blog.Image = i;
                    break;
                }
            }

            List<Comment> commentsOnPost = new List<Comment>();

            foreach (Comment c in commentRepository.Comments)
            {
                if (c.BlogID == blog.ID)
                {
                    commentsOnPost.Add(c);
                }
            }

            ViewData["Comments"] = commentsOnPost;
            return View(blog);
        }
        public IActionResult EditListing(int id)
        {
            PropertyListing listing = new PropertyListing();
            foreach (PropertyListing l in propertyRepository.PropertyListings)
            {
                if (id == l.ID)
                {
                    listing = l;
                    break;
                }
            }

            foreach (ImageModel i in imageRepository.Images)
            {
                if (listing.ImageID == i.ImageId)
                {
                    listing.Image = i;
                    break;
                }
            }
            return View(listing);
        }

        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            blog.DateOfPost = DateTime.Now;
            blogRepository.EditBlog(blog);
            return RedirectToAction("BlogIndex");
        }

        [HttpPost]
        public IActionResult EditListing(PropertyListing listing)
        {
            propertyRepository.EditPropertyListing(listing);
            return RedirectToAction("ListingsIndex");
        }

        [HttpPost]
        public IActionResult DeleteBlog(int BlogID, int ImageID)
        {
            imageRepository.DeleteImage(ImageID);
            blogRepository.DeleteBlog(BlogID);
            return RedirectToAction("BlogIndex");
        }

        [HttpPost]
        public IActionResult DeleteListing(int PropertyID, int ImageID)
        {
            imageRepository.DeleteImage(ImageID);
            propertyRepository.DeletePropertyListing(PropertyID);
            return RedirectToAction("ListingsIndex");
        }

        [HttpPost]
        public IActionResult AddTestimony(Testimony testimony)
        {
            testimony.DateOfPost = DateTime.Now;
            testimonyRepository.AddTestimony(testimony);

            return RedirectToAction("TestimonyIndex");
        }

        public IActionResult EditTestimony(int id)
        {
            Testimony testimony = new Testimony();
            IQueryable<Testimony> testimonies = testimonyRepository.Testimonies;
            foreach (Testimony b in testimonies)
            {
                if (id == b.ID)
                {
                    testimony = b;
                    break;
                }
            }
            return View(testimony);
        }

        [HttpPost]
        public IActionResult EditTestimony(Testimony testimony)
        {
            testimony.DateOfPost = DateTime.Now;
            testimonyRepository.EditTestimony(testimony);
            return RedirectToAction("TestimonyIndex");
        }

        [HttpPost]
        public IActionResult DeleteTestimony(int TestimonyID)
        {
            testimonyRepository.DeleteTestimony(TestimonyID);
            return RedirectToAction("TestimonyIndex");
        }

        [HttpPost]
        public IActionResult DeleteComment(int CommentID)
        {
            commentRepository.DeleteComment(CommentID);
            return RedirectToAction("BlogIndex");
        }

        [HttpPost]
        public IActionResult DeleteUser(int ID)
        {
            mailingListRepository.DeleteUser(ID);
            return RedirectToAction("MailingListIndex");
        }

        [HttpPost]
        public IActionResult SendEmailToAllSubscribers()
        {
            string emailSubject = Request.Form["subject"];
            string emailContent = Request.Form["content"];

            EmailToCustomer email = new EmailToCustomer();
            email.SendEmailToGroup(emailSubject, emailContent, mailingListRepository.Users);

            return RedirectToAction("MailingListIndex");
        }

        [HttpPost]
        public IActionResult SendEmailToAllSellers()
        {
            string emailSubject = Request.Form["subject"];
            string emailContent = Request.Form["content"];

            EmailToCustomer email = new EmailToCustomer();

            List<MailingListUser> users = new List<MailingListUser>();

            foreach (MailingListUser ml in mailingListRepository.Users)
            {
                if(ml.IsLookingToSell == true)
                {
                    users.Add(ml);
                }
            }

            IQueryable<MailingListUser> usersWhoWantToSell = users.AsQueryable();

            email.SendEmailToGroup(emailSubject, emailContent, usersWhoWantToSell);

            return RedirectToAction("MailingListIndex");
        }

        public IActionResult SendEmailToAllBuyers()
        {
            string emailSubject = Request.Form["subject"];
            string emailContent = Request.Form["content"];

            EmailToCustomer email = new EmailToCustomer();

            List<MailingListUser> users = new List<MailingListUser>();

            foreach (MailingListUser ml in mailingListRepository.Users)
            {
                if (ml.IsLookingToBuy == true)
                {
                    users.Add(ml);
                }
            }

            IQueryable<MailingListUser> usersWhoWantToBuy = users.AsQueryable();

            email.SendEmailToGroup(emailSubject, emailContent, usersWhoWantToBuy);

            return RedirectToAction("MailingListIndex");
        }

        [HttpPost]
        public IActionResult EditAboutUs()
        {
            string newHeader = Request.Form["header"];
            string newPara = Request.Form["paragraph"];
            contentsRepository.EditAboutUsContents(newHeader, newPara);
            return RedirectToAction("WebsiteContentsIndex");
        }

        [HttpPost]
        public IActionResult EditCompanyDetails()
        {
            string newNumber = Request.Form["phoneNumber"];
            string newEmail = Request.Form["email"];
            string newAddress = Request.Form["address"];
            contentsRepository.EditCompanyDetails(newNumber, newEmail, newAddress);
            return RedirectToAction("WebsiteContentsIndex");
        }

        [HttpPost]
        public IActionResult EditHomePage()
        {
            string newTalkToUsText = Request.Form["talkToUs"];
            string newMeetUsText = Request.Form["meetUs"];
            string newConnectWithUsText = Request.Form["connectWithUs"];
            contentsRepository.EditHomePage(newTalkToUsText, newMeetUsText, newConnectWithUsText);
            return RedirectToAction("WebsiteContentsIndex");
        }

        public IActionResult ResetAdminPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetAdminPassword(ResetAdminModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if(user == null)
                {
                    Console.WriteLine("User cannot be found");
                    return RedirectToPage("/Login");
                }

                var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await signInManager.RefreshSignInAsync(user);

                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddListing(PropertyListing property)
        {
            ImageModel image = property.Image;
            int imageID = await imageRepository.AddImageAsync(image);
            property.ImageID = imageID;
            propertyRepository.AddPropertyListing(property);

            return RedirectToAction("ListingsIndex");
        }
    }
}
