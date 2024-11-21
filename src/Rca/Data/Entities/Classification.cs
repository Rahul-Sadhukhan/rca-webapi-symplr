// <copyright file="Classifications.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;

namespace Rca.Data.Entities
{
    internal class Classification
    {
        public int ClassificationId { get; set; }

        public string ClassificationName { get; set; }

        public ICollection<Cause> Causes { get; set; }
    }
}
