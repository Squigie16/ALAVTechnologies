using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public interface IBlogRepository
    {
        IQueryable<Blog> Blogs { get; }
        void AddBlog(Blog blog);
        void EditBlog(Blog blog);
    }
}
