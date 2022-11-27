using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnEnableSoundSpurt : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool ImNotDoinIt;
    private AudioSource src;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(!ImNotDoinIt) src.PlayOneShot(clip);
    }
    
    public void DoItAgain()
    {
        print("boop");
        src.PlayOneShot(clip);
    }
}
