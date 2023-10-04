using SimpleTest.Utils;

namespace SimpleTest.UnitTest.Utils;

[TestFixture]
public class WordComparerTests
{
    private WordComparer _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new WordComparer();
    }

    [TestCase(null, null, 0)]
    [TestCase(null, "foo", -1)]
    [TestCase("foo", null, 1)]
    [TestCase("abc", "abc", 0)]
    [TestCase("Abc", "abc", -1)]
    [TestCase("abc", "Abc", 1)]
    [TestCase("abc", "cba", -1)]
    [TestCase("abc", "Cba", -1)]
    [TestCase("Abc", "cba", -1)]
    [TestCase("Abc", "Cba", -1)]
    [TestCase("abc", "aBc", 1)]
    [TestCase("aBc", "abc", -1)]
    [TestCase("aBc", "aBc", 0)]
    [TestCase("ABC", "ABC", 0)]
    [TestCase("ABC", "ABc", -1)]
    [TestCase("abc", "abcd", -1)]
    [TestCase("abc", "abcD", -1)]
    public void Compare_ReturnsExpected(string? x, string? y, int expected)
    {
        // Act
        var actual = _sut.Compare(x, y);

        // Assert
        switch (expected)
        {
            case 0:
                Assert.That(actual, Is.EqualTo(expected));
                break;
            case 1:
                Assert.That(actual, Is.GreaterThan(0));
                break;
            case -1:
                Assert.That(actual, Is.LessThan(0));
                break;
        }
    }
}
