using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoFiWebhookDemo.Models
{
    public class KoFiData
    {
        public string MessageId { get; set; }
        public string Timestamp { get; set; }
        public string Type { get; set; }
        public string FromName { get; set; }
        public string Message { get; set; }
        public decimal Amount { get; set; }
        public string Url { get; set; }
    }
}
