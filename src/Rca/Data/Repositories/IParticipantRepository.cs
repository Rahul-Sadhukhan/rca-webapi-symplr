// <copyright file="IParticipantRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal interface IParticipantRepository
    {
        List<Participant> GetAllParticipants();

        IEnumerable<Participant> AddParticipants(IEnumerable<Participant> participantList);
    }
}
