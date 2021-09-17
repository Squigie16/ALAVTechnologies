using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LloydStephanieRealty.Models
{
    public class MailingListUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsLookingToSell { get; set; }
        public bool IsLookingToBuy { get; set; }
        public string NeighbourhoodOfInterest { get; set; }
        public string Email { get; set; }
    }
}
