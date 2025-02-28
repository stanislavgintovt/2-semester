using System.Diagnostics.Tracing;

namespace BurrowsWheeler.Tests;

public class Tests
{
    [TestCase("")] // empty string
    [TestCase("a")]
    [TestCase("banana")]
    [TestCase(null)]
    [TestCase("abracadabra")]
    [TestCase("So long string that I hope it will crash the program")]
    [TestCase("Soвыа long stфыriа!ы@вn4#g ^t2at%вы I' hвы:ope it ^wiывl234l c?rasывh/ tпheпы ыавы program но еsdasщё с русски\nми буквами и спец сим\"волами")]
    public void IsReverseBWTReturnsSourse(string? source)
    {
        (string res, int index) = Transformation.BurrowWheelerTransformation(source);

        Assert.That(Transformation.ReverseBurrowWheelerTransformation(res, index), Is.EqualTo(source));
    }
}
