using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager_v5 : MonoBehaviour
{
    public Dictionary<string, UnityAction> baseEvents = new Dictionary<string, UnityAction>();
    public Dictionary<string, UnityAction> events = new Dictionary<string, UnityAction>();

    private void Awake()
    {
        EventManager_v5[] objs = FindObjectsOfType(typeof(EventManager_v5)) as EventManager_v5[];
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void CurrentEventList()
    {
        foreach (KeyValuePair<string, UnityAction> ev in events)
        {
            Debug.Log($"{ev.Key} - {ev.Value}");
        }
    }

    public void SetBaseEvent(string _eventName, UnityAction _action)
    {
        if (baseEvents.ContainsKey(_eventName))
        {
            baseEvents.Remove(_eventName);
            baseEvents.Add(_eventName, _action);
        }
        else
        {
            baseEvents.Add(_eventName, _action);
        }
    }

    public void AddBaseEvent(string _eventName, UnityAction _event)
    {
        Debug.Log("add event : " + _eventName);
        // 있는 이벤트인지 확인
        if (baseEvents.ContainsKey(_eventName))
        {
            baseEvents[_eventName] -= _event;
            baseEvents[_eventName] += _event;
        }
        else
        {
            UnityAction _temp = () => { };
            _temp += _event;

            SetBaseEvent(_eventName, _temp);
        }
    }

    public void SetEvent(string _eventName, UnityAction _action)
    {
        if (events.ContainsKey(_eventName))
        {
            events.Remove(_eventName);
            events.Add(_eventName, _action);
        }
        else
        {
            events.Add(_eventName, _action);
        }
    }

    public void AddEvent(string _eventName, UnityAction _event)
    {
        Debug.Log("add event : " + _eventName);
        // 있는 이벤트인지 확인
        if (events.ContainsKey(_eventName))
        {
            events[_eventName] -= _event;
            events[_eventName] += _event;
        }
        else 
        {
            UnityAction _temp = () => { };
            _temp += _event;

            SetEvent(_eventName, _temp);
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
        else if (baseEvents.ContainsKey(_eventName))
        {
            return baseEvents[_eventName];
        }
        else 
        {
            Debug.Log(_eventName + "은 존재하지 않는 이벤트입니다.");
            return null; 
        }
    }
}
