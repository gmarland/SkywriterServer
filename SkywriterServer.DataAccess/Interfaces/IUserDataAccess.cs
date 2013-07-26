using SkywriterServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkywriterServer.DataAccess.Interfaces
{
    public interface IUserDataAccess
    {
        Boolean CreateUserTable();

        ClipUser GetUserByNameAndPassword(String name, String password);

        ClipUser GetUserById(String id);

        ClipUser GetUserByName(String email);

        Boolean InsertUser(String id, String name, String password);
    }
}
