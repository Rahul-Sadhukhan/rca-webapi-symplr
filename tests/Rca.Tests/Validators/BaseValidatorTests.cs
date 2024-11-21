// <copyright file="BaseValidatorTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Moq;
using Rca.Enums;
using Rca.Services;

namespace Srm.Tests.Validators
{
    public abstract class BaseValidatorTests
    {
        protected readonly Mock<ISoftLabelResourceService> _softLabelResourceServiceMock;

        protected BaseValidatorTests()
        {
            _softLabelResourceServiceMock = new Mock<ISoftLabelResourceService>();

            _softLabelResourceServiceMock
                .Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>(), It.IsAny<object[]>()))
                .Returns((ValidationMessage msg, object[] args) => msg.ToString());
        }
    }
}
