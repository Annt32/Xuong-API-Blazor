using AppAPI.Repositories;
using AppData.AppDbContext;
using AppData.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDBContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IRepository<Field>, Repository<Field>>();
            builder.Services.AddScoped<IRepository<FieldShift>, Repository<FieldShift>>();
            builder.Services.AddScoped<IRepository<Invoice>, Repository<Invoice>>();
            builder.Services.AddScoped<IRepository<InvoiceDetail>, Repository<InvoiceDetail>>();

			builder.Services.AddAutoMapper(typeof(AutoMapperProfile.AppMapperProfile).Assembly);
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
