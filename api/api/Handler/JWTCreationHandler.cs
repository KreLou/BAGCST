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
using api.Selectors;

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
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(this.config.JWT_SecurityKey);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(TokenFields.Username, this.User.Username),
                    new Claim(TokenFields.Firstname, this.User.Firstname),
                    new Claim(TokenFields.Lastname, this.User.Lastname),
                    new Claim(TokenFields.DeviceID, this.Session.DeviceID.ToString()),
                    new Claim(TokenFields.SessionID, this.Session.InternalID.ToString())
                }),
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            this.Token = tokenHandler.WriteToken(token); ;
        }
    }
}
