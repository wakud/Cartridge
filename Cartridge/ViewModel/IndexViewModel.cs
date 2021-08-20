using System.Collections.Generic;
using Cartridge.Models;

namespace Cartridge.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Cartridges> cartridges { get; set; }
        public IEnumerable<Punkt> punkts { get; set; }
        public OperationType operationsType { get; set; }
        public int? selectedLocationId { get; set; }
        public int? newPunkt { get; set; }
    }
}
