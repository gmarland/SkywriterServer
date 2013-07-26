using SkywriterServer.DataAccess;
using SkywriterServer.DataAccess.Interfaces;
using SkywriterServer.Models;
using SkywriterServer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkywriterServer.Service
{
    public class UserService : IUserService
    {
        private IUserDataAccess _userDataAccess;

        public UserService(String databaseLocation)
        {
            _userDataAccess = new UserDataAccess(databaseLocation);
        }

        public void Initialize()
        {
            _userDataAccess.CreateUserTable();
        }

        public ClipUser GetClipUser(String name, String password)
        {
            return _userDataAccess.GetUserByNameAndPassword(name, password);
        }

        public ClipUser GetClipUser(String id)
        {
            return _userDataAccess.GetUserById(id);
        }

        public Boolean InsertClipUser(String id, String name, String password)
        {
            if (_userDataAccess.GetUserByName(name) == null)
            {
                return _userDataAccess.InsertUser(id, name, password);
            }
            else 
            {
                return false;
            }
        }
    }
}
