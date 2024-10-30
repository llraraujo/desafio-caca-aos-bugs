using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.Test.AccountContext.ValueObjects.MockObjects
{
    public class MockDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}
