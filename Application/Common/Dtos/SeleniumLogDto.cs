namespace Application.Common.Dtos
{
    public class SeleniumLogDto
    {
        public string Message { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public Guid OrderId { get; set; }

        public SeleniumLogDto(Guid orderId, string message)
        {
            OrderId = orderId;
            Message = message;
            SentOn = DateTimeOffset.Now;
        }

    }
}