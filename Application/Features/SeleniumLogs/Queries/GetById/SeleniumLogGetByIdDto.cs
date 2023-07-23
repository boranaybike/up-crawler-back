namespace Application.Features.SeleniumLogs.Queries.GetById
{
    public class SeleniumLogGetByIdDto

    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public Guid OrderId { get; set; }

    }
}
