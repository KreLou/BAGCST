using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Models;
using api.Databases;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IContactsDB database = getDatabase();
        private static IContactsDB getDatabase()
        {
            //TODO set environment
            return new offlineDB_contacts();
        }

        /// <summary>
        /// returns the ContactItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContactItem|NotFound</returns>
        [HttpGet("{id}")]
        public ActionResult<ContactItem> getContactItem(int id)
        {
            ContactItem item = database.getContactItem(id);
            if(item==null)
            {
                return NotFound($"No ContactItem found for id: {id}");
            }
            else
            {
                return Ok(item);
            }
        }

        /// <summary>
        /// returns an array of ContactItems
        /// </summary>
        /// <returns>ContactItems[]</returns>
        [HttpGet]
        public ActionResult<ContactItem[]> getAllContactItems()
        {
            ContactItem[] items = database.getAllContactItems();
            return Ok(items);
        }

        /// <summary>
        /// returns the edited ContactItem for the given ID and ContactItem. If ID is not found, it returns NotFound. 
        /// If the ContactItem is not found, it returns BadRequest.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item_in"></param>
        /// <returns>ContactItem|NotFound|BadRequest</returns>
        [HttpPut("{id}")]
        public ActionResult<ContactItem> editContactItem(int id, [FromBody]ContactItem item_in)
        {
            //TODO check for permission
            //Check if id is valid
            if (database.getContactItem(id) == null)
            {
                return NotFound(($"No ContactItem found for id: {id}"));
            }

            //Check if item not null
            if(item_in == null)
            {
                return BadRequest("ContactItem not found");
            }

            //update existing item
            ContactItem item_out = database.editContactItem(id, item_in);

            //return new item
            return Ok(item_out);
        }

        /// <summary>
        /// deletes the ContactItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult deleteContactItem(int id)
        {
            //TODO check for permission
            if (database.getContactItem(id)==null)
            {
                return NotFound(($"No ContactItem found for id: {id}"));
            }
            database.deleteContactItem(id);
            return Ok();
        }

        /// <summary>
        /// creates a ContactItem based on the given ContactItem. If the given ContactItem is null, it returns BadRequest.
        /// </summary>
        /// <param name="item_in"></param>
        /// <returns>ContactItem|BadRequest</returns>
        [HttpPost]
        public ActionResult<ContactItem> createContactItem(ContactItem item_in)
        {
            //TODO check for permission
            if( item_in == null)
            {
                return BadRequest("ContactItem not found");
            }
            
            ContactItem item_out = database.createContactItem(item_in);
            return Created("", item_out);
        }

    }
}