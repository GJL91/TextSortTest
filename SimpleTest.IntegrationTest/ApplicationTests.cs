using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTest.Services;
using SimpleTest.Utils;

namespace SimpleTest.IntegrationTest
{
    public class Tests
    {
        private Application _sut;

        [SetUp]
        public void Setup()
        {
            var analysisService = new AnalysisService(new Mock<ILogger<AnalysisService>>().Object, new WordComparer());
            _sut = new Application(new Mock<ILogger<Application>>().Object, analysisService);
        }

        [TestCase("Zoom Boom", "Boom Zoom", TestName = "WordSort")]
        [TestCase("boom Boom", "Boom boom", TestName = "CaseSort")]
        [TestCase("b, b", "b b", TestName = "RemoveInvalidChars")]
        [TestCase("Go baby, go", "baby Go go", TestName = "SimpleTest1")]
        [TestCase("CBA, abc aBc ABC cba CBA.", "ABC aBc abc CBA CBA cba", TestName = "SimpleTest2")]
        public void Run_ReturnsExpected(string input, string expected)
        {
            // Arrange
            var args = Regex.Split(input, "\\s");

            // Act
            var actual = _sut.Run(args);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
