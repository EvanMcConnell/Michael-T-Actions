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

    public void AnimEvents_ToggleCursorOff() => CursorController.Instance.toggle(false);

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

    public void AnimEvent_Quit() => Application.Quit();

    public void AnimEvent_PlayerEndingPosition()
    {
        PlatformerPlayerController.Instance.isPaused = true;
        PlatformerPlayerController.Instance.GetComponent<CharacterController>().enabled = false;
        PlatformerPlayerController.Instance.transform.position = new Vector3(59.285003662109378f, 1.5050048828125f, 1486.5889892578125f);
        PlatformerPlayerController.Instance.transform.eulerAngles = Vector3.zero;
    }
}
