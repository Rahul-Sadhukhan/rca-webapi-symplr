// <copyright file="ParticipantODataModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Runtime.Serialization;

namespace Rca.Models.OData
{
    /// <summary>
    /// OData model for Participants.
    /// </summary>
    [DataContract]
    public class ParticipantODataModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for participant.
        /// </summary>
        [DataMember(Name = "participantId")]
        public int ParticipantId { get; set; }

        /// <summary>
        /// Gets or sets the participant name.
        /// </summary>
        [DataMember(Name = "participantName")]
        public string ParticipantName { get; set; }
    }
}
