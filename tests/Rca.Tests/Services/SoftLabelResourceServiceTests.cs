// <copyright file="SoftLabelResourceServiceTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using AutoFixture.Xunit2;
using Moq;
using Orion.Multilingual;
using Orion.Multilingual.Enums;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Orion.Users;
using Orion.Users.Models;
using Rca.Enums;
using Rca.Services;
using Xunit;

namespace Rca.Tests.Services
{
    public class SoftLabelResourceServiceTests : BaseTest
    {
        [Theory]
        [AutoMoqData]
        internal void GetSoftLabel_Always_CallsMultilingualComponent(
            [Frozen] Mock<IUsersComponent> usersComponentMock,
            [Frozen] Mock<IMultilingualComponent> multilingualComponentMock,
            SoftLabelResourceService sut,
            ValidationMessage validationMessage,
            OrionUserModel user,
            int param)
        {
            // Arrange
            usersComponentMock
                .Setup(x => x.CurrentUser)
                .Returns(user);

            // Act
            sut.GetSoftLabel(validationMessage, param);

            // Assert
            multilingualComponentMock.Verify(x => x.GetSoftLabel(validationMessage.ToString(), ResourceType.ValidationMessage, user.Language, param), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void Initialize_Always_InitializesMultilingualComponent(
            [Frozen] Mock<IMultilingualComponent> multilingualComponentMock,
            SoftLabelResourceService sut)
        {
            // Act
            sut.Initialize();

            // Assert
            multilingualComponentMock.Verify(x => x.Initialize(), Times.Once);
        }
    }
}
