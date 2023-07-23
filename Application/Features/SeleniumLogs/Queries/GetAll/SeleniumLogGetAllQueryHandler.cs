using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SeleniumLogs.Queries.GetAll
{
    public class SeleniumLogGetAllQueryHandler : IRequestHandler<SeleniumLogGetAllQuery, List<SeleniumLogGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public SeleniumLogGetAllQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SeleniumLogGetAllDto>> Handle(SeleniumLogGetAllQuery request, CancellationToken cancellationToken)
        {
            var logs = await _context.SeleniumLogs
                .Select(log => new SeleniumLogGetAllDto
                {
                    Id = log.Id,
                    Message = log.Message,
                    SentOn = log.SentOn,
                    OrderId = (Guid)log.OrderId,

                })
                .ToListAsync(cancellationToken);

            return logs;
        }
    }
}
