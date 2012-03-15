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

/*        public Ticket CreateTicket(string userId)
        {
            CurrentPosition++;
            var ticket = new Ticket
            {
                UserId = userId,
                ProviderId = MerchantId,
                QueueId = Id
            };
            return ticket;
        }*/
    }
}