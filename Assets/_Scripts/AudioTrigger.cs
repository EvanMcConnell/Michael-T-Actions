using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] float timeAmount = 0.01f;
    [SerializeField] float Steps = 0.05f;
    [SerializeField] float volume = .5f;

    /// <summary>
    /// EnterTrigger Normal > Turn On the Music 
    /// EnterTrigger Flipped > Turn Off the Music 
    /// </summary>
    [SerializeField] bool flipTrigger = false;
    [SerializeField] bool defaultOn = false;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        audioSource.volume = defaultOn ? volume : 0;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();

            if (flipTrigger)
                TurnDown();
            else
                TurnUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();

            if (flipTrigger)
                TurnUp();
            else
                TurnDown();
        }
    }

    private void TurnDown() => StartCoroutine(VolumeDown(audioSource.volume, 0, -1 * Steps));
    private void TurnUp() => StartCoroutine(VolumeChange(audioSource.volume, volume, Steps));

    IEnumerator VolumeChange(float start, float end, float steps)
    {
        audioSource.volume = start;
        while (audioSource.volume <= end)
        {
            yield return new WaitForSeconds(timeAmount);
            audioSource.volume += steps;
        }
    }

    IEnumerator VolumeDown(float start, float end, float steps)
    {
        audioSource.volume = start;

        while (audioSource.volume >= end)
        {
            yield return new WaitForSeconds(timeAmount);
            audioSource.volume += steps;
        }
    }

    public void VolumeDown()
    {
        StopAllCoroutines();
        StartCoroutine(VolumeDown(audioSource.volume, 0, -1 * Steps));
    }

    public void VolumeUp()
    {
        StopAllCoroutines();
        StartCoroutine(VolumeChange(audioSource.volume, volume, Steps));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //added the 1.001f so you could see the color more clearly
        Gizmos.DrawWireCube(transform.position + GetComponent<BoxCollider>().center, Vector3.Scale(GetComponent<BoxCollider>().size * 1.001f, transform.localScale));
    }
}
