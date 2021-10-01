using Ectotec.Entities.Common.Response;
using Ectotec.Entities.Contact;
using EctoTec.Framework.Common.Interfaces;
using EctoTec.Framework.Common.Mailer;
using EctoTec.Framework.Common.Resources;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ectotec.BusinessLogic.Contact
{
    /// <summary>
    /// Capa de logica de negocio para modulo contactos
    /// </summary>
    public class ContactBusinessLogic : IEmailBusinessLogic<ContactModel, CommonResponseModel>
    {
        private readonly IEmailDataAccess<ContactModel, CommonResponseModel> _dataManager = null;
        private readonly ILogger<ContactBusinessLogic> _logger;
        private readonly IMailerManager<ContactModel> _mailerManager;
        public ContactBusinessLogic(IEmailDataAccess<ContactModel, CommonResponseModel> dataManager, ILogger<ContactBusinessLogic> logger, IMailerManager<ContactModel> mailerManager)
        {
            _logger = logger;
            _mailerManager = mailerManager;
            _dataManager = dataManager;
        }

        public async Task<IEnumerable<CommonResponseModel>> Countrys(ContactModel item)
        {
            IEnumerable<CommonResponseModel> response = new List<CommonResponseModel>();
            try
            {
                response = await Task.Run(() => _dataManager.Countrys(item));               
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message, error.Data);
            }
            return response;
        }

        public async Task<CommonResponseModel> Send(ContactModel item)
        {
            CommonResponseModel response = new CommonResponseModel() { Description = ContactResource.ContactErrorGeneric };
            try
            {
                response = await Task.Run(()=> _dataManager.Save(item));
                if(response?.Success == true)
                {
                    if(!_mailerManager.SendMail(item))
                    {
                        response.Description = ContactResource.ContactErrorSend;
                        return response;
                    }
                    response.Description = ContactResource.ContactOKSend;
                }
                else
                {
                    response = new CommonResponseModel();
                    response.Description = ContactResource.ContactErrorSave;
                }                
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message, error.Data);
            }
            return response;
        }
    }
}
