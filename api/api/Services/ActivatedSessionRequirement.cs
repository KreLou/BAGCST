using Microsoft.AspNetCore.Authorization;
using System;

namespace api.Services
{
    public class ActivatedSessionRequirement : IAuthorizationRequirement
    {
        public DateTime CurrentTimestamp { get { return DateTime.Now; } }
    }
}
