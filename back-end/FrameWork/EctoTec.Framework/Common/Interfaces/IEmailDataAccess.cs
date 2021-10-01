using System.Collections.Generic;
using System.Threading.Tasks;

namespace EctoTec.Framework.Common.Interfaces
{
    /// <summary>
    /// Interface acciones de acceso a datos
    /// </summary>
    /// <typeparam name="I">Objeto de datos de entrada</typeparam>
    /// <typeparam name="R">Objeto de datos de salida</typeparam>
    public interface IEmailDataAccess<I,R>
    {
        /// <summary>
        /// Guarda datos a la base
        /// </summary>
        /// <param name="item">Objeto de datos de entrada</param>
        /// <returns>Objeto de datos de salida</returns>
        R Save(I item);
        /// <summary>
        /// Obtiene datos de estados y ciudades
        /// </summary>
        /// <param name="item">Objeto de datos de entrada</param>
        /// <returns>Objeto de datos de salida</returns>
        IEnumerable<R> Countrys(I item);
    }
}
