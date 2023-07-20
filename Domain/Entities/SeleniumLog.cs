namespace Domain.Entities
{
    public class SeleniumLog
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTimeOffset SentOn { get; set; }
    }
}
