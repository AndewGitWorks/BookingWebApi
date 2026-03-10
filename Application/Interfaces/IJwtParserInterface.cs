using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IJwtParserInterface
    {
        public Task<Guid> GetId(string token);
        public Task<string> GetEmailFromClaimAsync(string token);
    }
}
