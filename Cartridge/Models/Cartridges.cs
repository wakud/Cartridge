using System;
using System.Collections;
using System.Collections.Generic;
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
        public bool Status { get; set; }

        [ForeignKey("StanId")]
        public int StanId { get; set; }
        public virtual Stan GetStan { get; set; }

        public ICollection<Operations> Operation { get; set; }
        public Cartridges()
        {
            Operation = new List<Operations>();
        }

        [ForeignKey("PunktId")]
        public int? PunktId { get; set; }
        public virtual Punkt GetPunkt { get; set; } //в якому підрозділі картридж

        [ForeignKey("ModelCartridgeId")]
        public int ModelCartridgeId { get; set; }
        public virtual ModelCartridge GetModelCartridge { get; set; }   //модель картриджу
    }
}
