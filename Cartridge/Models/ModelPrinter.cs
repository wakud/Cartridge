using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cartridge.Models
{
    [Table("ModelPrinter")]
    public class ModelPrinter
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public ICollection<Cartridges> GetCartridges { get; set; }     //список картриджів які підходять до цієї моделі
        public ICollection<Punkt> Punkts { get; set; }         //список де стоять такі моделі принтерів
        public ModelPrinter()
        {
            Punkts = new List<Punkt>();
            GetCartridges = new List<Cartridges>();
        }
    }
}
