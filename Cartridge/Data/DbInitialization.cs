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
                Punkt nul = new() { Name = "Невідомо" };
                Punkt sklad = new() { Name = "Центральний офіс" };
                Punkt zapravka = new() { Name = "Хардсофт (обслуговування)" };
                Punkt tr27 = new() { Name = "Бережанський ЦОК" };
                Punkt tr28 = new() { Name = "Борщівський ЦОК" };
                Punkt tr29 = new() { Name = "Бучацький ЦОК" };
                Punkt tr30 = new() { Name = "Гусятинський ЦОК" };
                Punkt tr31 = new() { Name = "Заліщицький ЦОК" };
                Punkt tr32 = new() { Name = "Збаразький ЦОК" };
                Punkt tr33 = new() { Name = "Зборівський ЦОК" };
                Punkt tr34 = new() { Name = "Козівський ЦОК" };
                Punkt tr35 = new() { Name = "Кременецький ЦОК" };
                Punkt tr36 = new() { Name = "Лановецький ЦОК" };
                Punkt tr37 = new() { Name = "Монастириський ЦОК" };
                Punkt tr38 = new() { Name = "Підволочиський ЦОК" };
                Punkt tr39 = new() { Name = "Тернопільський ЦОК" };
                Punkt tr41 = new() { Name = "Теребовлянський ЦОК" };
                Punkt tr42 = new() { Name = "Чортківський ЦОК" };
                Punkt tr43 = new() { Name = "Шумський ЦОК" };
                Punkt tr44 = new() { Name = "Підгаєцький ЦОК" };
                context.AddRange(/*nul,*/ sklad, zapravka, tr27, tr28, tr29, tr30, tr31, tr32, tr33, tr34, tr35, tr36, tr37, tr38, tr39, tr41,
                     tr42, tr43, tr44);
                context.SaveChanges();
            }

            if (!context.Stans.Any())
            {
                Stan eks = new() { Name = "В роботі" };
                Stan serv = new() { Name = "На заправці" };
                Stan rem = new(){ Name = "На ремонті" };
                Stan skl = new() { Name = "На складі" };
                context.AddRange(eks, serv, rem, skl);
                context.SaveChanges();
            }

            if (!context.OperationTypes.Any())
            {
                OperationType rec = new() { Name = "Прийнято з обслуговування", PunktId = 1, FillDefCheck = true, StanId = 4 };
                context.AddRange(rec);
                context.SaveChanges();
            }
        }
    }
}
