// <copyright file="CreateRCAModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace Rca.Models
{
    public class CreateRCAModel
    {
        public string RcaName { get; set; }

        public string Theme { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? IncidentDate { get; set; }

        public IEnumerable<CreateParticipantModel> Participants { get; set; }

        public IEnumerable<long> ItemIds { get; set; }
    }
}
