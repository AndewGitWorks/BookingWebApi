using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IJwtParserInterface
    {
        public Task<Guid> GetId(string token);
    }
}
