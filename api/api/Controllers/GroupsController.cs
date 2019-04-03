using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Models;
using api.Databases;
using api.offlineDB;
using api.Exception;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private IGroupsDB database = getDatabase();
        private IRightsDB rightsDatabase = getRightsDatabase();

        private static IRightsDB getRightsDatabase()
        {
            return new offlineDB_Rights();
        }

        private static IGroupsDB getDatabase()
        {
            return new offlineDB_Groups();
        }

        /// <summary>
        /// returns the Group for the given ID. If it's not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Group</returns>
        [HttpGet("{id}")]
        public ActionResult<Group> getGroup(int id)
        {
            Group group = database.getGroup(id);
            if (group == null)
            {
                return NotFound($"No Group found for id: {id}");
            }
            else
            {
                return Ok(group);
            }
        }

        [HttpGet]
        public ActionResult<Group[]> getAllGroups()
        {
            Group[] groups = database.getAllGroups();
            return Ok(groups);
        }

        [HttpPut("{id}")]
        public ActionResult<Group> editGroup(int id, [FromBody] Group group_in)
        {
            //Check if id is valid
            //if (database.getGroup(id) == null)
            //{
            //    return NotFound(($"No Group found for ID: {id}"));
            //}

            //Check if groups are not null
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //checked if Path and ID are valid
            try
            {
                validateRights(group_in.Rights);
            }
            catch (RightIDNotFoundException IDNotFound)
            {
                return NotFound("No ID found for the Path:" + IDNotFound.RightPath);
            }
            catch (RightItemNotFoundException ItemNotFound)
            {
                return NotFound("No Right found for the ID:" + ItemNotFound.RightID);
            }

            //update existing group
            Group group_out = database.editGroup(id, group_in);

            //return new item
            return Ok(group_out);
        }

        [HttpDelete("{id}")]
        public ActionResult deleteGroup(int id)
        {
            //TODO check for permission
            if (database.getGroup(id) == null)
            {
                return NotFound(($"No Group found for id: {id}"));
            }
            database.deleteGroup(id);
            return Ok();
        }

        /// <summary>
        /// creates a Group based on the given Group. If the given Group is null, it returns BadRequest.
        /// </summary>
        /// <param name="group_in"></param>
        /// <returns>Group|BadRequest</returns>
        [HttpPost]
        public ActionResult<Group> createGroup(Group group_in)
        {
            //TODO check for permission

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                validateRights(group_in.Rights);
            }catch(RightIDNotFoundException ridnfe)
            {
                return NotFound("No RightID found for Right.Path: " +ridnfe.RightPath);
            } catch(RightItemNotFoundException rightItemNotFoundEx)
            {
                return NotFound($"No RightItem found for ID {rightItemNotFoundEx.RightID}");
            } catch(RightPathInvalidException)
            {
                return BadRequest("Path is invalid");
            }

            foreach(Group group in database.getAllGroups())
            {
                if(group_in.Name == group.Name)
                {
                    return BadRequest("GroupName already used. Try another one.");
                }
            }

            
            Group group_out = database.createGroup(group_in);
            return Created("", group_out);
        }

        /// <summary>
        /// Validate all Rights
        /// Throws RightIDNotFoundException and RightItemNotFoundException
        /// </summary>
        /// <param name="rights"></param>
        private void validateRights(Right[] rights)
        {
            foreach (Right right in rights)
            {
                if (String.IsNullOrWhiteSpace(right.RightID.ToString()))
                {
                    throw new RightIDNotFoundException(right.Path);
                }
                else
                {
                    Right databaseRight = rightsDatabase.getRight(right.RightID);
                    if (databaseRight == null)
                    {
                        throw new RightItemNotFoundException(right.RightID);
                    }
                }
                if(String.IsNullOrWhiteSpace(right.Path))
                {
                    throw new RightPathInvalidException(right.Path);
                }
            }
        }
    }
}