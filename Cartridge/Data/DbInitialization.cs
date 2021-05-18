using Cartridge.Models;
using System;
using System.Linq;

namespace Cartridge.Data
{
    public class DbInitialization
    {
        public static void Initial(MainContext context)
        {
            if (!context.Punkts.Any())
            {
                Punkt tr27 = new Punkt { Name = "Бережанський ЦОК" };
                Punkt tr28 = new Punkt { Name = "Борщівський ЦОК" };
                Punkt tr29 = new Punkt { Name = "Бучацький ЦОК" };
                //Punkt tr30 = new Punkt { Name = "Гусятинський ЦОК" };
                //Punkt tr31 = new Punkt { Name = "Заліщицький ЦОК" };
                //Punkt tr32 = new Punkt { Name = "Збаразький ЦОК" };
                //Punkt tr33 = new Punkt { Name = "Зборівський ЦОК" };
                //Punkt tr34 = new Punkt { Name = "Козівський ЦОК" };
                //Punkt tr35 = new Punkt { Name = "Кременецький ЦОК" };
                //Punkt tr36 = new Punkt { Name = "Лановецький ЦОК" };
                //Punkt tr37 = new Punkt { Name = "Монастириський ЦОК" };
                //Punkt tr38 = new Punkt { Name = "Підволочиський ЦОК" };
                //Punkt tr39 = new Punkt { Name = "Тернопільський ЦОК" };
                //Punkt tr41 = new Punkt { Name = "Теребовлянський ЦОК" };
                //Punkt tr42 = new Punkt { Name = "Чортківський ЦОК" };
                //Punkt tr43 = new Punkt { Name = "Шумський ЦОК" };
                //Punkt tr44 = new Punkt { Name = "Підгаєцький ЦОК" };
                Punkt sklad = new Punkt { Name = "Склад" };
                Punkt zapravka = new Punkt { Name = "Хардсофт (обслуговування)" };
                context.AddRange(tr27, tr28, tr29, sklad, zapravka);
                context.SaveChanges();
            }
        }
    }
}
