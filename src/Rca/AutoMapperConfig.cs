// <copyright file="AutoMapperConfig.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca
{
    using AutoMapper;
    using Orion.Api;
    using Orion.Clients;
    using Orion.Multilingual;
    using Orion.Permissions;
    using Orion.Users;

    /// <summary>
    /// AutoMapper Config file.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// AutoMapper Configuration.
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new RcaAutoMapperProfile());
                cfg.AddProfile(new UsersComponentAutoMapperProfile());
                cfg.AddProfile(new ClientComponentAutoMapperProfile());
                cfg.AddProfile(new WebApiComponentAutoMapperProfile());
                cfg.AddProfile(new PermissionsComponentAutoMapperProfile());
                cfg.AddProfile(new MultilingualComponentAutoMapperProfile());
            });
        }
    }
}
