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
            //�������� ����� ���������� �� ��
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // ��������� ��
            services.AddDbContext<MainContext>(options => options.UseSqlServer(connection));

            // ����������� �����������
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });

            //������ �������� �� ������������
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
            //env.EnvironmentName = "Production";   //� ���� �������� ��������
            //if (env.IsDevelopment())    //���������� �� � ���� ��������
            //{
                app.UseDeveloperExceptionPage();    //� ���� �������� �������� �������
            //}
            //else    //� ���� ��������� �������� ������� 404
            //{
                //app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
            //}

            app.UseStaticFiles();       //����� ���������� ����� �� �������� ����������� ����� �������
            app.UseRouting();           // ��������� ����������� �������������
            app.UseAuthentication();    // ��������������
            app.UseAuthorization();     // �����������

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //��� ����� ����� ��������� ������� �����
                    pattern: "{controller=Account}/{action=Login}/{id?}"
                    );
            });

            //�������� ��������� ��������� �� � ���������� �� ����� �������� �����
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            MainContext context = scope.ServiceProvider.GetRequiredService<MainContext>();
            //DbInitialization.Initial(context);
        }
    }
}
