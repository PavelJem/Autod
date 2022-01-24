using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autod.Core.Domain
{
    public class ExistingFilePath
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public Guid? AutoId { get; set; }
    }
}
