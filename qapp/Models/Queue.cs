using System;
using System.Linq;
using Raven.Client;

namespace qapp.Models {
    public class Queue
    {
        public string MerchantId { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// Format:
        ///     YYYYMMDDXXX
        /// </summary>
        public long CurrentPosition { get; set; }
        public long LastPosition { get; set; }

        public TimeSpan GetAverageProcessTime(IDocumentSession session)
        {
            var start = DateTime.UtcNow.AddMinutes(-30);
            var end = DateTime.UtcNow;

            var tickets = session.Query<Ticket>()
                .Where(t => t.QueueId == Id && t.TimeToServe != null && t.CloseTimeUTC >= start && t.CloseTimeUTC < end)
                .ToArray();
            if (!tickets.Any())
                return TimeSpan.FromMinutes(15);
            return TimeSpan.FromSeconds((long)tickets.Average(t => t.TimeToServe.Value));
        }
    }
}