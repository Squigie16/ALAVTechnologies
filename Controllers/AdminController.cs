﻿using Microsoft.AspNetCore.Mvc;
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
        private IWebsiteContentsRepository contentsRepository;
        public AdminController(IBlogRepository bRepository, ICommentRepository cRepository, IMailingListRepository mlRepository, IWebsiteContentsRepository contents)
        {
            blogRepository = bRepository;
            commentRepository = cRepository;
            mailingListRepository = mlRepository;
            contentsRepository = contents;
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
            ViewData["IGURL"] = contentsRepository.Content.InstagramURL;
            ViewData["FBURL"] = contentsRepository.Content.FacebookURL;
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
            string newIG = Request.Form["igURL"];
            string newFB = Request.Form["fbURL"];
            contentsRepository.EditCompanyDetails(newNumber, newEmail, newAddress, newIG, newFB);
            return RedirectToAction("WebsiteContentsIndex");
        }
    }
}
