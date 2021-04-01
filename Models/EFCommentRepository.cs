using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class EFCommentRepository : ICommentRepository
    {
        private MBS_DBContext context;

        public EFCommentRepository(MBS_DBContext dBContext)
        {
            context = dBContext;
        }

        public IQueryable<Comment> Comments => context.Comments;

        public void AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            Comment comment = new Comment();
            foreach(Comment c in context.Comments)
            {
                if(c.ID == id)
                {
                    comment = c;
                    break;
                }
            }

            context.Comments.Remove(comment);
            context.SaveChanges();
        }
    }
}
