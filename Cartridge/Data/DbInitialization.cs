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
                Punkt sklad = new() { Name = "Склад" };
                context.Add(sklad);
                context.SaveChanges();
                Punkt zapravka = new() { Name = "обслуговування" };
                context.Add(zapravka);
                context.SaveChanges();
            }

            if (!context.Stans.Any())
            {
                Stan eks = new() { Name = "В роботі" };
                context.Add(eks);
                context.SaveChanges();
                Stan serv = new() { Name = "На заправці" };
                context.Add(serv);
                context.SaveChanges();
                Stan rem = new(){ Name = "На ремонті" };
                context.Add(rem);
                context.SaveChanges();
                Stan skl = new() { Name = "На складі" };
                context.Add(skl);
                context.SaveChanges();
            }

            if (!context.OperationTypes.Any())
            {
                OperationType rec1 = new() { Name = "Прийняти картридж", PunktId = 1, FillDefCheck = false, StanId = 4 };
                context.Add(rec1);
                context.SaveChanges();
                OperationType rec2 = new() { Name = "Видати картридж", FillDefCheck = true, StanId = 1 };
                context.Add(rec2);
                context.SaveChanges();
                OperationType rec3 = new() { Name = "Передати на обслуговування", PunktId = 2, FillDefCheck = false, StanId = 2 };
                context.Add(rec3);
                context.SaveChanges();
                OperationType rec4 = new() { Name = "Прийняти з обслуговування", PunktId = 1, FillDefCheck = true, StanId = 4 };
                context.Add(rec4);
                context.SaveChanges();
                OperationType rec5 = new() { Name = "Повернення", PunktId = 2, FillDefCheck = true, StanId = 3 };
                context.Add(rec5);
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                User adm = new() { Login = "admin", 
                                   Password = "123", 
                                   Name = "Administrator", 
                                   IsAdmin = "1"
                };
                context.Add(adm);
                context.SaveChanges();

                User user = new() { Login = "user", 
                                    Password = "1", 
                                    Name = "User", 
                                    IsAdmin = "0"
                };
                context.Add(user);
                context.SaveChanges();
            }

            if (!context.Organizations.Any())
            {
                Organization organization = new()
                {
                    Name = "Назва організації",
                    Address = "Адреса організації",
                    Buh = "Бухгалтер",
                    Nach = "Начальник",
                    EDRPOU = "ЄДРПОУ",
                    IPN = "ІПН",
                    MFO = "МФО",
                    RozRah = "Розрахунковий рахунок",
                    NumberDoc = 1
                };
                context.Add(organization);
                context.SaveChanges();
            }
        }
    }
}
