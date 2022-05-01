using System;
using System.Linq;

namespace Eagle_Monitor_Builder
{
    internal class RandomString
    {
        private static string randomString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789abcdefghijklmnopqrstuvwxyz|@#^{}[]`´~";
        private static readonly Random random = new Random();
        internal static string NextString(int length) => new string((
            from _ in Enumerable.Range(0, length)
            let i = random.Next(0, RandomString.randomString.Length)
            select RandomString.randomString[i]).ToArray());
    }
}
