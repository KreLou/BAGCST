using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IUserTypeDB
    {
        bool UserTypeExistsById(int id);

        UserType getByID(int id);
        UserType[] getAll();
    }
}
