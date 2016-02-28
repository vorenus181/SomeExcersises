using System;
using System.Linq;

namespace SparseArray
{
    class Program
    {
        static void Main(string[] args)
        {
            SparseArray<int, string> array = new SparseArray<int, string>(string.Empty);

            for (int i = 0; i < 1000; i++)
            {
                array[i * 10] = GetRandomString(5);
            }

            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine("Key: {0}; Value: {1}", i, array[i]);
            }

            Console.ReadKey();
        }

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
