using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServerApp.Entities;
using System;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ServerApp.Data;
using ServerApp.Repositories.Abstract;
using ServerApp.Repositories.Concrete;
using ServerApp.Helpers;

namespace ServerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowOrigins = "_myAllowOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServerApp", Version = "v1" });
            });

            services.AddDbContext<SociAllContext>(options =>
                options.UseSqlServer("Data Source=DESKTOP-AJT2GI5; Initial Catalog=SociALLDb;Integrated Security=SSPI;"));

            services.AddAutoMapper(typeof(Startup));
            services.AddCors(options => {
                options.AddPolicy(
                    name: MyAllowOrigins,
                    builder => {
                        builder
                            .WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddIdentity<User, Role>().AddEntityFrameworkStores<SociAllContext>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserToUserRepository,UserToUserRepository>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Hesabı 5 dakika kilitle
                options.Lockout.MaxFailedAccessAttempts = 5; // şifre 5 kez yanlış girilirse kilitle 
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Secrets").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddScoped<LastActiveActionFilter>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServerApp v1"));
                SeedDatabase.Seed(userManager).Wait();
            }
            else{
                app.UseExceptionHandler(appError => {
                    
                    appError.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var exception = context.Features.Get<IExceptionHandlerFeature>();

                        if(exception != null){
                            await context.Response.WriteAsync(new ErrorDetails(){
                                StatusCode = context.Response.StatusCode,
                                Message = exception.Error.Message 
                            }.ToString());
                        }
                    });
                });
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowOrigins);
            app.UseAuthentication(); // Mutlaka authorization ın üzerinde tanımlanmalı
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
