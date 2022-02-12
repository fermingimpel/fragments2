using UnityEngine;

namespace CustomUtils
{
    public class IdGenerator
    {
        private static string[] alhpanumeric =
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
            "u", "v", "w","x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };

        public static string GenerateId()
        {
            string id = "";

            for (int i = 0; i < 5; i++)
            {
                id += alhpanumeric[Random.Range(0, alhpanumeric.Length)];
            }

            return id;
        }
    }
}
