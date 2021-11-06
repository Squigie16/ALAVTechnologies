using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class EFPropertyListingRepository : IPropertyListingRepository
    {
        private MBS_DBContext context;

        public EFPropertyListingRepository(MBS_DBContext dBContext)
        {
            context = dBContext;
        }

        public IQueryable<PropertyListing> PropertyListings => context.PropertyListings;

        public void AddPropertyListing(PropertyListing property)
        {
            context.PropertyListings.Add(property);
            context.SaveChanges();
        }
        public void EditPropertyListing(PropertyListing property)
        {
            context.PropertyListings.Update(property);
            context.SaveChanges();
        }
        public void DeletePropertyListing(int id)
        {
            PropertyListing property = new PropertyListing();
            foreach(PropertyListing pl in context.PropertyListings)
            {
                if(pl.ID == id)
                {
                    property = pl;
                    break;
                }
            }

            context.PropertyListings.Remove(property);
            context.SaveChanges();
        }
    }
}
