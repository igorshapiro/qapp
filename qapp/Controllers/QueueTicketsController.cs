using System.Web.Mvc;
using qapp.Models;
using System;

namespace qapp.Controllers
{
    public class QueueTicketsController : Controller
    {
        /// <summary>
        /// Returns the stastus ("ok", "error"), position in queue, estimatedtime to be first in line, ticketId 
        /// </summary>
        /// <param name="providerId"></param>
        /// <param name="queueId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult GetTicket(string providerId, string queueId, string userId) {
            Ticket ticket = Ticket.GetTicket(providerId, queueId, userId);
            return ticket == null ? Json(new {status = "error"}) : Json(new { status = "OK", position = 1, estimatedTime = 4235235, ticketId = ticket.Id });
        }

        [HttpPost]
        public ActionResult Index(string merchantId, string queueId, string userId, string queueTicketId, bool isProcessed)
        {
            try
            {
                using (var session = MvcApplication.Store.OpenSession())
                {
                    session.Advanced.AllowNonAuthoritativeInformation = false;
                    session.Advanced.UseOptimisticConcurrency = true;

                    var queue = session.Load<Queue>(queueId);
                    queue.LastPosition++;
                    var ticket = new Ticket { UserId = userId, ProviderId = merchantId, QueueId = queueId };
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
        public ActionResult Index(string queueTicketId)
        {
            using (var session = MvcApplication.Store.OpenSession())
            {
                session.Advanced.AllowNonAuthoritativeInformation = false;
                session.Advanced.UseOptimisticConcurrency = true;

                var ticket = session.Load<Ticket>(queueTicketId);
                ticket.CloseTimeUTC = DateTime.UtcNow;

                return new EmptyResult();
            }
        }
    }
}
