//-----------------------------------------------------------------------
// <copyright file="HealthCheckModule.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service.Modules
{
    using Nancy;

    /// <summary>
    /// The health check api endpoint
    /// </summary>
    public class HealthCheckModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the HealthCheckModule class
        /// </summary>
        public HealthCheckModule() : base("/healthcheck")
        {   
            this.Get(
                "/", 
                args => 
                {
                    return HttpStatusCode.OK;
                });
        }
    }
}
