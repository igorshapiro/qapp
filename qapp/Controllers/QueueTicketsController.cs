using System.Web.Mvc;
using qapp.Models;
using System;
using System.Linq;

namespace qapp.Controllers
{
    public class QueueTicketsController : Controller
    {
        [HttpPost]
        public ActionResult Index(string merchantId, string queueId, string userId, string queueTicketId, bool isProcessed)
        {
            try
            {
                using (var session = MvcApplication.Store.OpenSession())
                {
                    session.Advanced.AllowNonAuthoritativeInformation = false;
                    session.Advanced.UseOptimisticConcurrency = true;

                    var merchant = session.Load<Merchant>(merchantId);

                    var queue = session.Load<Queue>(queueId);
                    queue.LastPosition++;
                    var ticket = new Ticket { UserId = userId, ProviderId = merchantId, QueueId = queueId, MerchantName = merchant.Name };
                    session.Store(ticket);
                    session.SaveChanges();

                    return Json(new { status = "ok", position = queue.LastPosition - queue.CurrentPosition, ticketId = ticket.Id });
                }
            }
            catch (Exception)
            {
                return Json(new {status = "failed"});
            }
        }

        [HttpGet]
        public ActionResult Index(string userId)
        {
            using (var session = MvcApplication.Store.OpenSession())
            {
                session.Advanced.AllowNonAuthoritativeInformation = true;

                var tickets =
                    session.Query<Ticket>()
                        .Where(t => t.UserId == userId && t.CloseTimeUTC == null)
                        .ToArray();

                var result = from t in tickets
                             let q = session.Load<Queue>(t.QueueId)
                             select new
                             {
                                 name = t.MerchantName,
                                 queueId = t.QueueId,
                                 queueTicketId = t.Id,
                                 queuePosition = t.Position - q.CurrentPosition
                             };

                return Json(result.ToArray(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Next(string queueTicketId)
        {
            using (var session = MvcApplication.Store.OpenSession())
            {
                session.Advanced.UseOptimisticConcurrency = true;
                session.Advanced.AllowNonAuthoritativeInformation = false;

                var ticket = session.Load<Ticket>(queueTicketId);
                ticket.CloseTimeUTC = DateTime.UtcNow;

                var queue = session.Load<Queue>(ticket.QueueId);
                queue.CurrentPosition++;

                session.SaveChanges();

                return Json(new { newPosition = queue.CurrentPosition}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpDelete]
        public ActionResult Index(string queueTicketId, string dummyArg1)
        {
            using (var session = MvcApplication.Store.OpenSession())
            {
                session.Advanced.AllowNonAuthoritativeInformation = false;
                session.Advanced.UseOptimisticConcurrency = true;

                var ticket = session.Load<Ticket>(queueTicketId);
                ticket.CloseTimeUTC = DateTime.UtcNow;

                session.SaveChanges();

                return new EmptyResult();
            }
        }
    }
}
