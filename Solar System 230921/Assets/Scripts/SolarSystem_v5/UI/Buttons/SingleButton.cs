using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleButton : MonoBehaviour
{
    private EventManager_v5 eventManager;
    private Button btn;
    public string eventName = "exit";

    private void Awake()
    {
        eventManager = FindObjectOfType<EventManager_v5>();
        btn = GetComponent<Button>();
    }
    private void Start()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => { Debug.Log(eventName); });
        btn.onClick.AddListener(eventManager.GetEvent(eventName));
    }
}
