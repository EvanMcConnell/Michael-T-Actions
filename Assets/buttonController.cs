using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class buttonController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] UnityEvent ButtonDownEvent;
    [SerializeField] UnityEvent ButtonResetEvent;
    [SerializeField] int timerSeconds = 5;

    internal void ButtonDown()
    {
        animator.Play("ButtonDown");
        ButtonDownEvent.Invoke();
    }

    internal void ButtonUp()
    {
        StartCoroutine(TimerEvent());
    }

    IEnumerator TimerEvent()
    {
        yield return new WaitForSeconds(timerSeconds);
        animator.Play("ButtonUp");
        ButtonResetEvent.Invoke();
    }
}
