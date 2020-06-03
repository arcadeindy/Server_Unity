using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        volatile static bool _stop = false;

        static void ThreadMain()
        {
            Console.WriteLine("Thread Start");
            
            while (_stop == false)
            {

            }

            Console.WriteLine("Thread Exit");
        }

        static void Main(string[] args)
        {
            Task t = new Task(ThreadMain);
            t.Start();

            Thread.Sleep(1000);

            _stop = true;

            Console.WriteLine("Call Stop");
            Console.WriteLine("Waiting for exit");
            
            t.Wait();

            Console.WriteLine("Exit");
        }
    }
}
