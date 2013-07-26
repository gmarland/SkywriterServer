using SkywriterServer.Models;
using SkywriterServer.Service;
using SkywriterServer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SkywriterServer.Controllers
{
    public class UsersController : ApiController
    {
        [DatabaseInitialize]
        public ClipUser Get(String id)
        {
            IUserService userService = new UserService(Properties.Settings.Default.DatabaseLocation);

            return userService.GetClipUser(id);
        }

        [DatabaseInitialize]
        public ClipUser Get(String name, String password)
        {
            IUserService userService = new UserService(Properties.Settings.Default.DatabaseLocation);

            return userService.GetClipUser(name, password);
        }

        [DatabaseInitialize]
        public String Post([FromBody]ClipUser clipUser)
        {
            IUserService userService = new UserService(Properties.Settings.Default.DatabaseLocation);

            String newUserId = System.Guid.NewGuid().ToString();

            if (userService.InsertClipUser(newUserId, clipUser.Name, clipUser.Password))
            {
                return newUserId;
            }
            else
            {
                return "";
            }
        }
    }
}
