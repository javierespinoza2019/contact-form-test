using System;
using System.Data.SqlTypes;

namespace Ectotec.Entities.Contact
{
    /// <summary>
    /// Modelo de datos para formulario contacto
    /// </summary>
    public class ContactModel
    {
        /// <summary>
        /// Identiificador de datos de contacto
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Nombre completo de usuario
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Correo electronico
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Numero de telefono
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Fecha de contacto
        /// </summary>
        public DateTime ContactDate { get; set; } = (DateTime)SqlDateTime.MinValue;
        /// <summary>
        /// Ciudad y estado de donde se contacta
        /// </summary>
        public string Country { get; set; }
    }
}
