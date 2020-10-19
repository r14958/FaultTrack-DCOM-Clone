// -----------------------------------------------------------------------------
//  <copyright file="Startup.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Web
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Class that provides application startup and configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializates a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/> configuration for the application.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration object.
        /// </summary>
        public IConfiguration Configuration
        {
            get;
        }

        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> container that contains application services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddIdentity<User, UserRole>()
                    .AddDefaultTokenProviders();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<UserRole>, RoleStore>();
            services.ConfigureApplicationCookie(options =>
                                                {
                                                    options.Cookie.HttpOnly = true;
                                                    options.LoginPath = "/Login";
                                                    options.LogoutPath = "/Logout";
                                                });

            services.Configure<IdentityOptions>(options =>
                                                {
                                                    options.Password.RequireDigit           = false;
                                                    options.Password.RequireLowercase       = false;
                                                    options.Password.RequireNonAlphanumeric = false;
                                                    options.Password.RequireUppercase       = false;
                                                    options.Password.RequiredLength         = 0;
                                                    options.Password.RequiredUniqueChars    = 0;
                                                });
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> application builder.</param>
        /// <param name="env">The <see cref="IWebHostEnvironment"/> environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}