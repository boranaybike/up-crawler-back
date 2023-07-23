using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.SeleniumLogs.Add
{
    internal class SeleniumLogAddCommandHandler : IRequestHandler<SeleniumLogAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public SeleniumLogAddCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<Guid>> Handle(SeleniumLogAddCommand request, CancellationToken cancellationToken)
        {
            var log = new SeleniumLog
            {
                Id = Guid.NewGuid(),
                Message = request.Message,
                SentOn = DateTime.UtcNow,
                OrderId = (Guid)request.OrderId,

            };

            await _applicationDbContext.SeleniumLogs.AddAsync(log, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>(log.Id);
        }


    }
}
