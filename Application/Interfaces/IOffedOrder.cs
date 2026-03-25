using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IOffedOrder
    {
        public Task CreateOffer(Guid userId, Guid orderId, CancellationToken ct);
    }
}
