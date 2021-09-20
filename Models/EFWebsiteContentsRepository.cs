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
            Content.AboutUsHeader = header;
            Content.AboutUsParagraph = content;
        }

        public void EditCompanyDetails(string phoneNumber, string email, string address)
        {
            Content.CompanyPhoneNumber = phoneNumber;
            Content.CompanyEmailAddress = email;
            Content.CompanyHeadquarters = address;
        }

        public void EditHomePage(string talkToUsText, string meetUsText, string connectWithUsText)
        {
            Content.HomePageTalkToUsText = talkToUsText;
            Content.HomePageMeetUsText = meetUsText;
            Content.HomePageConnectWithUsText = connectWithUsText;
        }
    }
}
