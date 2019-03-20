using api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Cryptography;

namespace api.Handler
{
    public class JWTCreationHandler
    {
        public SessionItem Session { get; private set; }
        public UserItem User { get; private set; }
        public string Token { get; private set; }

        private ServerConfig config;

        public JWTCreationHandler(SessionItem session, UserItem user)
        {
            this.config = ServerConfigHandler.ServerConfig;
            this.Session = session;
            this.User = user;
            createToken();

        }

        private void createToken()
        {
            
            var claims = new[]
            {
            //new Claim("userid", User.UserID.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, User.Username),
            new Claim(JwtRegisteredClaimNames.GivenName, User.Firstname),
            new Claim(JwtRegisteredClaimNames.FamilyName, User.Lastname),
            //new Claim("deviceid", Session.DeviceID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JWT_SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config.JWT_Issuer,
                audience: config.JWT_Audience,
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: creds);
            this.Token = new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
