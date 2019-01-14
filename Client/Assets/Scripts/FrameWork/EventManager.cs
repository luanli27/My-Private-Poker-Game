using System;
using System.Collections.Generic;

class EventManager : Singleton<EventManager> {

    private Dictionary<EventName, Action<object>> _eventHandler = new Dictionary<EventName, Action<object>>();
    public void AddEventListener(EventName eventName, Action<object> handler)
    {
        if (!_eventHandler.ContainsKey(eventName))
            _eventHandler[eventName] = handler;
        else
            _eventHandler[eventName] += handler;
    }

    public void RemoveEventListner(EventName eventName, Action<object> handler)
    {
        if (_eventHandler[eventName] != null)
            _eventHandler[eventName] -= handler;
    }

    public void DispatchEvent(EventName eventName, object msg)
    {
        if (_eventHandler.ContainsKey(eventName))
        {
            _eventHandler[eventName]?.Invoke(msg);
        }
    }
}
