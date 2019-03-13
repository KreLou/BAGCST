using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.offlineDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using api.Handler;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IUserDB userDatabase = getUserDatabase();
        private IUserDeviceDB deviceDatabasae = getUserDeviceDatabase();
        private ISessionDB sessionDatabase = getSessionDatabase();

        #region DatabaseCreator
        private static ISessionDB getSessionDatabase()
        {
            return new offlineSessionDB();
        }

        private static IUserDeviceDB getUserDeviceDatabase()
        {
            return new offlineUserDeviceDB();
        }

        private static IUserDB getUserDatabase()
        {
            return new offlineUserDB();
        }
        #endregion

        [HttpPost("register")]
        public ActionResult register([FromBody] LoginDataItem loginData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserItem user = userDatabase.getUserByName(loginData.Username);

            if (user == null)
            {
                return NotFound($"No UserItem found for Username: {loginData.Username}");
            }
            UserDeviceItem device = deviceDatabasae.getDeviceByNameAndUser(user.UserID, loginData.DeviceName);

            if (device == null)
            {
                device = new UserDeviceItem
                {
                    CreateTime = DateTime.Now,
                    DeviceName = loginData.DeviceName,
                    UserID = user.UserID
                };
                device = deviceDatabasae.createNewUserDevice(device);
            }

            SessionItem session = sessionDatabase.findExistingSession(user.UserID, device.DeviceID);
            if (session == null)
            {
                session = new SessionItem
                {
                    DeviceID = device.DeviceID,
                    UserID = user.UserID,
                    StartTime = DateTime.Now,
                    ExpirationTime = DateTime.Now.AddMonths(10)
                };
                JWTCreationHandler jWTCreationHandler = new JWTCreationHandler(session, user);
                session.Token = jWTCreationHandler.Token;

                sessionDatabase.createNewSession(session);
            }

            return Ok(session);

        }




    }
}