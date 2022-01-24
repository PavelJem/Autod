using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autod.Core.Domain
{
    public class Auto
    {
        public Guid? Id { get; set; }
        public string Mark { get; set; }
        public string Modell { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public IEnumerable<ExistingFilePath> ExistingFilePaths { get; set; } = new List<ExistingFilePath>();
    }
}
