using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NFClinic.Data.Data;
using Microsoft.EntityFrameworkCore;
using NFClinic.Data.Models.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using NFClinic.Core.Persistance;
using NFClinic.Data.Presistence;
using NFClinic.Core.Repository;
using NFClinic.Data.Repository;
using NFClinic.Services.PatientService;
using AutoMapper;
using NFClinic.AppSettings;

namespace NFClinic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			//Add DBContext
			services.AddDbContext<NFClinicContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
					sqlOptions.MigrationsAssembly("NFClinic.Data")));

			//Add identity
			services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 3;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;

			})
			.AddEntityFrameworkStores<NFClinicContext>()
			.AddDefaultTokenProviders();


			//Return 401 on unauthorized requests
			services.ConfigureApplicationCookie(cfg =>
			{
				cfg.Events = new CookieAuthenticationEvents
				{
					OnRedirectToLogin = ctx =>
					{
						if (ctx.Request.Path.StartsWithSegments("/api"))
							ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

						return Task.FromResult(0);
					}
				};
			});

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(cfg =>
			{
				cfg.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = Configuration["JwtSecurityToken:Issuer"],

					ValidateAudience = true,
					ValidAudience = Configuration["JwtSecurityToken:Issuer"],

					RequireExpirationTime = true,
					ValidateLifetime = true,

					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityToken:Key"])),

					ClockSkew = TimeSpan.Zero
				};

			});

			//Add JWT AppSettings
			services.Configure<JwtSettings>(Configuration.GetSection("JwtSecurityToken"));

			//Add Unit of Work
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			//Add repos and services
			services.AddScoped<IPatientRepository, PatientRepository>();
			services.AddTransient<IPatientService, PatientService>();

			//Add AutoMapper
			services.AddAutoMapper();

			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			//Create initial roles
			UserRoleFactory.CreateRolesAsync(serviceProvider).Wait();

			//Create initial admin user
			UserRoleFactory.CreateAdminUserAsync(serviceProvider).Wait();

			app.UseAuthentication();
			app.UseMvc();
        }
    }
}
