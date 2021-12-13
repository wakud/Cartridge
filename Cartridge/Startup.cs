using Cartridge.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cartridge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //отримуємо рядок підключення до БД
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // створюємо БД
            services.AddDbContext<MainContext>(options => options.UseSqlServer(connection));

            // авторизація користувача
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });

            //робимо перевірку на адміністратора
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("OnlyForAdministrator", policy => {
                    policy.RequireClaim("AdminStatus", "1");
                });
            });

            services.AddControllersWithViews();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.EnvironmentName = "Production";   //в стані робочого продукту
            //if (env.IsDevelopment())    //перевіряємо чи в стані розробки
            //{
                app.UseDeveloperExceptionPage();    //в стані розробки виводимо помилки
            //}
            //else    //в стані продакшин виводимо помилку 404
            //{
                //app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
            //}

            app.UseStaticFiles();       //чтобы приложение могло бы отдавать статические файлы клиенту
            app.UseRouting();           // добавляем возможности маршрутизации
            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //при старті проги запускаємо сторінку логіну
                    pattern: "{controller=Account}/{action=Login}/{id?}"
                    );
            });

            //Спочатку запускаємо створення БД і наповнюємо її перед сторінкою логіну
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            MainContext context = scope.ServiceProvider.GetRequiredService<MainContext>();
            //DbInitialization.Initial(context);
        }
    }
}
