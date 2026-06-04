using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, Delegate> subscribers = new();

    public static void Subscribe<T>(Action<T> listener)
    {
        Type eventType = typeof(T);

        if (subscribers.TryGetValue(eventType, out Delegate existingDelegate))
        {
            subscribers[eventType] = Delegate.Combine(existingDelegate, listener);
        }
        else
        {
            subscribers[eventType] = listener;
        }
    }

    public static void Unsubscribe<T>(Action<T> listener)
    {
        Type eventType = typeof(T);

        if (!subscribers.TryGetValue(eventType, out Delegate existingDelegate))
        {
            return;
        }

        Delegate newDelegate = Delegate.Remove(existingDelegate, listener);

        if (newDelegate == null)
        {
            subscribers.Remove(eventType);
        }
        else
        {
            subscribers[eventType] = newDelegate;
        }
    }

    public static void Publish<T>(T eventData)
    {
        Type eventType = typeof(T);

        if (subscribers.TryGetValue(eventType, out Delegate existingDelegate))
        {
            ((Action<T>)existingDelegate)?.Invoke(eventData);
        }
    }

    public static void Clear() => subscribers.Clear();
}