using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Autod.Core.Dtos;
using Autod.Core.ServiceInterface;
using Autod.Data;
using Autod.Models.Files;
using Autod.Models.Auto;

namespace Autod.Controllers
{
    public class AutoController : Controller
    {
        private readonly AutodDbContext _context;
        private readonly IAutoService _autoService;
        private readonly IFileServices _fileService;

        public AutoController
            (
                AutodDbContext context,
                IAutoService autoService,
                IFileServices fileService
            )
        {
            _context = context;
            _autoService = autoService;
            _fileService = fileService;
        }


        public IActionResult Index()
        {
            var result = _context.Auto
                .Select(x => new AutoListViewModel
                {
                    Id = x.Id,
                    Mark = x.Mark,
                    Price = x.Price,
                    Ammount = x.Amount,
                    Modell = x.Modell
                });

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var auto = await _autoService.Delete(id);

            if (auto == null)
            {
                RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {
            AutoViewModel model = new AutoViewModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AutoViewModel model)
        {
            var dto = new AutoDto()
            {
                Id = model.Id,
                Modell = model.Modell,
                Mark = model.Mark,
                Amount = model.Amount,
                Price = model.Price,
                ModifiedAt = model.ModifiedAt,
                CreatedAt = model.CreatedAt,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths
                    .Select(x => new ExistingFilePathDto
                    {
                        PhotoId = x.PhotoId,
                        FilePath = x.FilePath,
                        AutoId = x.AutoId
                    }).ToArray()
            };

            var result = await _autoService.Add(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var auto = await _autoService.Edit(id);
            if (auto == null)
            {
                return NotFound();
            }

            var photos = await _context.ExistingFilePath
                .Where(x => x.AutoId == id)
                .Select(y => new ExistingFilePathViewModel
                {
                    FilePath = y.FilePath,
                    PhotoId = y.Id
                })
                .ToArrayAsync();


            var model = new AutoViewModel();

            model.Id = auto.Id;
            model.Modell = auto.Modell;
            model.Mark = auto.Mark;
            model.Amount = auto.Amount;
            model.Price = auto.Price;
            model.ModifiedAt = auto.ModifiedAt;
            model.CreatedAt = auto.CreatedAt;
            model.ExistingFilePaths.AddRange(photos);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(AutoViewModel model)
        {
            var dto = new AutoDto()
            {
                Id = model.Id,
                Modell = model.Modell,
                Mark = model.Mark,
                Amount = model.Amount,
                Price = model.Price,
                ModifiedAt = model.ModifiedAt,
                CreatedAt = model.CreatedAt,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths
                    .Select(x => new ExistingFilePathDto
                    {
                        PhotoId = x.PhotoId,
                        FilePath = x.FilePath,
                        AutoId = x.AutoId
                    }).ToArray()
            };

            var result = await _autoService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(ExistingFilePathViewModel model)
        {
            var dto = new ExistingFilePathDto()
            {
                FilePath = model.FilePath
            };

            var image = await _fileService.RemoveImage(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
