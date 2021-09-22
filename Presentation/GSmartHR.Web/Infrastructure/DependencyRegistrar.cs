using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using NonFactors.Mvc.Grid;
using GSmartHR.Core;
using GSmartHR.IRepository;
using GSmartHR.IRepository.Users;
using GSmartHR.Repository;
using GSmartHR.Repository.Users;
using GSmartHR.Services.Authentication;
using GSmartHR.Services.Security;
using GSmartHR.Services.UploadHelper;
using GSmartHR.Services.Users;
using GSmartHR.Web.Policies;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace GSmartHR.Web.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration _config)
        {
            //http
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IAttendanceRepository), typeof(AttendanceRepository));
            services.AddScoped(typeof(IUserRoleRepository), typeof(UserRoleRepository));
            services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));


            //services
            services.AddScoped(typeof(IEncryptionService), typeof(EncryptionService));
            services.AddScoped(typeof(GSmartHR.Services.Authentication.IAuthenticationService), typeof(CookieAuthenticationService));
            services.AddScoped(typeof(IUserRegistrationService), typeof(UserRegistrationService));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IUserRoleService), typeof(UserRoleService));
            services.AddScoped(typeof(IAttendanceService), typeof(AttendanceService));
            services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));

            //work context
            services.AddScoped(typeof(IWorkContext), typeof(WebWorkContext));
            services.AddScoped(typeof(IFileUploadService), typeof(FileUploadService));


            //other
            services.AddAutoMapper();
       

            //authentication
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-GB");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-GB") };
                options.RequestCultureProviders.Clear();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminHandler",
                    policy => policy.Requirements.Add(new AdminHandler()));

            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("StaffHandler",
                    policy => policy.Requirements.Add(new StaffHandler()));

            });

            services.AddSingleton<IFileProvider>(
              new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //mvc
            services.AddMvc()
                    .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                    .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddMemoryCache();
            services.AddMvcGrid();
        }
    }
}
