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
    AudioSource AudioSourceButton;

    private void Start()
    {
        AudioSourceButton = GetComponent<AudioSource>();
        AudioSourceButton.loop = true;
        AudioSourceButton.Stop();
        ButtonResetEvent.Invoke();
    }

    internal void ButtonDown()
    {
        if (!AudioSourceButton.isPlaying)
            AudioSourceButton.Play();

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
        AudioSourceButton.Stop();
        ButtonResetEvent.Invoke();
    }
}
