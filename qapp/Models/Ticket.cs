using System;
using System.Linq;

namespace qapp.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public string QueueId { get; set; }
        public string ProviderId { get; set; }
        public string UserId { get; set; }
        public DateTime OpenTimeUTC { get; set; }
        public DateTime? CloseTimeUTC { get; set; }

        public static Ticket GetTicket(string providerId, string queueId, string userId) {
            using (var session = MvcApplication.Store.OpenSession()) {
                return
                    session.Query<Ticket>().Where(
                        t => t.ProviderId == providerId && t.QueueId == queueId && t.UserId == userId).FirstOrDefault();
            }
        }

        public Ticket()
        {
            OpenTimeUTC = DateTime.UtcNow;
        }
    }
}