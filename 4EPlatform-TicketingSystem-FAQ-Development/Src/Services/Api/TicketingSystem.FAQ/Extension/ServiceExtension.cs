

namespace TicketingSystem.TicketingServices.Extension
{
    public static class ServiceExtension
    {
        public static void AddUnitOfWorkRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));
        }
        public static void AddDiServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(ApplicationLayer)));
            services.AddAutoMapper((typeof(ApplicationLayer)));
            services.AddHttpContextAccessor();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IResponseHelper, ResponseHelper>();
            services.AddScoped<GlobalExceptionHandler, GlobalExceptionHandler>();
            services.AddScoped<PermissionsCheckerMiddleware, PermissionsCheckerMiddleware>();
            services.AddSingleton<ITokenService, TokenService>();

        }


        public static void AddDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(options =>
         {
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

             options.EnableSensitiveDataLogging(true);
             options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
         });
        }

        public static void ApplyAutoMigration(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DBContext>();
                var serviceProvider = serviceScope.ServiceProvider;
                if (!serviceScope.ServiceProvider.GetService<DBContext>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<DBContext>().Migrate();
                }

            }
        }
        public static void AddMapGrpcServices(this WebApplication app)
        {
            //app.MapGrpcService<MainClassificationService>();
            app.MapGrpcService<FAQControllerAndActionNameService>();
        }
    }
}
