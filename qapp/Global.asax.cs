using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client;
using Raven.Abstractions.Data;
using Raven.Client.Document;
using qapp.Models;

namespace qapp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static IDocumentStore Store { get; private set; }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
            parser.Parse();

            Store = new DocumentStore
            {
                ApiKey = parser.ConnectionStringOptions.ApiKey,
                Url = parser.ConnectionStringOptions.Url
            };
            Store.Initialize();

            InitDB();
        }

        static Random s_random = new Random();

        private void InitDB()
        {
            using (var s = Store.OpenSession())
            {
                if (s.Query<Merchant>().Any()) return;
                for (int i = 0; i < 10; i++)
                {
                    var queueId =  "queues/" + Guid.NewGuid().ToString();
                    var merchant = new Merchant
                    {
                        Id = "merchants/" + Guid.NewGuid(),
                        Address = "addr " + Guid.NewGuid(),
                        Longitude = s_random.NextDouble(),
                        Latitude = s_random.NextDouble(),
                        Name = Guid.NewGuid().ToString(),
                        QueueIds = new[] {queueId}
                    };
                    var queue = new Queue { Id = queueId, MerchantId = merchant.Id };

                    s.Store(queue);
                    s.Store(merchant);
                }
                s.SaveChanges();
            }
        }
    }
}