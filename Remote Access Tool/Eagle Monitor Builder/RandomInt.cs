using System;
using System.Linq;

namespace Eagle_Monitor_Builder
{
    internal class RandomInt
    {
        private static string randomString = "1234567890";
        private static readonly Random random = new Random();
        internal static string NextInt(int length) => new string((
            from _ in Enumerable.Range(0, length)
            let i = random.Next(0, RandomInt.randomString.Length)
            select RandomInt.randomString[i]).ToArray());
    }
}
