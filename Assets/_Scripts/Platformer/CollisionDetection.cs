using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("KubaCoin"))
        {
            Debug.Log("hey");
        }

        Debug.Log($"Cos {collision.gameObject}");
    }
}
