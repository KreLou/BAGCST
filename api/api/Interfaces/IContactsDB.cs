using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    interface IContactsDB
    {
        /// <summary>
        /// Get ContactItem by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>ContactItem|null</returns>
        ContactItem getContactItem(int id);

        /// <summary>
        /// Get ContactItem by E-Mail-Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>ContactItem|null</returns>
        ContactItem getContactItem(string email);

        /// <summary>
        /// Get all ContactItems
        /// Order by:
        /// 1. ContactItem.Lastname asc
        /// 2. ContactItem.Firstname asc
        /// </summary>
        /// <returns>Array of ContactItems with length>=0</returns>
        ContactItem[] getAllContactItems();

        /// <summary>
        /// Create ContactItem
        /// </summary>
        /// <param name="item">ContactItem</param>
        /// <returns>full ContactItem</returns>
        ContactItem createContactItem(ContactItem item);

        /// <summary>
        /// Edit ContactItem by ID and ContactItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>edited ContactItem|null</returns>
        ContactItem editContactItem(int id, ContactItem item);

        /// <summary>
        /// Delete ContactItem by ID
        /// </summary>
        /// <param name="id">ID</param>
        void deleteContactItem(int id);

        
    }
}
