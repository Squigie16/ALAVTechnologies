using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public interface IWebsiteContentsRepository
    {
        WebsiteContents Content { get; }
        void EditAboutUsContents(string header, string content);

        void EditCompanyDetails(string phoneNumber, string email, string address, string igURL, string fbURL);
    }
}
