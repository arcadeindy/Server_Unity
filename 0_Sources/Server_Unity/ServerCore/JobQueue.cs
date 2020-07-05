using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerCore
{
    // 각각의 클래스 방식으로의 큐 구현
    //interface ITask
    //{
    //    void Execute();
    //}

    //class BroadcastTask : ITask
    //{
    //    GameRoom _room;
    //    ClientSession _session;
    //    string _chat;

    //    BroadcastTask(GameRoom room, ClientSession session, string chat)
    //    {
    //        _room = room;
    //        _session = session;
    //        _chat = chat;
    //    }
    //    public void Execute()
    //    {
    //        _room.Broadcast(_session, _chat);
    //    }
    //}

    //class TaskQueue
    //{
    //    Queue<ITask> _queue = new Queue<ITask>();
    //}

    public interface IJobQueue
    {
        void Push(Action job);
    }

    public class JobQueue : IJobQueue
    {
        Queue<Action> _jobQueue = new Queue<Action>();
        object _lock = new object();
        bool _flush = false;

        public void Push(Action job)
        {
            bool flush = false;

            lock (_lock)
            {
                _jobQueue.Enqueue(job);

                if (_flush == false)
                    flush = _flush = true;
            }

            if (flush)
                Flush();
        }

        void Flush()
        {
            while (true)
            {
                Action action = Pop();
                if (action == null)
                    return;

                action.Invoke();
            }
        }

        Action Pop()
        {
            lock (_lock)
            {
                if (_jobQueue.Count == 0)
                {
                    _flush = false;
                    return null;
                }

                return _jobQueue.Dequeue();
            }
        }
    }
}
