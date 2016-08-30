//-----------------------------------------------------------------------
// <copyright file="SyncConfigurationsModule.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service.Modules
{
    using Domain;
    using Nancy;
    using Infrastructure;
    using ResourceModels;
    using Nancy.ModelBinding;
    using AutoMapper;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The sync configuration api endpoint
    /// </summary>
    public class SyncConfigurationsModule : NancyModule
    {
        private readonly ISyncConfigurationRepository syncConfigurationRepository;

        /// <summary>
        /// Initializes a new instance of the SyncConfigurationsModule class
        /// </summary>
        public SyncConfigurationsModule(ISyncConfigurationRepository syncConfigurationRepository) : base("/syncconfigurations")
        {
            this.syncConfigurationRepository = syncConfigurationRepository;
            
            this.Get(
                "/", 
                args => 
                {
                    var syncConfigs = this.syncConfigurationRepository.GetAll();
                    return Response.AsJson(Mapper.Map<IEnumerable<SyncConfigurationRM>>(syncConfigs));
                });
            
            this.Get(
                "/{id}",
                args =>
                {
                    Guid id;
                    if (Guid.TryParse(args.id, out id))
                    {
                        var syncConfig = this.syncConfigurationRepository.Get((Guid)args.id);
                        return Response.AsJson(Mapper.Map<SyncConfigurationRM>(syncConfig));
                    }

                    return HttpStatusCode.BadRequest;
                });

            this.Post(
                "/",
                args =>
                {
                    var syncConfigResource = this.Bind<SyncConfigurationRM>();
                    var syncConfig = Mapper.Map<SyncConfiguration>(syncConfigResource);
                    if (this.syncConfigurationRepository.Save(syncConfig))
                    {
                        return Response.AsJson(Mapper.Map<SyncConfigurationRM>(syncConfig));
                    }

                    return HttpStatusCode.BadRequest;
                });

            this.Put(
                "/{id}",
                args =>
                {
                    var syncConfigResource = this.Bind<SyncConfigurationRM>();
                    var syncConfig = Mapper.Map<SyncConfiguration>(syncConfigResource);
                    if (this.syncConfigurationRepository.Save(syncConfig))
                    {
                        return Response.AsJson(Mapper.Map<SyncConfigurationRM>(syncConfig));
                    }

                    return HttpStatusCode.BadRequest;
                });

            this.Delete(
                "/{id}",
                args =>
                {
                    Guid id;
                    if (Guid.TryParse(args.id, out id))
                    {
                        if (this.syncConfigurationRepository.Delete(id))
                        {
                            return HttpStatusCode.OK; // Deleted successfully
                        }
                    }

                    return HttpStatusCode.BadRequest;
                });
        }
    }
}
