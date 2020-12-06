
using LogitWebApp.Attributes.ModelValidationAttributes;
using System;
using Xunit;

namespace LogitWebApp.Tests.AttributesTest
{
    public class ValidateCurrentDateAttributeTests
    {
        [Fact]
        public void IsValidReturnTrueIfGivenDateIsInFuture()
        {
            var instance = new ValidateCurrentDateAttribute();

            bool result = instance.IsValid(DateTime.UtcNow.AddDays(1));

            Assert.True(result);
        }

        [Fact]
        public void IsValidReturnFalseIfGivenDateIsInPast()
        {
            var instance = new ValidateCurrentDateAttribute();

            bool result = instance.IsValid(DateTime.UtcNow);

            Assert.False(result);
        }

        [Fact]
        public void IsValidReturnFalseIfGivenDateIsNull()
        {
            var instance = new ValidateCurrentDateAttribute();

            bool result = instance.IsValid(null);

            Assert.False(result);
        }
    }
}
