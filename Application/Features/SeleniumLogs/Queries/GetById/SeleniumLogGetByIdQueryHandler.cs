using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SeleniumLogs.Queries.GetById
{
    public class SeleniumLogGetByIdQueryHandler : IRequestHandler<SeleniumLogGetByIdQuery, SeleniumLogGetByIdDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public SeleniumLogGetByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<SeleniumLogGetByIdDto> Handle(SeleniumLogGetByIdQuery request, CancellationToken cancellationToken)
        {
            var log = await _applicationDbContext.SeleniumLogs.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return SeleniumGetByIdDtoMapper(log);
        }

        private SeleniumLogGetByIdDto SeleniumGetByIdDtoMapper(SeleniumLog? log)
        {
            return new SeleniumLogGetByIdDto()
            {
                Id = log.Id,
                Message = log.Message,
                SentOn = log.SentOn,
                OrderId = (Guid)log.OrderId,

            };
        }
    }
}