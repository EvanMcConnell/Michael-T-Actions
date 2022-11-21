using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventTriggerArea : MonoBehaviour
{
    [SerializeField] UnityEvent EventOnTrigger;
    [SerializeField] bool DestroyOnTrigger = false;

    public void TriggerEvent()
    {
        EventOnTrigger.Invoke();

        if (DestroyOnTrigger)
            Destroy(this.gameObject);
    }
}
