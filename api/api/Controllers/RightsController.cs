using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Models;
//using api.Databases;
using Microsoft.AspNetCore.Http;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RightsController : ControllerBase
    {
        private IRightsDB database = getDatabase();

        private static IRightsDB getDatabase()
        {
            return null;
        }

        /// <summary>
        /// returns the Right for the given ID. If it's not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Right> getRight(int id)
        {
            Right right = null; //database
            if (right == null)
            {
                return NotFound($"No right found for id: {id}");
            }
            else
            {
                return Ok(right);
            }
        }

        [HttpGet]
        public ActionResult<Right[]> getAllRights()
        {
            Right[] rights = null; //database
            return Ok(rights);
        }

        [HttpPut("{id}")]
        public ActionResult<Right> editRight(int id, [FromBody] Right[] rights_in)
        {
            //Check if id is valid
            if (id == null) //database
            {
                return NotFound(($"No Right found for ID: {id}"));
            }

            //Check if rights are not null
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //update existing rights
            Right[] rights_out = null; //database

            //return new item
            return Ok(rights_out);
        }

        [HttpDelete("{id}")]
        public ActionResult deleteRight(int id)
        {
            //TODO check for permission
            if (id == null) //database
            {
                return NotFound(($"No ContactItem found for id: {id}"));
            }
            //delete id. Database
            return Ok();
        }

    }
}
