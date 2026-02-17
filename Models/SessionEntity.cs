using Azure;
using Azure.Data.Tables;

namespace hanumachantsApi.Models
{
    public class SessionEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "SESSION";
        public required string RowKey { get; set; }
        public DateTimeOffset? RangeStart { get; set; }
        public DateTimeOffset? RangeEnd { get; set; }
        public string CompletedDates { get; set; } = "";
        public DateTimeOffset Expiry {  get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
