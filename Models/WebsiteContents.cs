using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class WebsiteContents
    {
        public string AboutUsHeader { get; set; } = "Stephanie and Lloyd Realty";
        public string AboutUsParagraph { get; set; } = "Vivamus condimentum dolor a dictum vehicula. Curabitur rhoncus nisl justo, at fringilla lacus viverra id. Mauris dignissim, felis vel suscipit luctus, nisl tortor luctus felis, at lobortis mauris nisl ut nisi. Nullam maximus ipsum consectetur tortor elementum vulputate. Nam quis purus dictum, porta est et, tempus lorem. Aliquam accumsan faucibus posuere. Phasellus finibus porta dapibus. Curabitur sit amet pulvinar ipsum, nec scelerisque justo. Aenean eleifend tincidunt sapien, sollicitudin ullamcorper nunc blandit sed. Maecenas ullamcorper, risus ac interdum pulvinar, turpis ipsum fermentum nunc, ut aliquet ipsum elit vel justo. In malesuada augue iaculis, ullamcorper nibh ac, dictum risus. Fusce ullamcorper tellus convallis congue laoreet. Pellentesque in tortor at nunc iaculis eleifend vel ac quam. Sed dui diam, mollis ac lorem in, vehicula tempor eros. Aliquam erat volutpat.";
        public string CompanyPhoneNumber { get; set; } = "123-456-7890";
        public string CompanyEmailAddress { get; set; } = "placeholder@mail.com";
        public string CompanyHeadquarters { get; set; } = "123 Main St";
        public string InstagramURL { get; set; } = "https://www.instagram.com/petroff.oliverhomes/";
        public string FacebookURL { get; set; } = "https://www.facebook.com/petroff.oliverhomes/";
        public string InitialSignUpEmail { get; set; }
        public string HomePageHeader { get; set; }
        public string HomePageParagraph { get; set; }
    }
}
