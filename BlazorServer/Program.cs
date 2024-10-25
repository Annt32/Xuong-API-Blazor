using AppData.DTO.User_RoleDto;
using AppData.Entities;
using Blazored.SessionStorage;
using BlazorServer.Data;
using BlazorServer.Handlers;
using BlazorServer.IServices;
using BlazorServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AuthenticationService = BlazorServer.Services.AuthenticationService;
using IAuthenticationService = BlazorServer.IServices.IAuthenticationService;

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

            builder.Services.AddTransient<AuthenticationHandler>(); 


            // Đăng ký HttpClient
            builder.Services.AddHttpClient("ServerApi")
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
                    .AddHttpMessageHandler<AuthenticationHandler>();

            // Đăng ký các dịch vụ
            builder.Services.AddScoped<IFieldService, FieldService>();
            builder.Services.AddHttpClient<IServices<WebUser>, UserService>(); // Sửa lại cách đăng ký UserService với HttpClient
            builder.Services.AddScoped<IFieldTypeServices, FieldTypeService>();
            builder.Services.AddScoped<IFieldShiftService, FieldShiftService>();
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddScoped<IShiftService, ShiftService>();

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