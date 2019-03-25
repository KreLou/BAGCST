using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public class ActivatedSessionHandler: AuthorizationHandler<ActivatedSessionRequirement>
    {
        private readonly ISessionDB _sessionDB;
        public ActivatedSessionHandler(ISessionDB sessionDB)
        {
            _sessionDB = sessionDB;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ActivatedSessionRequirement requirement)
        {
            //TODO Check if Session is active
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
