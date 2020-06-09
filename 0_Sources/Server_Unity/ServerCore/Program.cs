using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        // 1. 존버
        // 2. 양보
        // 3. 갑질

        // 상호배제
        // Monitor
        static object _lock = new object();
        static SpinLock _lock2 = new SpinLock();
        // or 직구현

        static ReaderWriterLockSlim _lock3 = new ReaderWriterLockSlim();

        class Reward
        {

        }

        

        static Reward GetRewardById(int id)
        {
            _lock3.EnterReadLock();

            _lock3.ExitReadLock();

            lock (_lock)
            {

            }
            return null;
        }

        static void AddReward(Reward reward)
        {
            _lock3.EnterWriteLock();

            _lock3.ExitWriteLock();

            lock (_lock)
            {

            }
        }

        static void Main(string[] args)
        {
            lock (_lock)
            {

            }

            bool lockTaken = false;
            try
            {
                _lock2.Enter(ref lockTaken);
            }
            finally
            {
                if (lockTaken)
                    _lock2.Exit();
            }

            
        }
    }
}
