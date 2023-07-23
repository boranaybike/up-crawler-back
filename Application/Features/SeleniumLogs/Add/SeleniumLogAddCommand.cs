using Domain.Common;
using MediatR;

namespace Application.Features.SeleniumLogs.Add
{
    public class SeleniumLogAddCommand: IRequest<Response<Guid>>
    {
        public string Message { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public Guid OrderId { get; set; }

    }
}
