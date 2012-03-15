using System.Linq;
using Raven.Client;
using Raven.Client.Linq;

namespace qapp.Models
{
    public class TextUtils
    {
        public static string[] GetKeywords(string s) 
        {
            return s.Split(' ', '\t', ';', ',', '.');
        }
    }

    public class Merchant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string[] QueueIds {get;set;}

        public string[] Keywords { 
            get {
                return new[] { Name, Address }
                    .SelectMany(s => TextUtils.GetKeywords(s))
                    .ToArray();
            } 
        }

        public static Merchant[] GetAll(IDocumentSession session, string[] keywords)
        {
            if (keywords == null || keywords.Length == 0)
                    return session.Query<Merchant>().ToArray();
            return session.Query<Merchant>()
                .Where(m => m.Keywords.Any(k => k.In(keywords)))
                .ToArray();
        }

        public Queue[] GetQueues()
        {
            if (QueueIds == null || QueueIds.Length == 0) return new Queue[] { };
            using (var session = MvcApplication.Store.OpenSession())
            {
                return session.Load<Queue>(QueueIds).ToArray();
            }
        }
    }

    public class User
    {
        public string Id { get; set; }
    }
}