// <copyright file="LinkedItemModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Models
{
    public class LinkedItemModel
    {
        public long? ItemId { get; set; }

        public string Name { get; set; }

        public bool IsReadPermissionEnabled { get; set; }
    }
}
