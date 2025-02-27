using System.Diagnostics.Tracing;

namespace BurrowsWheeler.Tests;

public class Tests
{
    private Transformation _transform;

    [SetUp]
    public void Setup()
    {
        _transform = new();
    }

    [Test]
    public void IsReverseBWTReturnsSourse()
    {
        string source = "banana";

        (string res, int index) = Transformation.BurrowWheelerTransformation(source);

        Assert.That(Transformation.ReverseBurrowWheelerTransformation(res, index), Is.EqualTo(source));
    }
}
