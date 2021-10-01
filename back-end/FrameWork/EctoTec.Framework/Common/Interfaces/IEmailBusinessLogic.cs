using System.Collections.Generic;
using System.Threading.Tasks;

namespace EctoTec.Framework.Common.Interfaces
{
    public interface IEmailBusinessLogic<I,R>
    {
        Task<R> Send(I item);
        Task<IEnumerable<R>> Countrys(I item);
    }
}
