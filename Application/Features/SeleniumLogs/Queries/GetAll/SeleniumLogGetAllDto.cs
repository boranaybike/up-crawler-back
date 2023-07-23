namespace Application.Features.SeleniumLogs.Queries.GetAll
{
    public class SeleniumLogGetAllDto
    {

        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public Guid OrderId { get; set; }
    }
}
