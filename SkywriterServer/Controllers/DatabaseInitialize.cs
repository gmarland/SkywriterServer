using SkywriterServer.Helpers;
using SkywriterServer.Service;
using SkywriterServer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SkywriterServer.Controllers
{
    public class DatabaseInitialize : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!SQLiteHelper.DatabaseExists(Properties.Settings.Default.DatabaseLocation))
            {
                SQLiteHelper.CreateDatabase(Properties.Settings.Default.DatabaseLocation);

                IUserService userService = new UserService(Properties.Settings.Default.DatabaseLocation);
                userService.Initialize();
            }
        }
    }
}