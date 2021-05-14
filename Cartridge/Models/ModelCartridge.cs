using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cartridge.Models
{
    [Table("ModelCartridge")]
    public class ModelCartridge
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }    //назва моделі картриджу
        public string Photo { get; set; }   //фото моделі
        public string Description { get; set; }     //опис
        public bool Baraban { get; set; }       //чи це є барабан
        public List<ModelPrinter> Printers { get; set; }        //список принтерів до яких підходить картридж
        public List<Cartridges> Cartridges { get; set; }        //список картриджів які відносаться до цієї моделі
        public ModelCartridge()
        {
            Printers = new List<ModelPrinter>();
            Cartridges = new List<Cartridges>();
        }
    }
}
