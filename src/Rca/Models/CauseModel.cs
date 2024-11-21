// <copyright file="CauseModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;

namespace Rca.Models
{
    /// <summary>
    /// Cause model.
    /// </summary>
    public class CauseModel
    {
        public int CauseId { get; set; }

        public int RCAId { get; set; }

        public string Description { get; set; }

        public int? ClassificationId { get; set; }

        public int? ParentCauseId { get; set; }
    }
}
