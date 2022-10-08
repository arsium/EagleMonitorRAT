using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Misc
{
    internal class RandomString
    {
        //private const string randomString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789abcdefghijklmnopqrstuvwxyz|@#^{}[]`´~";
        public static string randomString = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψωABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzابتثجحخدذرزسشصضطظعغفقكلمنهويابتثجحخدذرزسشصضطظعغفقكلمنهوي艾诶比西迪伊弗吉尺杰开勒马娜哦屁吉吾儿丝提伊吾维豆贝尔维克斯吾贼德אבגדהוזחטיכךלמםנןסעפףצץקרשתאבגדהוזחטיכךלמםנןסעפףצץקרשת";
        //private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //public static string chars = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψωABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzابتثجحخدذرزسشصضطظعغفقكلمنهويابتثجحخدذرزسشصضطظعغفقكلمنهوي0123456789艾诶比西迪伊弗吉尺杰开勒马娜哦屁吉吾儿丝提伊吾维豆贝尔维克斯吾贼德אבגדהוזחטיכךלמםנןסעפףצץקרשתאבגדהוזחטיכךלמםנןסעפףצץקרשת";


        private static readonly List<string> names = new List<string>();

        private static readonly Random random = new Random();
        internal static string NextString(int length) => new string((
            from _ in Enumerable.Range(0, length)
            let i = random.Next(0, RandomString.randomString.Length)
            select RandomString.randomString[i]).ToArray());


        internal static int RandomDigit() 
        {
            return random.Next(1, 10);
        }

        public static string RandomStringFunction(int length)
        {
            names.Clear();
            string name = "";
            do
            {
                name = new string(Enumerable.Repeat(randomString, length).Select(s => s[random.Next(s.Length)]).ToArray());
            } while (names.Contains(name));

            return name;
        }

        public static string GenerateRandomString()
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= random.Next(10, 20); i++)
            {
                var randomCharacterPosition = random.Next(0, randomString.Length);
                sb.Append(randomString[randomCharacterPosition]);
            }
            return sb.ToString();
        }
    }
}
