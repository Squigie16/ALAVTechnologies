using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class EFTestimonyRepository : ITestimonyRepository
    {
        private MBS_DBContext context;

        public EFTestimonyRepository(MBS_DBContext dBContext)
        {
            context = dBContext;
        }

        public IQueryable<Testimony> Testimonies => context.Testimonies;

        public void AddTestimony(Testimony testimony)
        {
            context.Testimonies.Add(testimony);
            context.SaveChanges();
        }

        public void EditTestimony(Testimony testimony)
        {
            context.Testimonies.Update(testimony);
            context.SaveChanges();
        }

        public void DeleteTestimony(int id)
        {
            Testimony testimony = new Testimony();
            foreach (Testimony c in context.Testimonies)
            {
                if (c.ID == id)
                {
                    testimony = c;
                    break;
                }
            }

            context.Testimonies.Remove(testimony);
            context.SaveChanges();
        }

    }
}
