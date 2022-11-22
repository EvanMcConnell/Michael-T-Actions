using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void AnimEvent_DisableObject() => gameObject.SetActive(false);
    
    public void AnimEvent_PlayAudioClipOneShot(AudioClip audio) => GetComponent<AudioSource>().PlayOneShot(audio);
}
