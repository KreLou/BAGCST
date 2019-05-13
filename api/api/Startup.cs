﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Handler;
using api.Interfaces;
using api.offlineDB;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using api.database;
using api.Databases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using api.Controllers;

namespace api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.Culture = new System.Globalization.CultureInfo("de-DE"));

            services.AddCors();

            //Add Authentiation
            var key = Encoding.ASCII.GetBytes(ServerConfigHandler.ServerConfig.JWT_SecurityKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Add Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("activatedSession", policy =>
                policy.Requirements.Add(new ActivatedSessionRequirement()));
            });
            services.AddTransient<IAuthorizationHandler, ActivatedSessionHandler>();

            //Register dependencies
            registerDependencies(services);

            //Register the Swagger Generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "BAGCST-Backend",
                    Version = "v1",
                    Description = "Documentation BA-Glauchau-Student-Project-Backend"
                });
            });
        }

        private void registerDependencies(IServiceCollection services)
        {
            //Configure Static Dependencies



            //Configure Environment Dependencies
            if (CurrentEnvironment.IsDevelopment())
            {
                //Development
                services.AddSingleton<IContactsDB, offlineContactsDB>();
                services.AddSingleton<IGroupsDB, offlineGroupsDB>();
                services.AddSingleton<IMealDB, OfflineMealDB>();
                services.AddSingleton<IMenuDB, OfflineMenuDB>();
                services.AddSingleton<INewsDB, offlineNewsDB>();
                services.AddSingleton<IPlaceDB, OfflinePlaceDB>();
                services.AddSingleton<IPostGroupDB, offlinePostGroupDB>();
                services.AddSingleton<IRightsDB, offlineRightsDB>();
                services.AddSingleton<ISemesterDB, offlineSemesterDB>();
                services.AddSingleton<ITimetableDB, offlineTimetableDB>();
                services.AddSingleton<IUserDB, offlineUserDB>();
                services.AddSingleton<IUserSettingsDB, offlineUserSettings>();
                services.AddSingleton<IUserGroupBindingDB, offlineUserGroupBindingDB>();
                services.AddSingleton<IStudyCourseDB, offlineStudyCourseDB>();
                services.AddSingleton<IStudyGroupDB, offlineStudyGroupDB>();
            }else
            {
                //Production
                services.AddSingleton<IContactsDB, onlineContactsDB>();
                services.AddSingleton<IGroupsDB, offlineGroupsDB>();
                services.AddSingleton<IMealDB, OfflineMealDB>();
                services.AddSingleton<IMenuDB, OfflineMenuDB>();
                services.AddSingleton<INewsDB, onlineNewsDB>();
                services.AddSingleton<IPlaceDB, OfflinePlaceDB>();
                services.AddSingleton<IPostGroupDB, onlinePostGroupDB>();
                services.AddSingleton<IRightsDB, offlineRightsDB>();
                services.AddSingleton<ISemesterDB, offlineSemesterDB>();
                services.AddSingleton<ITimetableDB, onlineTimetableDB>();
                services.AddSingleton<IUserDB, onlineUserDB>();
                services.AddSingleton<IUserSettingsDB, onlineUserSettings>();
                services.AddSingleton<IUserGroupBindingDB, offlineUserGroupBindingDB>();
                services.AddSingleton<IStudyCourseDB, offlineStudyCourseDB>();
                services.AddSingleton<IStudyGroupDB, offlineStudyGroupDB>();
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder =>
                    builder.WithOrigins("http://localhost:4200/").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
                    .UseRequestLocalization(new RequestLocalizationOptions {DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(new System.Globalization.CultureInfo("de-DE"))});
            if (env.IsDevelopment())
            {
                //Enable Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BAGCST-Backend v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
        
    }
}
