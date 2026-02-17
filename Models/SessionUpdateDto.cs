namespace hanumachantsApi.Models
{
    public class SessionUpdateDto
    {
        public string? CompletedDates { get; set; }
        public DateTimeOffset? RangeStart { get; set; }
        public DateTimeOffset? RangeEnd { get; set; }
        public DateTimeOffset? Expiry { get; set; }
    }
}
