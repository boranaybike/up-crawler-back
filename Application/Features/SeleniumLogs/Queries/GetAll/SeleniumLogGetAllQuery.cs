using MediatR;

namespace Application.Features.SeleniumLogs.Queries.GetAll
{
    public class SeleniumLogGetAllQuery : IRequest<List<SeleniumLogGetAllDto>>
    {
    }
}
