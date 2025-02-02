

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 7229, o => o.Protocols = HttpProtocols.Http2);

    // ADDED THIS LINE to fix the problem
    options.Listen(IPAddress.Any, 5171, o => o.Protocols = HttpProtocols.Http1);

});
builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagementUrl"]);
builder.Services.AddScoped<PermissionsCheckerMiddleware, PermissionsCheckerMiddleware>();
builder.Services.AddHostedService<AddPermissionsMiddleware>();
builder.Services.AddScoped<PermissionsGroupingCheckerMiddleware, PermissionsGroupingCheckerMiddleware>();

// Add services to the container.
builder.Services.AddControllers();

// Add unit of work & GRepo
builder.Services.AddUnitOfWorkRepository();
builder.Services.AddDiServices();
builder.Services.AddDbConfig(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors",
        builder =>
        {
            builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000", "http://57.129.28.127:3100", "http://57.129.28.126:3100")
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials();
        });
});

var logger = new LoggerConfiguration()
         .ReadFrom.Configuration(builder.Configuration)
         .Enrich.FromLogContext()
         .CreateLogger();
builder.Host.UseSerilog(logger);
// Add grpc config to serviceCollection
builder.Services.AddGrpc();
//builder.Services.AddGrpcReflection();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "FAQ APIs Reference",
    });

    c.AddSecurityDefinition(
    "token",
    new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization
    }
        );
    c.AddSecurityRequirement(
    new OpenApiSecurityRequirement
    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
    }
        );
});
#endregion

builder.Services.AddAuthentication("Bearer")
  .AddJwtBearer("Bearer", options =>
  {
      // the name of your api resources   
      options.Audience = "UserManagementServer";
      /// identity server url                    
      options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
      options.RequireHttpsMetadata = false;
  });

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PostResolveValidation>());

var app = builder.Build();

app.MapGrpcService<ResolveReportService>();

app.UseMiddleware<GlobalExceptionHandler>();

// Configure the HTTP request pipeline.


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "/wwwroot"
});

app.ApplyAutoMigration();

// Add GrpcServices
app.AddMapGrpcServices();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowCors");
//}
//app.UseRequestPostActionContollerName();
app.UseStaticFiles();
app.UseRouting();
//app.UseHttpsRedirection();
//app.UseMiddleware<PostActionContollerNameMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<PermissionsCheckerMiddleware>();
app.UseMiddleware<PermissionsGroupingCheckerMiddleware>();
app.MapControllers();
app.UseSerilogRequestLogging();


app.Run();

