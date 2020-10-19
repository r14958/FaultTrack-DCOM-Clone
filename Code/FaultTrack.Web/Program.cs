// -----------------------------------------------------------------------------
//  <copyright file="Program.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Web
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Main application class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main application loop.
        /// </summary>
        /// <param name="args">The array of application arguments passed to the program by the environment.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates a the application host.
        /// </summary>
        /// <param name="args">The array of application arguments passed to the program by the environment.</param>
        /// <returns>Returns the <see cref="IHostBuilder"/> that provides program initialization.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                                                 {
                                                     webBuilder.UseStartup<Startup>();
                                                 });
        }
    }
}