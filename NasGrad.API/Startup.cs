using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NasGrad.API.Auth;
using NasGrad.DBEngine;
using Swashbuckle.AspNetCore.Swagger;

namespace NasGrad.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicyAnyOrigin",
                    builder => 
                 builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "NasGrad API", Version = "v1" });
            });
            services.ConfigureSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerAuthHelper>();
            });


            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.JwtSecret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(CreateAuthPolicies);

            var dbSettings = appSettings.DB;
            
            var dbClient = MongoDBUtil.CreateMongoClient(dbSettings.ServerAddress, dbSettings.ServerPort, dbSettings.Username, dbSettings.Password);            
            var mongoDB = dbClient.GetDatabase(dbSettings.DbName);            

            services.AddSingleton(typeof(MongoDB.Driver.IMongoDatabase), mongoDB);
            services.AddSingleton<IDBStorage, MongoDBStorage>();
        }
        

        private static void CreateAuthPolicies(AuthorizationOptions options)
        {
            var adminRoleType = string.Format(Constants.AuthClaimFormat, Constants.AuthorizationRole,
                (int) AuthRoleType.Admin);
            var superuserRoleType = string.Format(Constants.AuthClaimFormat, Constants.AuthorizationRole,
                (int) AuthRoleType.Superuser);

            options.AddPolicy(Constants.AuthorizationPolicyAdmin,
                policy => policy.RequireAssertion(context => context.User.HasClaim(c => c.Type == adminRoleType)));

            options.AddPolicy(Constants.AuthorizationPolicySuperuser,
                policy => policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == superuserRoleType || c.Type == adminRoleType)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicyAnyOrigin");

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "NasGrad API v1"); });
            }

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
