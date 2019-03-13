using api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Handler
{
    public class JWTCreationHandler
    {
        public SessionItem Session { get; private set; }
        public UserItem User { get; private set; }
        public string Token { get; private set; }

        public JWTCreationHandler(SessionItem session, UserItem user)
        {
            this.Session = session;
            this.User = user;
            createToken();

        }

        private void createToken()
        {
            
            var claims = new[]
            {
            new Claim("userid", User.UserID.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, User.Username),
            new Claim(JwtRegisteredClaimNames.GivenName, User.Firstname),
            new Claim(JwtRegisteredClaimNames.FamilyName, User.Lastname),
            new Claim("deviceid", Session.DeviceID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BA-Glauchau Studenten APP der WI16"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "BA-Glauchau APP",
                audience: "BA-Glauchau APP",
                claims: claims,
                signingCredentials: creds);
            this.Token =  new JwtSecurityTokenHandler().WriteToken(token);
        
        }
    }
}
