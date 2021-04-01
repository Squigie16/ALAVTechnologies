using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        void AddComment(Comment comment);
    }
}
