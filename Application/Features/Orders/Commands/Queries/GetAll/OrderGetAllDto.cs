﻿namespace Application.Features.Orders.Commands.Queries.GetAll
{
    public class OrderGetAllDto
    {
        public Guid Id { get; set; }
        public int RequestedAmount { get; set; }
        public string? ProductCrawlType { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
