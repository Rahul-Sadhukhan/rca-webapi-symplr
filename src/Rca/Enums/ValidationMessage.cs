// <copyright file="ValidationMessage.cs" company="symplr">
// Copyright © 2022 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Enums
{
    public enum ValidationMessage
    {
        /// <summary>
        /// Incident Date must be current or past date.
        /// </summary>
        INCIDENT_DATE_MUST_NOT_BE_FUTURE_DATE,

        /// <summary>
        /// RCA Name is required.
        /// </summary>
        RCA_NAME_IS_REQUIRED,

        /// <summary>
        /// Problem Statement is required.
        /// </summary>
        PROBLEM_STATEMENT_IS_REQUIRED,

        /// <summary>
        /// {field} Length cannot be more than {length}.
        /// </summary>
        VALIDATION_MAXLENGTH,

        /// <summary>
        /// Cause id is invalid.
        /// </summary>
        CAUSE_ID_INVALID,

        /// <summary>
        /// Cause id does not exist.
        /// </summary>
        CAUSE_ID_DOES_NOT_EXIST,

        /// <summary>
        /// RCA id is required.
        /// </summary>
        RCA_ID_REQUIRED,

        /// <summary>
        /// RCA id is invalid.
        /// </summary>
        RCA_ID_INVALID,

        /// <summary>
        /// Classification id is invalid.
        /// </summary>
        CLASSIFICATION_ID_INVALID,

        /// <summary>
        /// Parent cause id is invalid.
        /// </summary>
        PARENT_CAUSE_ID_INVALID,

        /// <summary>
        /// RCA id does not exist.
        /// </summary>
        RCA_ID_DOES_NOT_EXIST,

        /// <summary>
        /// RCA id is not matching with parameter rcaId.
        /// </summary>
        RCA_ID_UNIQUE_WITHIN_PARAMETERS,

        /// <summary>
        /// RCA id and description are required for creating a cause.
        /// </summary>
        CANNOT_CREATE_CAUSE_WITHOUT_REQUIRED_DATA,

        /// <summary>
        /// No records found.
        /// </summary>
        RCA_DATA_NOT_FOUND,

        /// <summary>
        /// Classification id does not exist.
        /// </summary>
        CLASSIFICATION_ID_DOES_NOT_EXIST,

        /// <summary>
        /// Incident Date is not provided.
        /// </summary>
        INCIDENT_DATE_NOT_PROVIDED,

        /// <summary>
        /// The following itemids are not permissible: {0} .
        /// </summary>
        INVALID_ITEMS,

        /// <summary>
        /// Request body cannot be null.
        /// </summary>
        CREATE_RCA_MODEL_NULL,

        /// <summary>
        /// Root Cause Analysis created successfully.
        /// </summary>
        CREATE_RCA_SUCCESSFUL_MESSAGE,

        /// <summary>
        /// ParticipantId cannot be negative.
        /// </summary>
        PARTICIPANT_ID_GREATER_THAN_EQUAL_ZERO,

        /// <summary>
        /// ParticipantId should be greater than zero when ParticipantName is blank.
        /// </summary>
        PARTICIPANT_ID_MISSING_WHEN_PARTICIPANTNAME_EMPTY,

        /// <summary>
        /// ParticipantName is required when ParticipantId is zero.
        /// </summary>
        PARTICIPANT_NAME_IS_REQUIRED,

        /// <summary>
        /// No matching participantId found for {0}.
        /// </summary>
        INVALID_PARTICIPANTS,

        /// <summary>
        /// Matching participantId found but incorrect participantName for {0}.
        /// </summary>
        INVALID_EXISTING_PARTICIPANTS,

        /// <summary>
        /// Incident date should be valid UTC format.
        /// </summary>
        INCIDENT_DATE_INVALID_UTC_FORMAT,

        /// <summary>
        /// Duplicate participants are not allowed.
        /// </summary>
        DUPLICATE_PARTICIPANT_NOT_ALLOWED,

        /// <summary>
        /// Duplicate items are not allowed.
        /// </summary>
        DUPLICATE_ITEMS_NOT_ALLOWED,
    }
}
