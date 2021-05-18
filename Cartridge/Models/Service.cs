using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cartridge.Models
{
    [Table("Service")]
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateInput { get; set; }
        public DateTime DateOut { get; set; }
        public ICollection<Cartridges> GetCartridges { get; set; }
        public Service()
        {
            GetCartridges = new List<Cartridges>();
        }
    }

}
