using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public interface IPropertyListingRepository
    {
        IQueryable<PropertyListing> PropertyListings { get; }
        void AddPropertyListing(PropertyListing property);
        void EditPropertyListing(PropertyListing property);
        void DeletePropertyListing(int id);
    }
}
