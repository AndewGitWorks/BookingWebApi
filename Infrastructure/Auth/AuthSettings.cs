using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Auth
{
    public class AuthSettings
    {
        public TimeSpan Expires { get; set; }
        public string? SecretKey { get; set; }
    }
}
