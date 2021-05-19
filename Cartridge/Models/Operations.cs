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
        public DateTime DateOperation { get; set; }
        
        [ForeignKey("PrevPlaceId")]
        public int PrevPlaceId { get; set; }
        public virtual Punkt PrevPunkt { get; set; }

        [ForeignKey("NextPlaceId")]
        public int NextPlaceId { get; set; }
        public virtual Punkt NextPunkt { get; set; }
        
        [ForeignKey("CatridgeId")]
        public int CatridgeId { get; set; }
        public virtual Cartridges Cartridge { get; set; }

        [ForeignKey("OperationTypeId")]
        public int OperationTypeId { get; set; }
        public virtual OperationType Type { get; set; }
    }
}
