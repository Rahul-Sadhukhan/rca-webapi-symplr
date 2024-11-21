// <copyright file="ItemODataModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Runtime.Serialization;

namespace Rca.Models.OData
{
    /// <summary>
    /// OData model for Items.
    /// </summary>
    [DataContract]
    public class ItemODataModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for item.
        /// </summary>
        [DataMember(Name = "ItemId")]
        public long ItemId { get; set; }

        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets item description.
        /// </summary>
        [DataMember(Name = "Description")]
        public string Description { get; set; }
    }
}
