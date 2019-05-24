using BAGCST.api.User.Models;

namespace BAGCST.api.User.Database
{
    public interface IUserTypeDB
    {
        bool UserTypeExistsById(int id);

        UserType getByID(int id);
        UserType[] getAll();
    }
}
