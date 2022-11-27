using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationEvents : MonoBehaviour
{
    public void AnimEvent_DisableObject() => gameObject.SetActive(false);
    
    public void AnimEvent_PlayAudioClipOneShot(AudioClip audio) => GetComponent<AudioSource>().PlayOneShot(audio);

    public void AnimEvent_PlayAudioClip(AudioClip audio)
    {
        GetComponent<AudioSource>().clip = audio;
        GetComponent<AudioSource>().Play();
    }

    public void AnimEvent_GameLoadingDone() => WorldWideWeb.Instance.StartGame();

    public void AnimEvent_ChangeInputMap(InputMaps map)
    {
        switch (map)
        {
            case InputMaps.Computer:
                GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Computer");
                break;
            
            case InputMaps.Human:
                GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Human");
                break;
            
            case InputMaps.Player:
                GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
                break;
            
            case InputMaps.Intro:
                GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Intro");
                break;
        }
    }

    public void AnimEvent_DisableAnimator() => GetComponent<Animator>().enabled = false;
}
