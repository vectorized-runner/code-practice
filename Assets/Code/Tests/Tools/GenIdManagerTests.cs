using NUnit.Framework;

namespace Code.Tests
{
    public class GenIdManagerTests
    {
        // Work with Index & Version 
        // Default Index = 0, Version for each index is 0
        // Version is incremented once that index is destroyed
        // Exists -> Index >= LastUsedIndex
        // No Exception on Double dispose

        [Test]
        public static void TestEqual_0()
        {
            for (int i = -100; i < 100; i++)
            {
                for (int j = -100; j < 100; j++)
                {
                    Assert.AreEqual(new GenId(i, j), new GenId(i, j));
                }
            }
        }

        [Test]
        public static void TestEqual_1()
        {
            for (int i = -100; i < 100; i++)
            {
                for (int j = -100; j < 100; j++)
                {
                    if (i != j)
                    {
                        Assert.AreNotEqual(new GenId(i, j), new GenId(j, i));
                    }
                }
            }
        }
    }
}