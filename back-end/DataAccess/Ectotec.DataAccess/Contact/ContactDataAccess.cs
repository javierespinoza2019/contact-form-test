using Ectotec.Entities.Common.Response;
using Ectotec.Entities.Contact;
using Ectotec.Entities.Enums;
using EctoTec.Framework.Common.Interfaces;
using EctoTec.Framework.Common.Utilities;
using EctoTec.Framework.Providers.SqlServer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Ectotec.DataAccess.Contact
{
    /// <summary>
    /// Capa de acceso a datos para modulo contactos
    /// </summary>
    public class ContactDataAccess : IEmailDataAccess<ContactModel, CommonResponseModel>
    {
        private readonly ISqlServerProvider _sqlProvider = null;
        private const string SP_NAME = "USP_CRUDContact";
        public ContactDataAccess(ISqlServerProvider sqlProvider)
        {
            _sqlProvider = sqlProvider;
        }
        public CommonResponseModel Save(ContactModel item)
        {
            CommonResponseModel response = new CommonResponseModel();
            using (var conn = _sqlProvider.NewConnection())
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", item.Id));
                parameters.Add(new SqlParameter("@FullName", item.FullName));
                parameters.Add(new SqlParameter("@Email", item.Email));
                parameters.Add(new SqlParameter("@PhoneNumber", item.PhoneNumber));
                parameters.Add(new SqlParameter("@ContactDate", item.ContactDate));
                parameters.Add(new SqlParameter("@Country", item.Country));
                parameters.Add(new SqlParameter("@Option", ActionTypeEnum.Add));
                parameters.Add(new SqlParameter("@PageNumber", 0));
                parameters.Add(new SqlParameter("@RecordsPerPage", 0));
                parameters.Add(new SqlParameter("@ReturnAll", false));
                using (var cmd = _sqlProvider.ExecuteStoreProcedure(SP_NAME, parameters))
                {
                    response = cmd.ReadFromJson<CommonResponseModel>();
                }
            }
            return response;
        }

        public IEnumerable<CommonResponseModel> Countrys(ContactModel item)
        {
            List<CommonResponseModel> response = new List<CommonResponseModel>();
            using (var conn = _sqlProvider.NewConnection())
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", item.Id));
                parameters.Add(new SqlParameter("@FullName", item.FullName));
                parameters.Add(new SqlParameter("@Email", item.Email));
                parameters.Add(new SqlParameter("@PhoneNumber", item.PhoneNumber));
                parameters.Add(new SqlParameter("@ContactDate", item.ContactDate));
                parameters.Add(new SqlParameter("@Country", item.Country));
                parameters.Add(new SqlParameter("@Option", ActionTypeEnum.GetList));
                parameters.Add(new SqlParameter("@PageNumber", 0));
                parameters.Add(new SqlParameter("@RecordsPerPage", 0));
                parameters.Add(new SqlParameter("@ReturnAll", true));
                using (var cmd = _sqlProvider.ExecuteStoreProcedure(SP_NAME, parameters))
                {
                    response = cmd.ReadFromJson<List<CommonResponseModel>>();
                }
            }
            return response;
        }
    }
}
