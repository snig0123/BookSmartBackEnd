using System;
using System.ComponentModel.DataAnnotations;

namespace BookSmartBackEndDatabase
{
    public class Worker
    {
        [Key]
        public Guid WORKER_ID { get; set; }
        public string? WORKER_FORENAME { get; set; }
        public string? WORKER_SURNAME { get; set; }
        public DateTime WORKER_STARTDATE { get; set; }
        public DateTime? WORKER_ENDDATE { get; set; }
        public string? WORKER_DAYS { get; set; }
        public TimeSpan WORKER_STARTTIME { get; set; }
        public TimeSpan WORKER_ENDTIME { get; set; }
        public bool WORKER_DELETED { get; set; }
        public Guid BUSINESS_ID { get; set; }
    }
}
