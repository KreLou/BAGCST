using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Selectors;
using api.Models;
using BAGCST.api.User.Database;
using BAGCST.api.User.Models;

namespace api.Services
{
    public class ActivatedSessionHandler: AuthorizationHandler<ActivatedSessionRequirement>
    {
        private readonly ISessionDB _sessionDB;
        private readonly TokenDecoderService _tokenDecoderService;
        public ActivatedSessionHandler(ISessionDB sessionDB, TokenDecoderService tokenDecoderService)
        {
            _sessionDB = sessionDB;
            _tokenDecoderService = tokenDecoderService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ActivatedSessionRequirement requirement)
        {
            long _sessionID = long.MinValue;
            SessionItem _sessionItem = null;
            bool _success = true;

            TokenInformation token = _tokenDecoderService.GetTokenInfo(context.User);

            if (tokenIsEmpty(token))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            _sessionID = token.SessionID;

            _sessionItem = _sessionDB.getSessionByInternalID(_sessionID);

            if (_sessionItem == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            
            _success &= _sessionItem != null;
            _success &= (_sessionItem.ExpirationTime >= DateTime.Now && _sessionItem.StartTime <= DateTime.Now);
            _success &= _sessionItem.isActivied;
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

        private bool tokenIsEmpty(TokenInformation token)
        {
            return token == new TokenInformation();
        }
    }
}
