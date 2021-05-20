using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cartridge.Models
{
    [Table("Punkt")]
    public class Punkt
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelPrinter> Printers { get; set; }
        public ICollection<Cartridges> Cartridges { get; set; }
        public Punkt()
        {
            Printers = new List<ModelPrinter>();
            Cartridges = new List<Cartridges>();
        }
    }
}
