using System;
using System.Threading.Tasks;
using Autod.Core.Domain;
using Autod.Core.Dtos;

namespace Autod.Core.ServiceInterface
{
    public interface IAutoService : IApplicationService
    {
        Task<Auto> Delete(Guid id);

        Task<Auto> Add(AutoDto dto);

        Task<Auto> Edit(Guid id);

        Task<Auto> Update(AutoDto dto);
    }
}
