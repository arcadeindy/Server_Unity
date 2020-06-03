﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        static int number = 0;

        static void Thread_1()
        {
            // 원자성
            for (int i = 0; i < 10000; i++)
            {
                Interlocked.Increment(ref number);

                //int temp = number;
                //temp += 1;
                //number = temp;
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 10000; i++)
            {
                Interlocked.Decrement(ref number);

                //int temp = number;
                //temp -= 1;
                //number = temp;
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);
        }
    }
}
