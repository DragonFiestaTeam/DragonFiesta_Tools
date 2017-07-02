using MySql.Data.MySqlClient;

namespace SHNtoMySQLConverter.Connection
{
    public class DatabaseHelper
    {
        #region .ctor

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        ~DatabaseHelper()
        {
        }

        #endregion .ctor

        #region Methods

        public MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        #endregion Methods



        #region Variables

        private string connectionString;

        #endregion Variables
    }
}