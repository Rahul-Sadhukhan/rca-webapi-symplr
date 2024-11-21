// <copyright file="ResultErrorModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Orion.Common.Models;

namespace Rca.Tests.Models
{
    internal class ResultErrorModel
    {
        public IEnumerable<FieldErrorModel> Errors { get; set; }
    }
}
