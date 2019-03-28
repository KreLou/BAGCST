using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Selectors;
using api.Models;
using System.Security.Claims;
using System.Reflection;

namespace api.Services
{
    public class TokenDecoderService
    {
        TokenInformation _tokenInfo;

        public TokenInformation GetTokenInfo(ClaimsPrincipal User)
        {
            try
            {
                if (_tokenInfo == null) DecodeToken(User);
                return _tokenInfo;
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodInfo.GetCurrentMethod().Name}-Fehler. {ex.ToString()}");
            }
                
        }
        private void DecodeToken(ClaimsPrincipal User)
        {
            try
            {
                _tokenInfo = new TokenInformation();

                foreach (var claim in User.Claims)
                {
                    switch (claim.Type)
                    {
                        case nameof(TokenFields.Username):
                            _tokenInfo.Username = claim.Value;
                            break;
                        case nameof(TokenFields.Firstname):
                            _tokenInfo.Firstname = claim.Value;
                            break;
                        case nameof(TokenFields.Lastname):
                            _tokenInfo.Lastname = claim.Value;
                            break;
                        case nameof(TokenFields.DeviceID):
                            _tokenInfo.DeviceID = claim.Value;
                            break;
                        case nameof(TokenFields.SessionID):
                            _tokenInfo.SessionID = claim.Value;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException($"{MethodInfo.GetCurrentMethod().Name}-Fehler: Token konnte nicht gelesen werden. {ex.ToString()}");
            }
        }
    }
}
