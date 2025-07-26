using DatingAppCourse.Api.Data;
using DatingAppCourse.Api.Helper;
using DatingAppCourse.Api.Interfaces;
using DatingAppCourse.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace DatingAppCourse.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddControllers();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Ensure AutoMapper is properly referenced
           services.AddAutoMapper(typeof(AutoMapperProfiles));

            return services;
        }
    }
}
