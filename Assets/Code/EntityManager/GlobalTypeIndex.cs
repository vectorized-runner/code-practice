using System.Threading;

namespace CodePractice
{
    public static class GlobalTypeIndex
    {
        private static int _value;

        public static int GetUniqueValue()
        {
            return Interlocked.Increment(ref _value);
        }
    }
}