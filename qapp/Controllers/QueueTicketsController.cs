using System.Web.Mvc;
using qapp.Models;

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
    }
}
