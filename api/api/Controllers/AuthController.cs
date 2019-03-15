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
                device = createNewUserDevice(loginData, user);
            }

            SessionItem session = sessionDatabase.findExistingSession(user.UserID, device.DeviceID);
            if (session == null)
            {
                session = createNewSession(user, device);
                return Created("", session);
            }

            return Ok(session);

        }

        private UserDeviceItem createNewUserDevice(LoginDataItem loginData, UserItem user)
        {
            UserDeviceItem device = new UserDeviceItem
            {
                CreateTime = DateTime.Now,
                DeviceName = loginData.DeviceName,
                UserID = user.UserID
            };
            device = deviceDatabasae.createNewUserDevice(device);
            return device;
        }

        private SessionItem createNewSession(UserItem user, UserDeviceItem device)
        {
            SessionItem session = new SessionItem
            {
                DeviceID = device.DeviceID,
                UserID = user.UserID,
                StartTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddMonths(10),
                isActivied = false
            };
            session.setActivationCode();
            JWTCreationHandler jWTCreationHandler = new JWTCreationHandler(session, user);
            session.Token = jWTCreationHandler.Token;

            sessionDatabase.createNewSession(session);
            return session;
        }
        
        
        [HttpPost("activate/{code}")]
        public IActionResult activate(string code)
        {
            SessionItem session = sessionDatabase.getSessionItemByActivationCode(code);
            if (session == null)
            {
                return NotFound("No Session found");
            }
            if (session.isActivied)
            {
                return BadRequest("Session is already activted");
            }
            session.isActivied = true;
            session = sessionDatabase.updateSessionItem(session.InternalID, session);
            return Ok(session);
        }
    



    }
}