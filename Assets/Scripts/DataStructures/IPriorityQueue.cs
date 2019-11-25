using UnityEngine;
using System.Collections;

public interface IPriorityQueue<T, P>
{
    /// Inserts and item with a priority
    void Insert(T item, P prio);

    /// Returns the element with the highest priority
    T Top();

    /// Deletes and returns the element with the highest priority
    T Pop();
}