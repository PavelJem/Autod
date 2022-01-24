using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Autod.Models.Files;

namespace Autod.Models.Auto
{
    public class AutoViewModel
    {
        public Guid? Id { get; set; }
        public string Mark { get; set; }
        public string Modell { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<IFormFile> Files { get; set; }

        public List<ExistingFilePathViewModel> ExistingFilePaths { get; set; } = new List<ExistingFilePathViewModel>();
    }
}
