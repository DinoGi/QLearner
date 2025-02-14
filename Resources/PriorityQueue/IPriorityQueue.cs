﻿// Implementation free by BlueRaja @ Bitbucket

using System.Collections.Generic;

namespace QLearner.Resources.PriorityQueue
{
    /// <summary>
    /// The IPriorityQueue interface.  This is mainly here for purists, and in case I decide to add more implementations later.
    /// For speed purposes, it is actually recommended that you *don't* access the priority queue through this interface, since the JIT can
    /// (theoretically?) optimize method calls from concrete-types slightly better.
    /// </summary>
    public interface IPriorityQueue<T> : IEnumerable<T>
        where T : PriorityQueueNode
    {
        void Remove(T node);
        void UpdatePriority(T node, decimal priority);
        void Enqueue(T node, decimal priority);
        T Dequeue();
        T First { get; }
        int Count { get; }
        int MaxSize { get; }
        void Clear();
        bool Contains(T node);
    }
}