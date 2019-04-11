using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NasGrad.API.Auth;
using NasGrad.DBEngine;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;
using System.Threading.Tasks;

namespace NasGrad.API
{
    public class Startup
    {
        private const string AuthLoginPath = "/security/auth/login";
        private const string SwaggerSuffix = "/swagger/index.html";

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
                options.SwaggerDoc("v1", new Info {Title = "NasGrad API", Version = "v1"});                
            });
            services.ConfigureSwaggerGen(options => { options.OperationFilter<SwaggerAuthHelper>(); });


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

            var dbClient = MongoDBUtil.CreateMongoClient(dbSettings.ServerAddress, dbSettings.ServerPort,
                dbSettings.Username, dbSettings.Password);
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

            //if (env.IsDevelopment())
            {
                app.UseStatusCodePages(context =>
                {
                    var response = context.HttpContext.Response;

                    if (response.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        response.Redirect(AuthLoginPath);                        
                    }

                    return Task.CompletedTask;
                });

                RequireAuthenticationOn(app, "/swagger");
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NasGrad API v1");
                });
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


        /// <summary>
        /// Requires authentication for paths that starts with <paramref name="pathPrefix" />
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="pathPrefix">The path prefix</param>
        /// <returns>The application builder</returns>
        private IApplicationBuilder RequireAuthenticationOn(IApplicationBuilder app, string pathPrefix)
        {
            return app.Use((context, next) =>
            {
                // First check if the current path is the swagger path
                if (context.Request.Path.HasValue &&
                    context.Request.Path.Value.StartsWith(pathPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    // Secondly check if the current user is authenticated
                    if (!context.User.Identity.IsAuthenticated)
                    {
                        string referer = context.Request.Headers["Referer"].ToString();
                        var authLoginRedirect = context.Request.Host.Value + AuthLoginPath;
                        var swaggerRedirect = context.Request.Host.Value + SwaggerSuffix;

                        if ( (!referer.Contains(authLoginRedirect, StringComparison.OrdinalIgnoreCase) && 
                              !referer.Contains(swaggerRedirect, StringComparison.OrdinalIgnoreCase)) ) 

                            return context.ChallengeAsync();
                    }
                }

                return next();
            });
        }
    }
}
