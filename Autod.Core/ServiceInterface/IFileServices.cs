using System.Threading.Tasks;
using Autod.Core.Domain;
using Autod.Core.Dtos;

namespace Autod.Core.ServiceInterface
{
    public interface IFileServices : IApplicationService
    {
        string ProcessUploadedFile(AutoDto dto, Auto auto);
        Task<ExistingFilePath> RemoveImage(ExistingFilePathDto dto);
        Task<ExistingFilePath> RemoveImages(ExistingFilePathDto[] dto);
    }
}
