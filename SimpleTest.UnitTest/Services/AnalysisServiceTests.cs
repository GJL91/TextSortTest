using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTest.Services;
using SimpleTest.Utils;

namespace SimpleTest.UnitTest.Services
{
    [TestFixture]
    public class AnalysisServiceTests
    {
        private AnalysisService _sut;

        private Mock<ILogger<AnalysisService>> _logger;
        private Mock<IWordComparer> _wordComparer;

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger<AnalysisService>>();
            _wordComparer = new Mock<IWordComparer>(MockBehavior.Strict);

            _sut = new AnalysisService(_logger.Object, _wordComparer.Object);
        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("\t")]
        public void Analyse_ThrowsException_WhenInputNullOrWhitespace(string input)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Analyse(input));
        }

        [TestCase("Boom Zoom", "Boom Zoom")]
        [TestCase("Boom, Zoom", "Boom Zoom")]
        [TestCase("Boom,.;'Zoom", "BoomZoom")]
        public void Analyse_ReturnsExpected(string input, string expected)
        {
            // Arrange
            var punctuationRegex = new Regex("[.,;']");
            var inputWords = Regex.Split(punctuationRegex.Replace(input, ""), "\\s");
            for (var i = 0; i < inputWords.Length - 1; i++)
            {
                var i1 = i;
                _wordComparer.Setup(x => x.Compare(inputWords[i1], inputWords[i1 + 1])).Returns(0);
            }

            // Act
            var actual = _sut.Analyse(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

            _wordComparer.VerifyAll();
        }
    }
}
