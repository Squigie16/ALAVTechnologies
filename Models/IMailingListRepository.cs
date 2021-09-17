using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public interface IMailingListRepository
    {
        IQueryable<MailingListUser> Users { get; }
        void AddUser(MailingListUser user);
        void DeleteUser(int id);
    }
}
