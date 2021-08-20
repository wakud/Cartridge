using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cartridge.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }        //назва 
        public string Address { get; set; }     //повна адреса
        public string Nach { get; set; }        //начальник
        public string Buh { get; set; }         //бухгалтер
        public string EDRPOU { get; set; }      //ЄДРПОУ
        public string IPN { get; set; }         //ІПН
        public string MFO { get; set; }         //МФО
        public string RozRah { get; set; }      //р/р
        public int NumberDoc { get; set; }     //№ документу
    }
}
