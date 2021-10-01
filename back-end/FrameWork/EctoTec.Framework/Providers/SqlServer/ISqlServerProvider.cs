using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EctoTec.Framework.Providers.SqlServer
{
    public interface ISqlServerProvider : IDisposable
    {
        SqlConnection NewConnection();
        SqlDataReader ExecuteStoreProcedure(string spName, List<SqlParameter> parameters);        
    }
}
