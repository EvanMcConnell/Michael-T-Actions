using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayer : MonoBehaviour
{
    [SerializeField] float jumpForce = 3;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hey");
        if (other.CompareTag("Player"))
        {
            Debug.Log("heyo");

            if (other.TryGetComponent(out PlatformerPlayerController platformerPlayerController))
            {
                Debug.Log("heyee");

                platformerPlayerController.HigherJump(jumpForce);
            }
        }
    }
}
