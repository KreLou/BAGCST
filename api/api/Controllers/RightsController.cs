using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Models;
using api.Databases;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RightsController : ControllerBase
    {
        private IRightsDB database = getDatabase();
        private static IRightsDB getDatabase()
        {
            return new offlineDB_Rights();
        }

        /// <summary>
        /// returns the Right for the given ID. If it's not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Right</returns>
        [HttpGet("{id}")]
        public ActionResult<Right> getRight(int id)
        {
            Right right = database.getRight(id);
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
            Right[] rights = database.getAllRights();
            return Ok(rights);
        }

        [HttpPut("{id}")]
        public ActionResult<Right> editRight(int id, [FromBody] Right right_in)
        {
            //Check if id is valid
            if (database.getRight(id) == null)
            {
                return NotFound(($"No Right found for ID: {id}"));
            }

            //Check if rights are not null
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //update existing right
            Right right_out = database.editRight(id, right_in);

            //return new item
            return Ok(right_out);
        }

        [HttpDelete("{id}")]
        public ActionResult deleteRight(int id)
        {
            //TODO check for permission
            if (database.getRight(id) == null)
            {
                return NotFound(($"No Right found for id: {id}"));
            }
            database.deleteRight(id);
            return Ok();
        }

        /// <summary>
        /// creates a Right based on the given Right. If the given Right is null, it returns BadRequest.
        /// </summary>
        /// <param name="right_in"></param>
        /// <returns>Right|BadRequest</returns>
        [HttpPost]
        public ActionResult<Right> createRight(Right right_in)
        {
            //TODO check for permission
            if (right_in == null)
            {
                return BadRequest("Right not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Right right_out = database.createRight(right_in);
            return Created("", right_out);
        }

    }
}
