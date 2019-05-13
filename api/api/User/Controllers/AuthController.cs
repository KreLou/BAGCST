using System;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Handler;
using System.Threading;
using BAGCST.api.User.Database;
using BAGCST.api.User.Models;

namespace BAGCST.api.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserDB _userDB;
        private readonly IUserDeviceDB _userDeviceDB;
        private readonly ISessionDB _sessionDB;
        private readonly SendMailService _sendMailService;


        public AuthController(IUserDB userDB, IUserDeviceDB userDeviceDB, ISessionDB sessionDB, SendMailService mailService)
        {
            _userDB = userDB;
            _userDeviceDB = userDeviceDB;
            _sessionDB = sessionDB;
            _sendMailService = mailService;
        }

        [HttpPost("register")]
        public ActionResult register([FromBody] LoginDataItem loginData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserItem user = _userDB.getUserByName(loginData.Username);

            if (user == null)
            {
                return NotFound($"No UserItem found for Username: {loginData.Username}");
            }
            UserDeviceItem device = _userDeviceDB.getDeviceByNameAndUser(user.UserID, loginData.DeviceName);

            if (device == null)
            {
                device = createNewUserDevice(loginData, user);
            }

            SessionItem session = _sessionDB.findExistingSession(user.UserID, device.DeviceID);
            if (session == null)
            {
                session = createNewSession(user, device);

                //Send mail
                _sendMailService.setUser(user);

                Thread thread = new Thread(() => _sendMailService.sendRegistrationMail(session));
                thread.Start();
                //mailHandler.sendRegistrationMail(session);

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
            device = _userDeviceDB.createNewUserDevice(device);
            return device;
        }

        private SessionItem createNewSession(UserItem user, UserDeviceItem device)
        {
            SessionItem session = new SessionItem
            {
                DeviceID = device.DeviceID,
                UserID = user.UserID,
                StartTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddMonths(ServerConfigHandler.ServerConfig.Default_SessionUseTimeInMonth),
                isActivied = false
            };
            session.setActivationCode();
            session.setShortHashCode();

            session = _sessionDB.createNewSession(session);

            JWTCreationHandler jWTCreationHandler = new JWTCreationHandler(session, user);
            session.Token = jWTCreationHandler.Token;

            _sessionDB.updateSessionItem(session.InternalID, session);
            
            return session;
        }
        
        
        [HttpPost("activate/{code}")]
        public IActionResult activate(string code)
        {
            SessionItem session = _sessionDB.getSessionItemByActivationCode(code);
            if (session == null)
            {
                return NotFound("No Session found");
            }
            if (session.isActivied)
            {
                return BadRequest("Session is already activted");
            }
            session.isActivied = true;
            session = _sessionDB.updateSessionItem(session.InternalID, session);
            return Ok(session);
        }
    



    }
}