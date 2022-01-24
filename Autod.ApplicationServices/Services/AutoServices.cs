using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Autod.Core.Domain;
using Autod.Core.Dtos;
using Autod.Core.ServiceInterface;
using Autod.Data;
using System.Linq;

namespace Autod.ApplicationServices.Services
{
    public class AutoServices : IAutoService
    {
        private readonly AutodDbContext _context;
        private readonly IFileServices _file;

        public AutoServices
            (
                AutodDbContext context,
                IFileServices file
            )
        {
            _context = context;
            _file = file;
        }

        public async Task<Auto> Delete(Guid id)
        {
            var photos = await _context.ExistingFilePath
                .Where(x => x.AutoId == id)
                .Select(y => new ExistingFilePathDto
                {
                    AutoId = y.AutoId,
                    FilePath = y.FilePath,
                    PhotoId = y.Id
                })
                .ToArrayAsync();


            var autoId = await _context.Auto
                .Include(x => x.ExistingFilePaths)
                .FirstOrDefaultAsync(x => x.Id == id);

            await _file.RemoveImages(photos);
            _context.Auto.Remove(autoId);
            await _context.SaveChangesAsync();

            return autoId;
        }

        public async Task<Auto> Add(AutoDto dto)
        {
            Auto auto = new Auto();

            auto.Id = Guid.NewGuid();
            auto.Modell = dto.Modell;
            auto.Mark = dto.Mark;
            auto.Amount = dto.Amount;
            auto.Price = dto.Price;
            auto.ModifiedAt = DateTime.Now;
            auto.CreatedAt = DateTime.Now;
            _file.ProcessUploadedFile(dto, auto);

            await _context.Auto.AddAsync(auto);
            await _context.SaveChangesAsync();

            return auto;
        }


        public async Task<Auto> Edit(Guid id)
        {
            var result = await _context.Auto
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Auto> Update(AutoDto dto)
        {
            Auto auto = new Auto();

            auto.Id = dto.Id;
            auto.Modell = dto.Modell;
            auto.Mark = dto.Mark;
            auto.Amount = dto.Amount;
            auto.Price = dto.Price;
            auto.ModifiedAt = dto.ModifiedAt;
            auto.CreatedAt = dto.CreatedAt;
            _file.ProcessUploadedFile(dto, auto);

            _context.Auto.Update(auto);
            await _context.SaveChangesAsync();

            return auto;
        }
    }
}