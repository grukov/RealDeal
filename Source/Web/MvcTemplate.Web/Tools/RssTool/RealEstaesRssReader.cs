namespace MvcTemplate.Web.Tools.RssTool
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;

    public class RealEstaesRssReader
    {
        public static IEnumerable<RealEstatesRss> GetFeed()
        {
            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            //remember to update this URL
            var xmlData = client.DownloadString("http://www.housingwire.com/rss/topic/57-real-estate");

            XDocument xml = XDocument.Parse(xmlData);

            var realEstateUpdates = (from story in xml.Descendants("item")
                                     select new RealEstatesRss
                                     {
                                         Title = ((string)story.Element("title")),
                                         Link = ((string)story.Element("link")),
                                         Description = ((string)story.Element("description")),
                                         PubDate = ((string)story.Element("pubDate"))
                                     });

            return realEstateUpdates;
        }
    }
}