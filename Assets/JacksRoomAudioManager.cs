using System;
using UnityEngine;

public class JacksRoomAudioManager : MonoBehaviour
{
    public static JacksRoomAudioManager Instance;
    [SerializeField] private AudioLowPassFilter rain;

    private void Awake() => Instance = this;

    // private void OnEnable() => ON();
    //
    // private void OnDisable() => OFF();

    public void OFF() => rain.enabled = false;
    
    public void ON() => rain.enabled = true;
}
