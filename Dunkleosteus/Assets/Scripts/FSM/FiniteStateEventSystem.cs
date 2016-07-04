using System;
using System.Collections;
using System.Collections.Generic;

namespace FiniteStateEventSystem
{

public class Dispatcher 
{

    class Listen
    {
        public int listenID;
        public Func<object, object, object, bool> action;
    }

    class Dispatched
    {
        public string eventName;
        public object arg1, arg2, arg3;
        public Dispatched Set(string name, object o1 = null, object o2 = null, object o3 = null)
        {
            eventName = name;
            arg1 = o1;
            arg2 = o2;
            arg3 = o3;
            return this;
        }
    }

    Dictionary<string, List<int>> registeredEvents = new Dictionary<string, List<int>>();
    Dictionary<int, Listen> registeredMap = new Dictionary<int, Listen>();
    Stack<Listen> freeListen = new Stack<Listen>();
    Stack<Dispatched> freeDispatch = new Stack<Dispatched>();
    Queue<Dispatched> dispatchedQueue = new Queue<Dispatched>():
    int nextListenId = 4711;

    public void Trigger(string eventName) 
    {
        Call(eventName, null, null, null);
    }

    public void Dispatch(string eventName) 
    {
        Dispatched d = (freeDispatch.Count == 0) ? new Dispatched() : freeDispatch.Pop();
        dispatchedQueue.Enqueue(d.Set(eventName));
    }

    public int On(string eventName, Func<bool> action)
    {
        return Register(eventName, delegate(object o1, object o2, object o3) {
            return (action());
            }
        );
    }

    public int On(string eventName, Action action)
    {
        return Register(eventName, delegate(object o1, object o2, object o3) {
            action();
            return true;
            }
        );
    }

    public void Trigger(string eventName, object o1) 
    {
        Call(eventName, o1, null, null);
    }

    public void Dispatch(string eventName, object o1) 
    {
        Dispatched d = (freeDispatch.Count == 0) ? new Dispatched() : freeDispatch.Pop();
        dispatchedQueue.Enqueue(d.Set(eventName, o1));
    }

    public int On<T>(string eventName, Func<bool> action)
    {
        return Register(eventName, delegate(object o1, object o2, object o3) {
            T param;
            try {
                param = (T)o1;
            }
            catch {
                param = default(T);
            }
            return (action(param));
            }
        );
    }

    public int On<T>(string eventName, Action action)
    {
        return Register(eventName, delegate(object o1, object o2, object o3) {
            T param;
            try {
                param = (T)o1;
            }
            catch {
                param = default(T);
            }
            action(param);
            return true;
            }
        );
    }

    public void Trigger(string eventName, object o1, object o2) 
    {
        Call(eventName, o1, o2, null);
    }

    public void Dispatch(string eventName, object o1, object o2) 
    {
        Dispatched d = (freeDispatch.Count == 0) ? new Dispatched() : freeDispatch.Pop();
        dispatchedQueue.Enqueue(d.Set(eventName, o1, o2));
    }

    public int On<T>(string eventName, Func<bool> action)
    {
        return Register(eventName, delegate(object o1, object o2, object o3) {
            T1 param1;
            T2 param2;
            try {
                param1 = (T1)o1;
            }
            catch {
                param1 = default(T1);
            }
            try {
                param2 = (T2)o2;
            }
            catch {
                param2 = default(T2);
            }
            return (action(param1, param2));
            }
        );
    }

    public int On<T>(string eventName, Action action)
    {
        return Register(eventName, delegate(object o1, object o2, object o3) {
            T1 param1;
            T2 param2;
            try {
                param1 = (T1)o1;
            }
            catch {
                param1 = default(T1);
            }
            try {
                param2 = (T2)o2;
            }
            catch {
                param2 = default(T2);
            }
            action(param1, param2);
            return true;
            }
        );
    }

    public void Trigger(string eventName, object o1, object o2, object o3) 
    {
        Call(eventName, o1, o2, o3);
    }

    public void Dispatch(string eventName, object o1, object o2, object o3) 
    {
        Dispatched d = (freeDispatch.Count == 0) ? new Dispatched() : freeDispatch.Pop();
        dispatchedQueue.Enqueue(d.Set(eventName, o1, o2, o3));
    }

    public int On<T>(string eventName, Func<bool> action)
    {
        return Register(eventName, delegate(object o1, object o2, object o3) {
            T1 param1;
            T2 param2;
            T3 param3;
            try {
                param1 = (T1)o1;
            }
            catch {
                param1 = default(T1);
            }
            try {
                param2 = (T2)o2;
            }
            catch {
                param2 = default(T2);
            }
            try {
                param3 = (T3)o3;
            }
            catch {
                param3 = default(T3);
            }
            return (action(param1, param2, param3));
            }
        );
    }

    public int On<T>(string eventName, Action action)
    {
        return Register(eventName, delegate(object o1, object o2, object o3) {
            T1 param1;
            T2 param2;
            T3 param3;
            try {
                param1 = (T1)o1;
            }
            catch {
                param1 = default(T1);
            }
            try {
                param2 = (T2)o2;
            }
            catch {
                param2 = default(T2);
            }
            try {
                param3 = (T3)o3;
            }
            catch {
                param3 = default(T3);
            }
            action(param1, param2, param3);
            return true;
            }
        );
    }

    public bool Cancel(int listenerID)
    {
        return registeredMap.Remove(listenerID);
    }

    public void DispatchPending()
    {
        while(dispatchedQueue.Count > 0) {
            Dispatched d = dispatchedQueue.Dequeue();
            Call(d.eventName, d.arg1, d.arg2, d.arg3);
            freeDispatch.Push(d);
        }
    }

    int Register(string eventName, Func<object, object, object, bool> action)
    {

    }

    void Call(string eventName, object o1, object o2, object o3)
    {

    }
}





}
