// <copyright file="SecurityItemResolver.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Orion.Api.Filters.UserItemSecurityApiActionFilter;

namespace Rca.Resolvers
{
    internal class SecurityItemResolver : ISecurityItemResolver
    {
        public long? GetSecurityItemId(Dictionary<string, object> actionArguments)
        {
            // need to add the implementation
            return null;
        }
    }
}
