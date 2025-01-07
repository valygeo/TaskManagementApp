using System.Data;
using System.Data.SqlClient;

namespace CommonServices
{
    public class CommonService : ICommonService
    {
        private string _connectionString;
         
        public CommonService()
        {
            _connectionString = "Server=.\\SQLEXPRESS;Database=TasksDatabase;Integrated Security=True";
            //_connectionString = "Data Source=EDI-DESKTOP-LT7;Initial Catalog=TaskManager;Integrated Security=True";
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
