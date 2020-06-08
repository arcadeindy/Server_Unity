using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    //class Lock
    //{
    //    //AutoResetEvent _available = new AutoResetEvent(true);
    //    ManualResetEvent _available = new ManualResetEvent(true);

    //    public void Acquire()
    //    {
    //        _available.WaitOne(); // 입장 시도
    //        _available.Reset(); // 문을 닫음
    //    }

    //    public void Release()
    //    {
    //        _available.Set();
    //    }
    //}

    class Program
    {
        static int _num = 0;
        static Mutex _lock = new Mutex();

        static void Thread_1()
        {
            for (int i = 0; i < 100000; i++)
            {
                _lock.WaitOne();
                _num++;
                _lock.ReleaseMutex();
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 100000; i++)
            {
                _lock.WaitOne();
                _num--;
                _lock.ReleaseMutex();
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(_num);
        }
    }
}
