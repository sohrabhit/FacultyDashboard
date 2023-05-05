﻿using Common;
using Common.Exceptions;
using Common.Utilities;
using Data;
using Data.Repositories;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseSqlServer(configuration.GetConnectionString("SqlServer"));
                //Tips
                //Automatic client evaluation is no longer supported. This event is no longer generated.
                //This line is no longer needed.
                //.ConfigureWarnings(warning => warning.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
        }

        public static void AddMinimalMvc(this IServiceCollection services)
        {
            //https://github.com/aspnet/AspNetCore/blob/0303c9e90b5b48b309a78c2ec9911db1812e6bf3/src/Mvc/Mvc/src/MvcServiceCollectionExtensions.cs
            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter()); //Apply AuthorizeFilter as global filter to all actions

                //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
                //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
                //Use this filter when use cookie 
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                //options.UseYeKeModelBinder();
            }).AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.Converters.Add(new StringEnumConverter());
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //option.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                //option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.AddSwaggerGenNewtonsoftSupport();

            #region Old way (We don't need this from ASP.NET Core 3.0 onwards)
            ////https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
            // AddMvc : خود این حاوی کلیه موارد زیر هست
            services.AddMvcCore(options =>
            {
                options.Filters.Add(new AuthorizeFilter());

                //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
                //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
                //Use this filter when use cookie 
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                //options.UseYeKeModelBinder();
            })
            .AddApiExplorer() // اگر خواستیم autofac اضافی کنیم این نیاز هست . 
            .AddAuthorization()
            .AddFormatterMappings()
            .AddDataAnnotations()
            .AddJsonOptions(option =>
            {
                //option.JsonSerializerOptions
            })
            .AddNewtonsoftJson(/*option =>
            {
                option.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }*/)

            //Microsoft.AspNetCore.Mvc.Formatters.Json
            //.AddJsonFormatters(/*options =>
            //{
            //    options.Formatting = Newtonsoft.Json.Formatting.Indented;
            //    options.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //}*/)

            .AddCors()
            .SetCompatibilityVersion(CompatibilityVersion.Latest); //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            #endregion
        }

        public static void AddElmahCore(this IServiceCollection services, IConfiguration configuration, SiteSettings siteSetting)
        {
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.Path = siteSetting.ElmahPath;
                options.ConnectionString = configuration.GetConnectionString("Elmah");
                // شرط برای افرادی که اجازه دسترسی به صفحه خطاها رو دارند
                //options.CheckPermissionAction = httpContext => httpContext.User.Identity.IsAuthenticated;
            });
        }

        // تنظیمات jwt
        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptKey);

                // پارامترهای لازم برای اعتبارسنجی توکن
                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min // اگر 5 باشه 5 دقیقه بعد و قبل انقضا هم کار میکنه که ما 0 میکنیم
                    RequireSignedTokens = true, // یعنی حتما توکن ها امضا داشته باشن

                    ValidateIssuerSigningKey = true, // امضای jwt حتما بررسی بشه
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey), // نحوه اعتبارسنجی که بالایی true شد

                    RequireExpirationTime = true, // زمان منقضی شدن توکن بررسی بشه
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false // مصرف کننده توکن رو حتما بررسی کن
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuer = true, //default : false // صادر کننده توکن رو حتما بررسی کن
                    ValidIssuer = jwtSettings.Issuer,

                    // رمزگشایی کلید سرور
                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;

                // اعتبارسنجی SecurityStamp
                // رخدادهای توکن jwt
                options.Events = new JwtBearerEvents
                {
                    // وقتی اعتبارسنجی با شکست مواجه بشه
                    OnAuthenticationFailed = context =>
                    {
                        // توجه : نحوه دسترسی به سرویس
                        //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        //logger.LogError("Authentication failed.", context.Exception);

                        if (context.Exception != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

                        return Task.CompletedTask;
                    },
                    // وقتی اعتبارسنجی درست هست بعد از اون این کارها رو میکنیم
                    OnTokenValidated = async context =>
                    {
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                        
                        // مهم مهم : اگر از تزریق وابستگی داخل سازنده نتونستید استفائه کنید فقط در حالت بحرانی میشه از این روش استفاده کنید
                        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                        if (!securityStamp.HasValue())
                            context.Fail("This token has no security stamp");

                        //Find user and token from database and perform your custom validation
                        var userId = claimsIdentity.GetUserId<string/*int*/>();
                        // context.HttpContext.RequestAborted : این همون CancellationToken هست
                        var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

                        if (user.SecurityStamp != securityStamp)//Guid.Parse(securityStamp))
                            context.Fail("Token security stamp is not valid.");

                        var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validatedUser == null)
                            context.Fail("Token security stamp is not valid.");

                        if (!user.IsActive)
                            context.Fail("User is not active.");

                        await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
                    },
                    // وقتی اکشنی احراز هویت نیاز داره ولی توکن ارسال نمیکنیم بهش
                    OnChallenge = context =>
                    {
                        //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);

                        if (context.AuthenticateFailure != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                        throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);

                        //return Task.CompletedTask;
                    }
                };
            });
        }

        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                //url segment => {version}
                // اگه هیچ ورژنی مشخص نشده بود خودت یه ورژنی رو بعنوان پیش فرض استفاده کن
                options.AssumeDefaultVersionWhenUnspecified = true; //default => false;
                options.DefaultApiVersion = new ApiVersion(1, 0); //v1.0 == v1 اگه بالایی بله شد اینم باید مشخص بشه
                options.ReportApiVersions = true;

                // میشه اینجوری هم تبدیل کرد
                //ApiVersion.TryParse("1.0", out var version10);
                //ApiVersion.TryParse("1", out var version1);
                //var a = version10 == version1;

                // تعیین روش خوندن api
                //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                // api/posts?api-version=1   =>  اگه ورژن مثل بالا کوئری استرینگ باشه

                //options.ApiVersionReader = new UrlSegmentApiVersionReader();
                // api/v1/posts

                //options.ApiVersionReader = new HeaderApiVersionReader(new[] { "Api-Version" });
                // header => Api-Version : 1

                //options.ApiVersionReader = new MediaTypeApiVersionReader()

                //options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"), new UrlSegmentApiVersionReader())
                // combine of [querystring] & [urlsegment]
            });
        }
    }
}
