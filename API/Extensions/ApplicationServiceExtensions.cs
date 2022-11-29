using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(option=>
            {
                option.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            //change with upper one if any error happen
            // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<LogUserActivity>();
            services.AddScoped<ILikesRepository, LikeRepository>();
            return services;
        }
    }
} 