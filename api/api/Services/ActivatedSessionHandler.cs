using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Selectors;
using api.Models;
using System.Linq;

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
            long _sessionID = long.MinValue;
            SessionItem _sessionItem = null;
            bool _success = true;

            foreach (var claim in context.User.Claims.ToList())
            {
                if (claim.Type == TokenFields.SessionID)
                {
                   _sessionID = Convert.ToInt64(claim.Value); 
                }
            }
            _sessionItem = _sessionDB.getSessionByInternalID(_sessionID);
            
            _success = _sessionItem != null;
            _success = (_sessionItem.ExpirationTime >= DateTime.Now && _sessionItem.StartTime <= DateTime.Now);
            _success = _sessionItem.isActivied;
            // TODO krelou should we compare userIDs...?
            // _success = _sessionItem.UserID == context.User.Claims.

            if (_success)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            
            return Task.CompletedTask;
        }
    }
}
