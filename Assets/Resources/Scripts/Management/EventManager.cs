using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventReceiver();
    static Dictionary<string, EventReceiver> _events;
   
    public static void SubscribeToEvent(string eventType, EventReceiver listener)
    {
        if (_events == null) _events = new Dictionary<string, EventReceiver>();
        if (!_events.ContainsKey(eventType)) _events.Add(eventType, null);
        _events[eventType] += listener;
    }

    public static void ClearAllEvents()
    {
        if (_events != null)
            _events.Clear();
    }

    public static void TriggerEvent(string s)
    {
        if(_events.ContainsKey(s))
        {
            if (_events[s] != null)
                _events[s]();
        }
    }
}
