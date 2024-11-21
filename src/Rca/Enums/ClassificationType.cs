// <copyright file="ClassificationType.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Runtime.Serialization;

namespace Rca.Enums
{
    public enum ClassificationType : int
    {
        [EnumMember(Value = "No classification")]
        NoClassification = 8,
    }
}
