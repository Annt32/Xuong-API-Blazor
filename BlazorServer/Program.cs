using AppData.DTO;
using AppData.Entities;
using BlazorServer.Data;
using BlazorServer.IServices;
using BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            // Đăng ký HttpClient
            builder.Services.AddHttpClient();

            // Đăng ký các dịch vụ
            builder.Services.AddScoped<IFieldService, FieldService>();
            builder.Services.AddHttpClient<IServices<WebUser>, UserService>(); // Sửa lại cách đăng ký UserService với HttpClient
            builder.Services.AddScoped<IFieldTypeServices, FieldTypeService>();
            builder.Services.AddScoped<IFieldShiftService, FieldShiftService>();
            builder.Services.AddScoped<IInvoiceDetailServices, InvoiceDetailService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}