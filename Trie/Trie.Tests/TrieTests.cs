using Trie;

namespace Trie.Tests;

public class Tests
{
    public string[] testcases = new string[20];
    Trie trie;
    [SetUp]
    public void Setup()
    {
        trie = new();
        testcases[0] = "he";
        testcases[1] = "she";
        testcases[2] = "his";
        testcases[3] = "her";
    }

    [Test]
    public void AddTest()
    {
        bool IsPassed = true;
        for (int i = 0; i < 4; i++)
        {
            if (!trie.Add(testcases[i]))
            {
                IsPassed = false;
            }
        }
        Assert.That(IsPassed);
    }

    [Test]
    public void TwiceAddTest()
    {
        for (int i = 0; i < 4; i++)
        {
            trie.Add(testcases[i]);
        }
        bool IsPassed = true;
        for (int i = 0; i < 4; i++)
        {
            if (trie.Add(testcases[i]))
            {
                IsPassed = false;
            }
        }
        Assert.That(IsPassed);
    }

    [Test]
    public void ContainTest()
    {
        for (int i = 0; i < 4; i++)
        {
            trie.Add(testcases[i]);
        }
        bool IsPassed = true;
        for (int i = 0; i < 4; i++)
        {
            if (!trie.Contain(testcases[i]))
            {
                IsPassed = false;
            }
        }
        Assert.That(IsPassed);
    }
    [Test]
    public void PrefixCountTest()
    {
        for (int i = 0; i < 4; i++)
        {
            trie.Add(testcases[i]);
        }
        Assert.That(trie.HowManyStartsWithPrefix("s") == 1 && trie.HowManyStartsWithPrefix("h") == 3);
    }
    [Test]
    public void RemoveTest()
    {
        for (int i = 0; i < 4; i++)
        {
            trie.Add(testcases[i]);
        }
        Assert.That(trie.Remove("he") && trie.HowManyStartsWithPrefix("h") == 2);
    }
    [Test]
    public void RemoveTest2()
    {
        for (int i = 0; i < 4; i++)
        {
            trie.Add(testcases[i]);
        }
        Assert.That(trie.Remove("she") && trie.HowManyStartsWithPrefix("s") == 0);
    }
    [Test]
    public void AllSymbolsTest()
    {
        Assert.That(trie.Add("`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./~!@#$%^&*()_QWERTYUIOP{}|ASDFGHJKL:\"ZXCVBNM<>?йцукенгшщзхъфывапролджэячсмитьбю.ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ,"));
    }
    [Test]
    public void RemoveTest3()
    {
        for (int i = 0; i < 4; i++)
        {
            trie.Add(testcases[i]);
        }
        trie.Remove("he");
        Assert.That(trie.Contain("her"));
    }
}
