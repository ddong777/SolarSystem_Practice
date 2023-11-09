using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager_v5 : MonoBehaviour
{
    public Dictionary<string, UnityAction> events = new Dictionary<string, UnityAction>();

    public void Init()
    {

    }

    public void Set()
    {

    }

    public void SetEvent(string _eventName, UnityAction _action)
    {
        if (events.ContainsKey(_eventName))
        {
            events[_eventName] = _action;
        }
        else
        {
            events.Add(_eventName, _action);
        }
    }

    public void AddEvent(string _eventName, UnityAction _event)
    {
        // 있는 이벤트인지 확인
        if (events.ContainsKey(_eventName))
        {
            events[_eventName] += _event;
        }
        else 
        {
            UnityAction _temp = () => { };
            _temp += _event;

            events.Add(_eventName, _temp);
        }
    }

    public void RemoveEvent(string _eventName, UnityAction _event)
    {
        if (events.ContainsKey(string.Empty))
        {
            events[_eventName] -= _event;
        }
        else
        {
            Debug.Log(_eventName + "은 존재하지 않는 이벤트입니다.");
        }
    }

    public void RemoveEvent(string _eventName)
    {
        if (events.ContainsKey(string.Empty))
        {
            events.Remove(_eventName);
        }
        else
        {
            Debug.Log(_eventName + "은 존재하지 않는 이벤트입니다.");
        }
    }

    public UnityAction GetEvent(string _eventName) {
        if (events.ContainsKey(_eventName))
        {
            return events[_eventName];
        }
        else 
        {
            Debug.Log(_eventName + "은 존재하지 않는 이벤트입니다.");
            return null; 
        }
    }
}
