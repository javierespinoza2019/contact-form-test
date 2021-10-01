using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EctoTec.Framework.Providers.SqlServer
{
    public class SqlServerProvider : ISqlServerProvider
    {
        private SqlConnection connection = null;
        private SqlCommand cmd = null;
        private SqlDataReader reader = null;
        private ILogger<SqlServerProvider> _logger = null;
        private readonly string STRING_CONNECTION = string.Empty;
        public SqlServerProvider(ILogger<SqlServerProvider> logger, IConfiguration config)
        {
            _logger = logger;
            STRING_CONNECTION = config.GetConnectionString("SQLMXConnection");
        }

        public void Dispose()
        {
            if(connection != null )
            {
                if(connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
            if (cmd != null)
            {               
                cmd.Dispose();
            }
            if(reader != null)
            {
                reader.Close();
            }
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        public SqlConnection NewConnection()
        {
            try
            {
                if (connection == null) connection = new SqlConnection(STRING_CONNECTION);

                connection.Open();
            }
            catch (Exception error)
            {
                _logger.LogError(error,error.Message, error.Data);
            }
            return connection;
        }       
        public SqlDataReader ExecuteStoreProcedure(string spName, List<SqlParameter> parameters)
        {
            try
            {
                if (cmd == null) cmd = new SqlCommand(spName, connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters.ToArray());
                reader = cmd.ExecuteReader();
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message, error.Data);
            }
            return reader;
        }
    }
}
