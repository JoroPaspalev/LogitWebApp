using LogitWebApp.Attributes.ModelValidationAttributes;
using Xunit;

namespace LogitWebApp.Tests
{
    public class BornAfterAttributeTests
    {
        [Fact]
        public void IsValidReturnFalseForYearLessGiven()
        {
            //Arrange
            var attribute = new BornAfterAttribute(1983);

            //Act - Роден лие след 1983г?
            bool result = attribute.IsValid(1982);

            //Assert - Не, не е роден след 1983. Затова резултата е false
            Assert.False(result);
        }

        [Fact]
        public void IsValidReturnTrueForYearAfterGiven()
        {
            var attr = new BornAfterAttribute(1983);

            var result = attr.IsValid(2020);

            Assert.True(result);
        }
        

    }
}
