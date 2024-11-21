// <copyright file="RcaDbContextTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Orion.Tests.EntityFrameworkHelpers;
using Rca.Data;
using Xunit;

namespace Rca.Tests.Data
{
    public class RcaDbContextTests
    {
        [Fact]
        public void TestRegisteredTypes()
        {
            using (var sut = new RcaDbContext("SomeConnectionString"))
            {
                DbContextTestHelper.CheckRegisteredEntityTypes(sut, "Rca.Data.Entities");
            }
        }
    }
}
