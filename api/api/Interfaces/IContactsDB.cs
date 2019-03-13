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
        /// search for ContactItem by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>ContactItem|null</returns>
        ContactItem getContactItem(int id);

        /// <summary>
        /// search for ContactItem by E-Mail-Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>ContactItem|null</returns>
        ContactItem getContactItem(string email);

        /// <summary>
        /// get all ContactItems
        /// </summary>
        /// <returns>Array of ContactItems with length>=0</returns>
        ContactItem[] getAllContactItems();

        /// <summary>
        /// creates a ContactItem
        /// </summary>
        /// <param name="item">ContactItem</param>
        /// <returns>full ContactItem</returns>
        ContactItem createContactItem(ContactItem item);

        /// <summary>
        /// edit ContactItem
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
