using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventReceiver();
    static Dictionary<string, EventReceiver> _events;

    public delegate void LocationEventReceiver(Model v);
    static Dictionary<string, LocationEventReceiver> _locEvents;

    public static void SubscribeToEvent(string eventType, EventReceiver listener)
    {
        if (_events == null) _events = new Dictionary<string, EventReceiver>();
        if (!_events.ContainsKey(eventType)) _events.Add(eventType, null);
        _events[eventType] += listener;
    }

    public static void SubscribeToLocationEvent(string eventType, LocationEventReceiver listener)
    {
        if (_locEvents == null) _locEvents = new Dictionary<string, LocationEventReceiver>();
        if (!_locEvents.ContainsKey(eventType)) _locEvents.Add(eventType, null);
        _locEvents[eventType] += listener;
    }

    public static void UnsubscribeToEvent(string eventType, EventReceiver listener)
    {
        if (_events != null)
        {
            if (_events.ContainsKey(eventType))
            {
                _events[eventType] -= listener;
            }
        }
    }

    public static void UnsubscribeToLocationEvent(string eventType, LocationEventReceiver listener)
    {
        if (_locEvents != null)
        {
            if( _locEvents.ContainsKey(eventType))
            _locEvents[eventType] -= listener;
        }
    }

    public static void ClearCertainKeyEvents(string s)
    {
        if (_events.ContainsKey(s))
            _events[s] = null;
    }

    public static void TriggerEvent(string s)
    {
        if (_events == null) _locEvents = new Dictionary<string, LocationEventReceiver>();
        if (_events.ContainsKey(s))
        {
            if (_events[s] != null)
                _events[s]?.Invoke();
        }
    }

    public static void TriggerLocEvent(string s, Model v)
    {
        if (_locEvents == null) _locEvents = new Dictionary<string, LocationEventReceiver>();
        if (_locEvents.ContainsKey(s))
        {
            _locEvents[s]?.Invoke(v);
        }
    }
}