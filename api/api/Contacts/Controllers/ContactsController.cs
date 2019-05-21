using Microsoft.AspNetCore.Mvc;
using BAGCST.api.StudySystem.Database;
using BAGCST.api.User.Database;
using BAGCST.api.Contacts.Database;
using BAGCST.api.Contacts.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;

namespace BAGCST.api.Contacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IStudyCourseDB studyCourseDB;
        private readonly IUserTypeDB userTypeDB;
        private readonly IContactsDB contactsDB;

        public ContactsController(IContactsDB contactsDB)
        {
            this.contactsDB = contactsDB;
        }

        /// <summary>
        /// Returns the ContactItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContactItem|NotFound</returns>
        [HttpGet("{id}")]
        public ActionResult<ContactItem> getContactItem(int id)
        {
            ContactItem item = contactsDB.getContactItem(id);
            if(item==null)
            {
                return NotFound($"No ContactItem found for ID: {id}");
            }
            else
            {
                return Ok(item);
            }
        }

        /// <summary>
        /// Returns an array of ContactItems.
        /// </summary>
        /// <returns>ContactItems[]</returns>
        [HttpGet]
        public ActionResult<ContactItem[]> getAllContactItems()
        {
            ContactItem[] items = contactsDB.getAllContactItems();
            return Ok(items);
        }

        /// <summary>
        /// Returns the edited ContactItem for the given ID and ContactItem. If ID is not found, it returns NotFound. 
        /// If the ContactItem is not found, it returns BadRequest.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item_in"></param>
        /// <returns>ContactItem|NotFound|BadRequest</returns>
        [HttpPut("{id}")]
        public ActionResult<ContactItem> editContactItem(int id, [FromBody]ContactItem item_in)
        {
            //Check if item not null
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check if ID is valid
            if (contactsDB.getContactItem(id) == null)
            {
                return NotFound(($"No ContactItem found for ID: {id}"));
            }

            ContactItem foundByEmail = contactsDB.getContactItem(item_in.Email);
            if (foundByEmail != null && foundByEmail.ContactID != id)
            {
                return BadRequest("This email address already exists");
            }

            //TODO handle evaluation better
  //          if (item_in.Course != null && studyCourseDB.getCourseById(item_in.Course.ID) == null)
  //          {
  //              return BadRequest("No Studycourse found for " + item_in.Course.ID);
  //          }

            //TODO handle better evaluation
            //if (item_in.Type != null && userTypeDB.getByID(item_in.Type.ID) == null)
            //{
            //    return BadRequest("No UserType found for " + item_in.Type.ID);
            //}

            //update existing item
            ContactItem item_out = contactsDB.editContactItem(id, item_in);

            //return new item
            return Ok(item_out);
        }

        /// <summary>
        /// Deletes the ContactItem for the given ID. If the ID is not found, it returns NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult deleteContactItem(int id)
        {
            //TODO check for permission
            if (contactsDB.getContactItem(id)==null)
            {
                return NotFound(($"No ContactItem found for ID: {id}"));
            }
            contactsDB.deleteContactItem(id);
            return Ok();
        }

        /// <summary>
        /// Creates a ContactItem based on the given ContactItem. If the given ContactItem is null, it returns BadRequest.
        /// </summary>
        /// <param name="item_in"></param>
        /// <returns>ContactItem|BadRequest</returns>
        [HttpPost]
        public ActionResult<ContactItem> createContactItem(ContactItem item_in)
        {
            //TODO check for permission
            if (item_in == null)
            {
                return BadRequest("ContactItem not found");
            }

            //TODO handle evaluation better
            //if (item_in.Course != null && studyCourseDB.getCourseById(item_in.Course.ID) == null)
            //{
            //    return BadRequest("No Studycourse found for " + item_in.Course.ID);
            //}

            ////TODO handle better evaluation
            //if (item_in.Type != null && userTypeDB.getByID(item_in.Type.ID) == null)
            //{
            //    return BadRequest("No UserType found for " + item_in.Type.ID);
            //}

            ContactItem foundByEmail = contactsDB.getContactItem(item_in.Email);
            if (foundByEmail != null)
            {
                return BadRequest("ContactItem is already existing");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ContactItem item_out = contactsDB.createContactItem(item_in);
            return Created("", item_out);
        }

    }
}
