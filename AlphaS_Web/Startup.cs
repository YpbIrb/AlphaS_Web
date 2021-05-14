using AlphaS_Web.Models;
using AlphaS_Web.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Identity.Mongo;
using AlphaS_Web.Areas.Identity.Data;
using AspNetCore.Identity.Mongo.Model;
using AlphaS_Web.Services.ApiAuthentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AlphaS_Web
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
            services.Configure<AlphaSDatabaseSettings>(Configuration.GetSection(nameof(AlphaSDatabaseSettings)));

            const string signingSecurityKey = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n1k41230";
            var signingKey = new SigningSymmetricKey(signingSecurityKey);
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);


            services.AddRazorPages()
            .AddRazorPagesOptions(options =>
            {
                options.Conventions
                       .ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            });
            
            services.AddMvc().AddRazorPagesOptions(o =>
            {
                o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            });
            
            services.AddSingleton<IAlphaSDatabaseSettings>(x => x.GetRequiredService<IOptions<AlphaSDatabaseSettings>>().Value);

            //Console.WriteLine("1_services.Count : " + services.Count);

           

            //Console.WriteLine("2_services.Count : " + services.Count);

            const string jwtSchemeName = "JwtBearer";

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = jwtSchemeName;
                    options.DefaultChallengeScheme = jwtSchemeName;
                })
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),

                        ValidateIssuer = true,
                        ValidIssuer = "AlphaS",

                        ValidateAudience = true,
                        ValidAudience = "AlphaSClient",

                        ValidateLifetime = false,

                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                });

            services.AddIdentityMongoDbProvider<ApplicationUser, MongoRole>(identityOptions =>
            {

                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireDigit = false;
            }, mongoIdentityOptions => {
                mongoIdentityOptions.ConnectionString = "mongodb://mongo:27017/AlphaSDB";
            });


            //Console.WriteLine("3_services.Count : " + services.Count);

            //services.AddAuthorization();

            //Console.WriteLine("4_services.Count : " + services.Count);

            /*
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyCustomCookieName";
            });
            */
            /*
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = jwtSchemeName;
                    options.DefaultChallengeScheme = jwtSchemeName;
                })
                .AddCookie();
                */
            /*
            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = jwtSchemeName;
                    options.DefaultChallengeScheme = jwtSchemeName;
                })
                .AddCookie()
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),

                        ValidateIssuer = true,
                        ValidIssuer = "AlphaS",

                        ValidateAudience = true,
                        ValidAudience = "AlphaSClient",

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                });
            */
            /*
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie("cookie1", "cookie1", options =>
                {
                    options.Cookie.Name = "cookie1";
                })
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),

                        ValidateIssuer = true,
                        ValidIssuer = "AlphaS",

                        ValidateAudience = true,
                        ValidAudience = "AlphaSClient",

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromSeconds(100)
                    };
                });*/


            services.AddSingleton<CounterContext>();
            services.AddSingleton<ParticipantContext>();
            services.AddSingleton<ModuleContext>();
            services.AddSingleton<ModuleTypeContext>();
            services.AddSingleton<ExperimentContext>();
            services.AddSingleton<ExperimentPresetContext>();
            services.AddSwaggerGen();



            services.AddControllersWithViews();

            
            

            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Supplement V1");
            });
        }
    }
}
