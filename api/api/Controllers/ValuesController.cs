using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.database;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private onlineValuesDB database = new onlineValuesDB();

        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //TokenDecoderService service = new TokenDecoderService();

            //TokenInformation token = service.GetTokenInfo(User);

            //return Ok($"HEllo {token.Username}");
            return Ok($"HEllo ");
        }

        /// <summary>
        /// GET api/values/5
        /// </summary>
        /// <param name="id">ID from value</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            string value = database.getByID(id);
            return Ok(value);
        }
        
        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            database.newID(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            database.setByID(id,value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            database.deleteByID(id);
        }
    }
}
