using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using Autod.Core.Domain;
using Autod.Core.Dtos;
using Autod.Core.ServiceInterface;
using Autod.Data;
using System.Linq;

namespace Autod.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly AutodDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FileServices
            (
                AutodDbContext context,
                IWebHostEnvironment env
            )
        {
            _context = context;
            _env = env;
        }

        public async Task<ExistingFilePath> RemoveImage(ExistingFilePathDto dto)
        {
            var imageId = await _context.ExistingFilePath
                .FirstOrDefaultAsync(x => x.FilePath == dto.FilePath);

            string photoPath = _env.WebRootPath + "\\multipleFileUpload\\" + dto.FilePath;

            File.SetAttributes(photoPath, FileAttributes.Normal);
            File.Delete(photoPath);

            _context.ExistingFilePath.Remove(imageId);
            await _context.SaveChangesAsync();

            return imageId;
        }

        public async Task<ExistingFilePath> RemoveImages(ExistingFilePathDto[] dto)
        {
            foreach (var dtos in dto)
            {
                var fileId = await _context.ExistingFilePath
                    .FirstOrDefaultAsync(x => x.FilePath == dtos.FilePath);

                string photoPath = _env.WebRootPath + "\\multipleFileUpload\\" + dtos.FilePath;

                File.Delete(photoPath);

                _context.ExistingFilePath.Remove(fileId);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public string ProcessUploadedFile(AutoDto dto, Auto auto)
        {
            string uniqueFileName = null;

            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_env.WebRootPath + "\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_env.WebRootPath + "\\multipleFileUpload\\");
                }

                foreach (var photo in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "multipleFileUpload");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);

                        ExistingFilePath paths = new ExistingFilePath
                        {
                            Id = Guid.NewGuid(),
                            FilePath = uniqueFileName,
                            AutoId = auto.Id
                        };

                        _context.ExistingFilePath.Add(paths);
                    }
                }
            }

            return uniqueFileName;
        }



    }
}