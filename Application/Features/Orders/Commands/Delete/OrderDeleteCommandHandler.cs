using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Commands.Delete
{
    public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _context;

        public OrderDeleteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Guid>> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Where(x => x.Id == request.Id).FirstOrDefaultAsync();


            Console.WriteLine(order);
            if (order is null)
            {
                throw new ArgumentException();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"successfully deleted.", order.Id);
        }
    }
}