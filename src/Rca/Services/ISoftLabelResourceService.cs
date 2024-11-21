// <copyright file="ISoftLabelResourceService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Rca.Enums;

namespace Rca.Services
{
    /// <summary>
    /// Interface for work with Multilingual component.
    /// </summary>
    public interface ISoftLabelResourceService
    {
        /// <summary>
        /// Initialize multilingual component with soft labels.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Get validation message soft label for document management application.
        /// </summary>
        /// <param name="resource">Validation message soft label resource.</param>
        /// <param name="args">Args.</param>
        /// <returns>Soft label value.</returns>
        string GetSoftLabel(ValidationMessage resource, params object[] args);
    }
}
