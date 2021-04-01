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
        public AdminController(IBlogRepository repository)
        {
            blogRepository = repository;
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
           // blog.Comments = new List<Comment>();
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

            return View(blog);
        }

        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            blogRepository.EditBlog(blog);
            return RedirectToAction("BlogIndex");
        }
    }
}
