using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IContactsDB
    {


        /// <summary>
        /// search for ContactItem by E-Mail-Address
        /// Get ContactItem by E-Mail-Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>ContactItem|null</returns>
        ContactItem getContactItem(string email);

        /// <summary>
        /// get all ContactItems
        /// Order by:
        /// 1. ContactItem.Lastname asc
        /// 2. ContactItem.Firstname asc
        /// </summary>
        /// <returns>Array of ContactItems with length>=0</returns>
        ContactItem[] getAllContactItems();

        /// <summary>
        /// creates a ContactItem
        /// Create ContactItem
        /// </summary>
        /// <param name="item">ContactItem</param>
        /// <returns>full ContactItem</returns>
        ContactItem createContactItem(ContactItem item);

        /// <summary>
        /// edit ContactItem
        /// Edit ContactItem by ID and ContactItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>edited ContactItem|null</returns>
        ContactItem editContactItem(int id, ContactItem item);

        /// <summary>
        /// delete ContactItem
        /// </summary>
        /// <param name="id">ID</param>
        void deleteContactItem(int id);
        
    }
}
