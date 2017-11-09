# DeepAsserts
C# - Assert a result to an expected value and provide the diff

Will compare all your public properties expect for those with `[DeepAssertIgnore]` attribute.

## Example with xUnit

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UEAT.DeepAsserts;
using Xunit;

namespace UnitTest
{
    public class FranchiseRepositoryTests
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory = new SqlConnectionFactory();

        [Fact]
        public async Task ListAsync_ReturnsFranchises()
        {
            var userContext = new UserContext(UserContextTestData.FranchiseHeadquarterApiKey, CultureEnum.FrenchCanadian, null, null);
            var repository = new FranchiseRepository(userContext, _sqlConnectionFactory);
            var franchises = await repository.ListAsync(OrderTypeEnum.Takeout);

            DeepAssert.Equals(FranchiseTestData.FranchisesTakeout, franchises);
        }
    }
}
```
