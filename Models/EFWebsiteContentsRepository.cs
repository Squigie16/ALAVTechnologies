using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class EFWebsiteContentsRepository: IWebsiteContentsRepository
    {
        private MBS_DBContext context;

        public EFWebsiteContentsRepository(MBS_DBContext ctx)
        {
            context = ctx;
        }
        public WebsiteContents Content => context.websiteContents;

        public void EditAboutUsContents(string header, string content)
        {
            context.websiteContents.AboutUsHeader = header;
            context.websiteContents.AboutUsParagraph = content;
        }

        public void EditCompanyDetails(string phoneNumber, string email, string address, string igURL, string fbURL)
        {
            context.websiteContents.CompanyPhoneNumber = phoneNumber;
            context.websiteContents.CompanyEmailAddress = email;
            context.websiteContents.CompanyHeadquarters = address;
            context.websiteContents.InstagramURL = igURL;
            context.websiteContents.FacebookURL = fbURL;
        }
    }
}
