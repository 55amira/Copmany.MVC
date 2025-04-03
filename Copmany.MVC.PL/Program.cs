using Company.MVC.BLL;
using Company.MVC.BLL.Interface;
using Company.MVC.BLL.Repositories;
using Company.MVC.DAL.Data.Context;
using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Controllers;
using Copmany.MVC.PL.Mapping;
using Copmany.MVC.PL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Copmany.MVC.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfwork,UnitOfWork>();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"));
            });

            // builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M=>M.AddProfile(new EmployeeProfile()));

            //builder.Services.AddScoped();
            //builder.Services.AddTransient();
            //builder.Services.AddSingleton();

            builder.Services.AddScoped<IScopedService, ScopedService>();
            builder.Services.AddTransient<ITransentService, TransentService>();
            builder.Services.AddSingleton<ISengletonSerivce,SengletonService>();    

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
