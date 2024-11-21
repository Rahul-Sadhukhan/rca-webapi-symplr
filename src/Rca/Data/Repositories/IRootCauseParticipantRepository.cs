// <copyright file="IRootCauseParticipantRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal interface IRootCauseParticipantRepository
    {
        IEnumerable<RootCauseParticipant> AddRootCauseParticipants(IEnumerable<RootCauseParticipant> rcaParticipantList);
    }
}
