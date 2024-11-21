// <copyright file="CauseSelectListModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;

namespace Rca.Models
{
    /// <summary>
    /// Model for cause select list.
    /// </summary>
    public class CauseSelectListModel
    {
        public int CauseId { get; set; }

        public string Description { get; set; }

        public int? ParentCauseId { get; set; }

        public List<CauseSelectListModel> Subcauses { get; set; }
    }
}
