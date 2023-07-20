using MediatR;

namespace Application.Features.SeleniumLogs.Queries.GetById
{
    public class SeleniumLogGetByIdQuery : IRequest<SeleniumLogGetByIdDto>
    {
        public Guid Id { get; set; }

        public SeleniumLogGetByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
