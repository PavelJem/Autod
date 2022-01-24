using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autod.Models.Auto
{
    public class AutoListViewModel
    {
        public Guid? Id { get; set; }
        public string Mark { get; set; }
        public string Modell { get; set; }
        public double Price { get; set; }
        public int Ammount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
