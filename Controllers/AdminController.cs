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
        private IBlogRepository blogRepository;
        private ICommentRepository commentRepository;
        public AdminController(IBlogRepository bRepository, ICommentRepository cRepository)
        {
            blogRepository = bRepository;
            commentRepository = cRepository;
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
        public IActionResult AddBlog()
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
    }
}
