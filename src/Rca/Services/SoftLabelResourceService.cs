// <copyright file="SoftLabelResourceService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Orion.Multilingual;
using Orion.Multilingual.Enums;
using Orion.Users;
using Rca.Enums;

namespace Rca.Services
{
    /// <summary>
    /// Service which initializes soft labels for the application and gets label values of different label types.
    /// </summary>
    public class SoftLabelResourceService : ISoftLabelResourceService
    {
        private readonly IMultilingualComponent _multilingualComponent;
        private readonly IUsersComponent _usersComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftLabelResourceService"/> class.
        /// </summary>
        /// <param name="multilingualComponent">Multilingual component.</param>
        /// <param name="usersComponent">Users component.</param>
        public SoftLabelResourceService(IMultilingualComponent multilingualComponent, IUsersComponent usersComponent)
        {
            _multilingualComponent = multilingualComponent;
            _usersComponent = usersComponent;
        }

        /// <summary>
        /// Initialize soft labels using multilingual component.
        /// </summary>
        public void Initialize()
        {
            _multilingualComponent.Initialize();
        }

        /// <summary>
        /// Get validation message soft label.
        /// </summary>
        /// <param name="resource">Validation message soft label resource.</param>
        /// <param name="args">Args.</param>
        /// <returns>Soft label value.</returns>
        public string GetSoftLabel(ValidationMessage resource, params object[] args)
        {
            var language = _usersComponent.CurrentUser.Language;
            return _multilingualComponent.GetSoftLabel(resource.ToString(), ResourceType.ValidationMessage, language, args);
        }
    }
}
