using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cartridge.Models
{
    public class OperationType
    {
        public int Id { get; set; }
        public string Name { get; set; }        //назва операції
        public bool FillDefCheck { get; set; }  //пустий чи заправлений

        [ForeignKey("PunktId")]
        public int? PunktId { get; set; }
        public virtual Punkt GetPunkt { get; set; } //в якому підрозділі картридж

        [ForeignKey("StanId")]
        public int StanId { get; set; }
        public virtual Stan GetStan { get; set; }   //стан картриджу

        public ICollection<Operations> Operation { get; set; }
        public OperationType()
        {
            Operation = new List<Operations>();     //список операцій по картриджу
        }
    }
}
