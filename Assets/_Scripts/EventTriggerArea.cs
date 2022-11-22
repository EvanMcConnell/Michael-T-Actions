using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventTriggerArea : MonoBehaviour
{
    [SerializeField] UnityEvent EventOnTrigger;
    [SerializeField] bool DestroyOnTrigger = false;
    [SerializeField] float Delay = 0;
    public void TriggerEvent()
    {
        StartCoroutine(TimedEvent(Delay));
        
    }

    IEnumerator TimedEvent(float delay)
    {
        yield return new WaitForSeconds(delay);

        EventOnTrigger.Invoke();

        if (DestroyOnTrigger)
            Destroy(this.gameObject);
    }
}
