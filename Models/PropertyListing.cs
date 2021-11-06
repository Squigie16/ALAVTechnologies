using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class PropertyListing
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int numOfBedrooms { get; set; }
        public int numOfBathrooms { get; set; }
        public string linkToFullListing { get; set; }
        public string AskingPrice { get; set; }
        public int ImageID { get; set; }
        [NotMapped]
        public ImageModel Image { get; set; }
    }
}
