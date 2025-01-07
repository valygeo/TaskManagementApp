using System.Data;

namespace CommonServices
{
    public interface ICommonService
    {
        public IDbConnection CreateConnection();
    }
}
