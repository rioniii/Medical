using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.Services;
using System.Text;

public class Program
{
    private readonly IConfiguration _configuration;

    public Program(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var startup = new Program(builder.Configuration);
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        startup.Configure(app, app.Environment);

        // Database seeding logic
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await ApplicationDbContextSeed.SeedEssentialsAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }

        app.Run();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureJwt(services); // JWT configuration
        ConfigureIdentity(services); // Identity setup
        ConfigureDatabase(services); // Database setup
        ConfigureCors(services); // CORS setup
        ConfigureSwagger(services); // Swagger setup

        services.AddScoped<IUserService, UserService>(); // Additional services

        services.AddControllersWithViews();
        services.AddEndpointsApiExplorer(); // Enable API endpoint discovery
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medical API v1"));
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // Apply CORS BEFORE authentication
        app.UseCors("AllowSpecificOrigin");

        // Authentication & Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Map API controllers
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private void ConfigureJwt(IServiceCollection services)
    {
        var jwtSettings = _configuration.GetSection("JWT");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        services.Configure<JWT>(jwtSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    private void ConfigureIdentity(IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    private void ConfigureDatabase(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                _configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
    }

    private void ConfigureCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.WithOrigins("https://localhost:5173", "http://localhost:5173") // Include both HTTP and HTTPS
                       .AllowAnyMethod()  // Allow GET, POST, PUT, DELETE, etc.
                       .AllowAnyHeader()  // Allow all headers (e.g., Authorization, Content-Type)
                       .AllowCredentials(); // Allow cookies and Authorization headers
            });
        });
    }

    private void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Medical API",
                Version = "v1"
            });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid JWT token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}