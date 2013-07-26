using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkywriterServer.Helpers
{
    public class SQLiteHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool DatabaseExists(String databaseLocation)
        {
            return File.Exists(databaseLocation);
        }

        public static bool CreateDatabase(String databaseLocation)
        {
            return CreateDatabase(databaseLocation, false);
        }

        public static bool CreateDatabase(String databaseLocation, bool overwriteExisting)
        {
            // Check if the base directory for the database exists

            if (databaseLocation.Split(new char[] { '\\' }).Length > 2)
            {
                String baseDirectory = databaseLocation.Substring(0, databaseLocation.LastIndexOf('\\'));

                if (!Directory.Exists(baseDirectory))
                {
                    Directory.CreateDirectory(baseDirectory);
                }
            }

            // Check if the database already exists

            if (File.Exists(databaseLocation))
            {
                if (overwriteExisting)
                {
                    try
                    {
                        File.Delete(databaseLocation);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            // Create the new SQLiteDatabase

            try
            {
                SQLiteConnection.CreateFile(databaseLocation);
            }
            catch (Exception ex)
            {
                _log.Error("CreateDatabase()", ex);

                return false;
            }

            return true;
        }

        public static bool DeleteDatabase(String databaseLocation)
        {
            if (File.Exists(databaseLocation))
            {
                try
                {
                    File.Delete(databaseLocation);
                }
                catch (Exception ex)
                {
                    _log.Error("DeleteDatabase()", ex);

                    return false;
                }
            }

            return true;
        }

        public static DataTable GetDataTable(String databaseLocation, String sql)
        {
            DataTable dataTable = new DataTable();

            SQLiteConnection connection = null;
            SQLiteCommand mycommand = null;
            SQLiteDataReader reader = null;

            try
            {
                connection = new SQLiteConnection("Data Source=" + databaseLocation);
                connection.Open();

                mycommand = new SQLiteCommand(connection);
                mycommand.CommandText = sql;

                reader = mycommand.ExecuteReader();
                dataTable.Load(reader);
            }
            catch (Exception ex)
            {
                _log.Error("GetDataTable()", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (connection != null)
                {
                    connection.Close();
                }
            }

            return dataTable;
        }

        public static int WriteToDatabase(String databaseLocation, String sql)
        {
            SQLiteConnection connection = null;
            SQLiteCommand command = null;

            int rowsUpdated = 0;

            try
            {
                connection = new SQLiteConnection("Data Source=" + databaseLocation);
                connection.Open();

                command = new SQLiteCommand(connection);
                command.CommandText = sql;

                rowsUpdated = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _log.Error("WriteToDatabase()", ex);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return rowsUpdated;
        }
    }
}
