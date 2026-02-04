using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour
{
    public bool onGameStart;
    public bool isLooping;
    public float delayTime;
    public UnityEvent DelayedEvent;

    private void Awake()
    {
        if (onGameStart)
        {
            StartCoroutine(StartEvent());
        }
    }
    public void  StartEventVoid()
    {
        StartCoroutine(StartEvent());
    }
    public IEnumerator StartEvent()
    {
        yield return new WaitForSeconds(delayTime);
        DelayedEvent.Invoke();
        if (isLooping)
        {
            StartCoroutine(StartEvent());
        }
    }
}
