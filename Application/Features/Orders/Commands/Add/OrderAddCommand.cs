using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.Add
{
    public class OrderAddCommand : IRequest<Response<int>>
    {
        public Guid Id { get; set; }
        public int RequestedAmount { get; set; }
        public int TotalFoundAmount { get; set; }
        public ProductCrawlType ProductCrawlType { get; set; }
        public ICollection<OrderEvent>? OrderEvents { get; set; } // Bot started...
        public DateTimeOffset CreatedOn { get; set; }

    }
}
