//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    
    /// <summary>
    /// The entry point class for the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point method
        /// </summary>
        /// <param name="args">Command line arguments passed in</param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
