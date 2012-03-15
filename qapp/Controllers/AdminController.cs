using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using qapp.Models;

namespace qapp.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Merchant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Merchant(string name, string address, double latitude, double longitude)
        {
            using (var session = MvcApplication.Store.OpenSession())
            {
                var queueId = "queues/" + Guid.NewGuid();
                var merchant = new Merchant {
                                                Name = name,
                                                Longitude = longitude,
                                                Latitude = latitude,
                                                Address = address,
                                                QueueIds = new[] {queueId}
                                            };


                session.Store(merchant);

                var queue = new Queue {Id = queueId, MerchantId = merchant.Id, LastPosition = 1};

                session.Store(queue);

                session.SaveChanges();
            }
            return View("Ok");
        }

        [HttpGet]
        public ActionResult Ticket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ticket(string userId, string queueId)
        {
            QueueTicketsController c = new QueueTicketsController();
            c.Index(queueId, userId, false);

            return View("Ok");
        }
    }
}
