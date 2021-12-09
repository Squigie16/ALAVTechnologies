using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class EFBlogRepository : IBlogRepository
    {
        private MBS_DBContext context;

        public EFBlogRepository(MBS_DBContext dBContext)
        {
            context = dBContext;
        }

        public IQueryable<Blog> Blogs => context.Blogs;

        public void AddBlog(Blog blog)
        {
            context.Blogs.Add(blog);
            context.SaveChanges();
        }
        public void EditBlog(Blog blog)
        {
            context.Blogs.Update(blog);
            context.SaveChanges();
        }

        public void DeleteBlog(int id)
        {
            foreach (Comment c in context.Comments)
            {
                if (c.BlogID == id)
                {
                    context.Comments.Remove(c);
                }
            }

            context.SaveChanges();

            Blog blog = new Blog();
            foreach(Blog b in context.Blogs)
            {
                if(b.ID == id)
                {
                    blog = b;
                    break;
                }
            }

            context.Blogs.Remove(blog);

            context.SaveChanges();

        }
    }
}
