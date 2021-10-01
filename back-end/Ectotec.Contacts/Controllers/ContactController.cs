using Ectotec.Entities.Common.Response;
using Ectotec.Entities.Contact;
using EctoTec.Framework.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ectotec.Contacts.Controllers
{
    /// <summary>
    /// Controlador de servicios API (modulo contactos)
    /// </summary>
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
       
        private readonly IEmailBusinessLogic<ContactModel, CommonResponseModel> _logicManager = null;
        public ContactController(IEmailBusinessLogic<ContactModel, CommonResponseModel> logicManager)
        {
            _logicManager = logicManager;
        }

        [Route("Send")]
        [HttpPost]
        public async Task<object> Add(ContactModel item)
        {
            return await _logicManager.Send(item);
        }

        [Route("Countrys")]
        [HttpPost]
        public async Task<object> Countrys(ContactModel item)
        {
            return await _logicManager.Countrys(item);
        }
    }
}
