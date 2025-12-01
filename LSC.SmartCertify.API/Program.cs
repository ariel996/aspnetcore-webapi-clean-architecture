
using LSC.SmartCertify.Application;
using LSC.SmartCertify.Application.Interfaces.Courses;
using LSC.SmartCertify.Application.Services;
using LSC.SmartCertify.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace LSC.SmartCertify.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SmartCertifyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
                    providerOptions => providerOptions.EnableRetryOnFailure());
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseCors("default");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("My API");
                    options.WithTheme(ScalarTheme.DeepSpace);
                    options.WithSidebar(false);
                });

                app.UseSwaggerUi(options =>
                {
                    options.DocumentPath = "openapi/v1.json";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
