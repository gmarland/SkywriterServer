using SkywriterServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkywriterServer.Service.Interfaces
{
    public interface IUserService
    {
        void Initialize();

        ClipUser GetClipUser(String name, String password);

        ClipUser GetClipUser(String id);

        Boolean InsertClipUser(String id, String name, String password);
    }
}
