using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cartridge.Models
{
    public class Operations
    {
        public int Id { get; set; }
        public DateTime DateOperation { get; set; } //дата операції
        
        [ForeignKey("PrevPunktId")]
        public int? PrevPunktId { get; set; }
        public virtual Punkt PrevPunkt { get; set; }    //попередній пункт

        [ForeignKey("NextPunktId")]
        public int? NextPunktId { get; set; }
        public virtual Punkt NextPunkt { get; set; }    //наступний пункт
        
        [ForeignKey("CartridgeId")]
        public int? CartridgeId { get; set; }
        public virtual Cartridges Cartridge { get; set; }   //картридж

        [ForeignKey("OperationTypeId")]
        public int OperationTypeId { get; set; }
        public virtual OperationType Type { get; set; } // тип операції
    }
}
