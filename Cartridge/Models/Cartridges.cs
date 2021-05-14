using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cartridge.Models
{
    [Table("Cartridge")]
    public class Cartridges
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }   //штрих-код
        public DateTime DateInsert { get; set; }    //дата введення в експлуатацію
        public DateTime? DateDel { get; set; }      //дата виведення з експлуатації

        [ForeignKey("PunktId")]
        public int? PunktId { get; set; }
        public virtual Punkt GetPunkt { get; set; } //де зараз картридж
    }
}
