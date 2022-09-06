using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMVD
{
    public static class DataBaseSqlCommands
    {
        public static SqlConnection _sqlConnection;

        public static SqlConnection sqlConnection => _sqlConnection ?? (_sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseMVD"].ConnectionString));

        public static string localUser;

        #region Методы класса
        public static SqlConnection CheckSqlConnectionOn()
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
            return sqlConnection;
        } //SqlConnection Close

        public static SqlConnection CheckSqlConnectionOff()
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            return sqlConnection;
        } //SqlConnection Open

        public static DataTable SelectSqlCommands(SqlCommand sqlCommand) 
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            CheckSqlConnectionOff();
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
            CheckSqlConnectionOn();
            return table;
        } //Select sql command, return table

        public static void UpdInsDelSqlCommands(SqlCommand sqlCommand)
        {
            CheckSqlConnectionOff();
            sqlCommand.ExecuteNonQuery();
            CheckSqlConnectionOn();
        } //Update, insert, delete sql commands
        #endregion
    }
}
