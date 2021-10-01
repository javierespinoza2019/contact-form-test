using Newtonsoft.Json;
using System.Data.SqlClient;

namespace EctoTec.Framework.Common.Utilities
{
    public static class SqlCommonUtilities
    {
        public static R ReadFromJson<R>(this SqlDataReader reader)
        {
            R response = default(R);
            if (reader == null) return default(R);

            string jsonResponse = string.Empty;
            while(reader.Read())
            {
                jsonResponse += reader.GetString(0);
            }
            if(!string.IsNullOrWhiteSpace(jsonResponse))
            {
                response = JsonConvert.DeserializeObject<R>(jsonResponse);
            }
            return response;
        }

    }
}
