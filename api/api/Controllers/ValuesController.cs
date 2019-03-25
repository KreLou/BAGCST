using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Selectors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize(Policy ="activatedSession")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
 
        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var username = "";
            var firstname = "";
            var lastname = "";
            var deviceID = int.MinValue;
            foreach(var claim in User.Claims)
            {
                string type = claim.Type;
                string value = claim.Value;

                if (type == TokenFields.Username) username = value;
                else if (type == TokenFields.Firstname) firstname = value;
                else if (type == TokenFields.Lastname) lastname = value;
                else if (type == TokenFields.DeviceID) deviceID = Convert.ToInt32(value);
            }
            return new string[] {
                $"Username:  {username}",
                $"Firstname: {firstname}",
                $"Lastname:  {lastname}",
                $"Device_ID: {deviceID}",
            };
        }

        
        /// <summary>
        /// GET api/values/5
        /// </summary>
        /// <param name="id">ID from value</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value " + id;
        }
        
        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
