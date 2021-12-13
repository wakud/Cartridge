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
                Punkt sklad = new Punkt() { Name = "Склад" };
                context.Add(sklad);
                context.SaveChanges();
                Punkt zapravka = new Punkt() { Name = "обслуговування" };
                context.Add(zapravka);
                context.SaveChanges();
            }

            if (!context.Stans.Any())
            {
                Stan eks = new Stan() { Name = "В роботі" };
                context.Add(eks);
                context.SaveChanges();
                Stan serv = new Stan() { Name = "На заправці" };
                context.Add(serv);
                context.SaveChanges();
                Stan rem = new Stan(){ Name = "На ремонті" };
                context.Add(rem);
                context.SaveChanges();
                Stan skl = new Stan() { Name = "На складі" };
                context.Add(skl);
                context.SaveChanges();
            }

            if (!context.OperationTypes.Any())
            {
                OperationType rec1 = new OperationType() { Name = "Прийняти картридж", PunktId = 1, FillDefCheck = false, StanId = 4 };
                context.Add(rec1);
                context.SaveChanges();
                OperationType rec2 = new OperationType() { Name = "Видати картридж", FillDefCheck = true, StanId = 1 };
                context.Add(rec2);
                context.SaveChanges();
                OperationType rec3 = new OperationType() { Name = "Передати на обслуговування", PunktId = 2, FillDefCheck = false, StanId = 2 };
                context.Add(rec3);
                context.SaveChanges();
                OperationType rec4 = new OperationType() { Name = "Прийняти з обслуговування", PunktId = 1, FillDefCheck = true, StanId = 4 };
                context.Add(rec4);
                context.SaveChanges();
                OperationType rec5 = new OperationType() { Name = "Повернення", PunktId = 2, FillDefCheck = true, StanId = 3 };
                context.Add(rec5);
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                User adm = new User() { Login = "admin", 
                                   Password = "zxpk3j6CfFjNP0OvVff2yw==", 
                                   Name = "Administrator", 
                                   IsAdmin = "1"
                };
                context.Add(adm);
                context.SaveChanges();

                User user = new User() { Login = "user", 
                                    Password = "BGBUx4nqgwIWY/sWdqYXVQ==", 
                                    Name = "User", 
                                    IsAdmin = "0"
                };
                context.Add(user);
                context.SaveChanges();
            }

            if (!context.Organizations.Any())
            {
                Organization organization = new Organization()
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
