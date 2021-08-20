using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cartridge.Models;

namespace Cartridge.ViewModel
{
    public class AktModel
    {
        public IEnumerable<Operations> Operations { get; set; }
        public IEnumerable<Punkt> Punkts { get; set; }
        public IEnumerable<Cartridges> Cartridges { get; set; }
        public IEnumerable<OperationType> OperationType { get; set; }
        public OperationType typeOperation { get; set; }
        public string prevPunkt { get; set; }
        public string newPunkt { get; set; }
    }
}
