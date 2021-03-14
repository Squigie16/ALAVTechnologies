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
        public DbSet<User> Users { get; set; }
    }
}
