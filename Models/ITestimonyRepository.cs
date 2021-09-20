using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public interface ITestimonyRepository
    {
        IQueryable<Testimony> Testimonies { get; }
        void AddTestimony(Testimony testimony);
        void EditTestimony(Testimony testimony);
        void DeleteTestimony(int id);
    }
}
