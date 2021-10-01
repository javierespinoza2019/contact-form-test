using Ectotec.BusinessLogic.Contact;
using Ectotec.DataAccess.Contact;
using Ectotec.Entities.Common.Mailer;
using Ectotec.Entities.Common.Response;
using Ectotec.Entities.Contact;
using EctoTec.Framework.Common.Interfaces;
using EctoTec.Framework.Common.Mailer;
using EctoTec.Framework.Providers.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ectotec.Contacts.Common
{
    public static class LoadServices
    {
        public static void LoadDependencies(this IServiceCollection services, IConfiguration Configuration)
        {            
            services.AddScoped<IEmailBusinessLogic<ContactModel, CommonResponseModel>, ContactBusinessLogic>();
            services.AddScoped<IEmailDataAccess<ContactModel, CommonResponseModel>, ContactDataAccess>();
            services.AddScoped<ISqlServerProvider, SqlServerProvider>();
            services.AddScoped<IMailerManager<ContactModel>, MailerManager>();
            services.Configure<MailConfigurationModel>(Configuration.GetSection("MailConfiguration"));
        }
    }
}
