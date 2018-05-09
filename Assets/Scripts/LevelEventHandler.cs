using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelEvent : UnityEvent
{

}

public class LevelEventHandler : MonoBehaviour {

    private Dictionary<string, LevelEvent> eventDictionary;

    private static LevelEventHandler levelEventHandler;

    public static LevelEventHandler instance
    {
        get
        {
            if (!levelEventHandler)
            {
                levelEventHandler = FindObjectOfType(typeof(LevelEventHandler)) as LevelEventHandler;

                if (!levelEventHandler)
                {
                    Debug.LogError("There needs to be one active LevelEventHandler script on a GameObject in your scene.");
                }
                else
                {
                    levelEventHandler.Init();
                }
            }

            return levelEventHandler;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, LevelEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        LevelEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new LevelEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (levelEventHandler == null) return;
        LevelEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        LevelEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
