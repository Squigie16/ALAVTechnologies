using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class EFMailingListRepository: IMailingListRepository
    {
        private MBS_DBContext context;
        public EFMailingListRepository(MBS_DBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<MailingListUser> Users => context.MailingListUsers;

        public void AddUser(MailingListUser user)
        {
            context.MailingListUsers.Add(user);
            context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            MailingListUser user = new MailingListUser();
            foreach(MailingListUser ml in context.MailingListUsers)
            {
                if(ml.ID == id)
                {
                    user = ml;
                    break;
                }
            }

            context.MailingListUsers.Remove(user);
            context.SaveChanges();
        }
    }
}
