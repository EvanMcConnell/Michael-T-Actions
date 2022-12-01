using System;
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

    [SerializeField] private Boolean timerSound = true;

    private void Start()
    {
        AudioSourceButton = GetComponent<AudioSource>();
        AudioSourceButton.loop = true;
        AudioSourceButton.Stop();
        ButtonResetEvent.Invoke();
    }

    internal void ButtonDown()
    {
        if (!AudioSourceButton.isPlaying && timerSound)
        {
            AudioSourceButton.pitch = 1f;
            AudioSourceButton.Play();
        }

        animator.Play("ButtonDown");
        ButtonDownEvent.Invoke();
    }

    internal void ButtonUp()
    {
        StartCoroutine(TimerEvent());
    }

    IEnumerator TimerEvent()
    {
        yield return new WaitForSeconds(timerSeconds/2f);
        AudioSourceButton.pitch = 1.25f;
        yield return new WaitForSeconds(timerSeconds/4f);
        AudioSourceButton.pitch = 1.5f;
        yield return new WaitForSeconds(timerSeconds/8f);
        AudioSourceButton.pitch = 2f;
        yield return new WaitForSeconds(timerSeconds/8f);
        animator.Play("ButtonUp");
        AudioSourceButton.Stop();
        ButtonResetEvent.Invoke();
    }
}
