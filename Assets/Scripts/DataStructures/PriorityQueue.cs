using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FibonacciHeap;
using System;

public class PriorityQueue<TElement, TPriority> : IPriorityQueue<TElement, TPriority>
    where TPriority : IComparable<TPriority>
{
    private readonly FibonacciHeap<TElement, TPriority> heap;

    public PriorityQueue(TPriority minPriority)
    {
        heap = new FibonacciHeap<TElement, TPriority>(minPriority);
    }

    public void Insert(TElement item, TPriority priority)
    {
        heap.Insert(new FibonacciHeapNode<TElement, TPriority>(item, priority));
    }

    public TElement Top()
    {
        if (heap.Min() != null)
            return heap.Min().Data;
        else return default(TElement);
    }

    public TPriority TopPriority()
    {
        if (heap.Min() != null)
            return heap.Min().Key;
        else return default(TPriority);
    }

    public TElement Pop()
    {
        return heap.RemoveMin().Data;
    }
}
