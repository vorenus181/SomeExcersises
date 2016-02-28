using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedQueue<int> queue = new SortedQueue<int>(20);

            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                queue.Enqueue(random.Next(1, 10));
            }

            foreach (var value in queue)
            {
                Console.WriteLine("value: {0}", value);
            }

            queue.Enqueue(10);
            queue.Enqueue(9);
            queue.Enqueue(8);

            Console.WriteLine("After additional adds");
            foreach (var value in queue)
            {
                Console.WriteLine("value: {0}", value);
            }

            Console.WriteLine("Get max element");
            Console.Write("Max element: {0}", queue.DequeueMaxElement());
            Console.Write("Max element: {0}", queue.DequeueMaxElement());
            Console.Write("Max element: {0}", queue.DequeueMaxElement());
            Console.Write("Max element: {0}", queue.DequeueMaxElement());

            Console.ReadKey();
        }
    }
}
