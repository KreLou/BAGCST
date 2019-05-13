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
            catch (System.Exception ex)
            {
                throw new System.Exception($"{MethodInfo.GetCurrentMethod().Name}-Fehler. {ex.ToString()}");
            }

        }
        private void DecodeToken(ClaimsPrincipal User)
        {
            try
            {
                _tokenInfo = new TokenInformation();

                IEnumerable<Claim> claims = User.Claims;

                _tokenInfo.Username = extractFieldFromClaims(TokenFields.Username, claims);
                _tokenInfo.Firstname = extractFieldFromClaims(TokenFields.Firstname, claims);
                _tokenInfo.Lastname = extractFieldFromClaims(TokenFields.Lastname, claims);
                _tokenInfo.DeviceID = Convert.ToInt32(extractFieldFromClaims(TokenFields.DeviceID, claims));
                _tokenInfo.SessionID = Convert.ToInt32(extractFieldFromClaims(TokenFields.SessionID, claims));
                

            }
            catch (System.Exception ex)
            {
                throw new ArgumentNullException($"{MethodInfo.GetCurrentMethod().Name}-Fehler: Token konnte nicht gelesen werden. {ex.ToString()}");
            }
        }

        private static string extractFieldFromClaims(string field, IEnumerable<Claim> claims)
        {
            return claims.Where(x => x.Type == field).FirstOrDefault().Value;
        }
    }
}
