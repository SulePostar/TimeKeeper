using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TimeKeeper.API.Handlers;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;

namespace TimeKeeper.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder().SetBasePath(environment.ContentRootPath).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<IISOptions>(o => o.AutomaticAuthentication = false);

            services.AddAuthorization(o =>
            {
                o.AddPolicy("IsAdmin", builder => {
                    builder.RequireClaim("role", "admin");
                });
                o.AddPolicy("IsLead", builder => {
                    builder.RequireClaim("role", "lead");
                });
                o.AddPolicy("IsMember", builder =>
                {
                    builder.AuthenticationSchemes.Add("TokenAuthentication");
                    builder.AddRequirements(new IsMemberRequirement());
                });
                o.AddPolicy("IsSomething", builder => {
                    builder.AddRequirements(new IsMemberRequirement());
                });
            });
            services.AddScoped<IAuthorizationHandler, IsMemberHandler>();

            if(Configuration["Connection:Type"] == "SQL")
            {
                services.AddDbContext<TimeContext>(o => o.UseSqlServer(Configuration["Connection:MsSql"]));
            }
            else
            {
                services.AddDbContext<TimeContext>(o => o.UseNpgsql(Configuration["Connection:PgSql"]));
            }

            services.AddAuthentication("TokenAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>("TokenAuthentication", null);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "https://localhost:44300";
                o.Audience = "timekeeper";
                o.RequireHttpsMetadata = false;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors(c => c.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}