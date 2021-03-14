using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class EFStoreRepository: IUserRepository
    {
        private MBS_DBContext context;
        public EFStoreRepository(MBS_DBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<User> Users => context.Users;
    }
}
