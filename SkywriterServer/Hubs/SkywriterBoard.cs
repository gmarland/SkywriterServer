using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SkywriterServer.Hubs
{
    [HubName("SkywriterBoard")]
    public class SkywriterBoard : Hub
    {
        private static Dictionary<String, List<String>> userConnections = new Dictionary<String, List<String>>();

        public override Task OnConnected()
        {
            String userId = Context.Request.QueryString["userId"];

            if (!userConnections.ContainsKey(userId))
            {
                userConnections[userId] = new List<String>();
            }

            userConnections[userId].Add(Context.ConnectionId);

            return base.OnConnected();
        }

        public void ReconnectClient(String userId)
        {
            if (!userConnections.ContainsKey(userId))
            {
                userConnections[userId] = new List<String>();
            }

            if (userConnections[userId].Where(c => c == Context.ConnectionId).Count() == 0)
            {
                userConnections[userId].Add(Context.ConnectionId);
            }
        }

        public void ClearSkywriterBoard(String userId)
        {
            if (userConnections.ContainsKey(userId))
            {
                for (int i = 0; i < userConnections[userId].Count; i++)
                {
                    if (userConnections[userId][i] != Context.ConnectionId)
                    {
                        Clients.Client(userConnections[userId][i]).ClearSkywriterBoard();
                    }
                }
            }
        }

        public void CopySkywriterItem(String userId, String text)
        {
            if (userConnections.ContainsKey(userId))
            {
                for (int i = 0; i < userConnections[userId].Count; i++)
                {
                    if (userConnections[userId][i] != Context.ConnectionId)
                    {
                        Clients.Client(userConnections[userId][i]).CopiedSkywriterItem(text);
                    }
                }
            }
        }

        public override Task OnDisconnected()
        {
            foreach (String key in userConnections.Keys)
            {
                Boolean connectionFound = false;

                for (int i = 0; i < userConnections[key].Count; i++)
                {
                    if (userConnections[key][i] == Context.ConnectionId)
                    {
                        connectionFound = true;
                        userConnections[key].RemoveAt(i);
                        break;
                    }
                }

                if (connectionFound)
                {
                    if (userConnections[key].Count == 0)
                    {
                        userConnections.Remove(key);
                    }
                    break;
                }
            }

            return base.OnDisconnected();
        }
    }
}