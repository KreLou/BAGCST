using BAGCST.api.User.Database;
using BAGCST.api.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace BAGCST.api.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeDB userTypeDB;

        public UserTypeController(IUserTypeDB userTypeDB)
        {
            this.userTypeDB = userTypeDB;
        }

        [HttpGet]
        public UserType[] getAll()
        {
            return userTypeDB.getAll();
        }
    }
}