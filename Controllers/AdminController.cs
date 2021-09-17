using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LloydStephanieRealty.Models;

namespace LloydStephanieRealty.Controllers
{
    public class AdminController : Controller
    {
        private IMailingListRepository mailingListRepository;
        private IBlogRepository blogRepository;
        private ICommentRepository commentRepository;
        public AdminController(IBlogRepository bRepository, ICommentRepository cRepository, IMailingListRepository mlRepository)
        {
            blogRepository = bRepository;
            commentRepository = cRepository;
            mailingListRepository = mlRepository;
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

        public IActionResult MailingListIndex()
        {
            IQueryable<MailingListUser> mailingListUsers = mailingListRepository.Users;
            return View(mailingListUsers);
        }

        public IActionResult AddBlog()
        {
            return View();
        }

        public IActionResult SendEmailToAll()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBlog(Blog blog)
        {
            blog.DateOfPost = DateTime.Now;
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

        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            blogRepository.EditBlog(blog);
            return RedirectToAction("BlogIndex");
        }

        [HttpPost]
        public IActionResult DeleteBlog(int BlogID)
        {
            blogRepository.DeleteBlog(BlogID);
            return RedirectToAction("BlogIndex");
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
    }
}
