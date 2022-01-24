using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autod.Core.Dtos
{
    public class AutoDto
    {
        public Guid? Id { get; set; }
        public string Mark { get; set; }
        public string Modell { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<IFormFile> Files { get; set; }

        public IEnumerable<ExistingFilePathDto> ExistingFilePaths { get; set; } = new List<ExistingFilePathDto>();
    }
}
