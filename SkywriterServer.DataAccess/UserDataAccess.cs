using SkywriterServer.DataAccess.Interfaces;
using SkywriterServer.Helpers;
using SkywriterServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkywriterServer.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private String _databaseLocation;

        public UserDataAccess(String databaseLocation)
        {
            _databaseLocation = databaseLocation;
        }

        public Boolean CreateUserTable()
        {
            String createUserTableSQL = "CREATE TABLE ClipUser (_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
									    "externalId TEXT NOT NULL, " +
									    "name TEXT NOT NULL, " +
									    "password TEXT NOT NULL);";

            return (SQLiteHelper.WriteToDatabase(_databaseLocation, createUserTableSQL) > 0);
        }

        public ClipUser GetUserByNameAndPassword(String name, String password)
        {
            String getClipUserSQL = "SELECT ClipUser.externalId, ClipUser.name, ClipUser.password " +
                                    "FROM ClipUser " +
                                    "WHERE name LIKE '" + name + "' " +
                                    "AND password LIKE '" + password + "';";

            DataTable dataTable = SQLiteHelper.GetDataTable(_databaseLocation, getClipUserSQL);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow clipUserRow = dataTable.Rows[i];

                ClipUser clipUser = new ClipUser();
                clipUser.Id = clipUserRow["externalId"].ToString();
                clipUser.Name = clipUserRow["name"].ToString();
                clipUser.Password = clipUserRow["password"].ToString();

                return clipUser;
            }

            return null;
        }

        public ClipUser GetUserById(String id)
        {
            String getClipUserSQL = "SELECT ClipUser.externalId, ClipUser.name, ClipUser.password " +
                                    "FROM ClipUser " + 
                                    "WHERE externalId = '" + id + "';";

            DataTable dataTable = SQLiteHelper.GetDataTable(_databaseLocation, getClipUserSQL);

            if (dataTable.Rows.Count == 1)
            {
                DataRow clipUserRow = dataTable.Rows[0];

                ClipUser clipUser = new ClipUser();
                clipUser.Id = clipUserRow["externalId"].ToString();
                clipUser.Name = clipUserRow["name"].ToString();
                clipUser.Password = clipUserRow["password"].ToString();

                return clipUser;
            }

            return null;
        }

        public ClipUser GetUserByName(String name)
        {
            String getClipUserSQL = "SELECT ClipUser.externalId, ClipUser.name, ClipUser.password " +
                                    "FROM ClipUser " +
                                    "WHERE name = '" + name + "';";

            DataTable dataTable = SQLiteHelper.GetDataTable(_databaseLocation, getClipUserSQL);

            if (dataTable.Rows.Count == 1)
            {
                DataRow clipUserRow = dataTable.Rows[0];

                ClipUser clipUser = new ClipUser();
                clipUser.Id = clipUserRow["externalId"].ToString();
                clipUser.Name = clipUserRow["name"].ToString();
                clipUser.Password = clipUserRow["password"].ToString();

                return clipUser;
            }

            return null;
        }

        public Boolean InsertUser(String id, String name, String password)
        {
            String insertClipUserSQL = "INSERT INTO ClipUser (externalId, name, password) " +
                                        "VALUES ('" + id + "', '" + name + "', '" + password + "');";

            return (SQLiteHelper.WriteToDatabase(_databaseLocation, insertClipUserSQL) > 0);
        }
    }
}
