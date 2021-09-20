using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace LloydStephanieRealty.Models
{
    public class MBS_DBContext : DbContext
    {
        public MBS_DBContext(DbContextOptions<MBS_DBContext> options)
            : base(options) { }
        //entity
        public DbSet<MailingListUser> MailingListUsers { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Comment> Comments {get; set;}
        public virtual DbSet<Testimony> Testimonies { get; set; }
        public virtual DbSet<ImageModel> Images { get; set; }

        public static WebsiteContents contents = new WebsiteContents();
        public WebsiteContents websiteContents { get; set; } = contents;
    }
}
