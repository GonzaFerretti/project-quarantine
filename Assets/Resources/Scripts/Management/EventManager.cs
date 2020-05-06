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

    public static void ClearAllEvents()
    {
        if (_events != null)
            _events.Clear();
    }

    public static void ClearAllLocEvents()
    {
        if (_locEvents != null)
            _locEvents.Clear();
    }

    public static void TriggerEvent(string s)
    {
        if (_events == null) _locEvents = new Dictionary<string, LocationEventReceiver>();
        if (_events.ContainsKey(s))
        {
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
