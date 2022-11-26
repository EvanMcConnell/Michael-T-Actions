using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements; // <-- Does this do anything?
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    
    [SerializeField] private Transform start, end, platform;
    [SerializeField] float speed = 0.1f;
    private float T;
    private bool forward = true;
    private Rigidbody rb;

    private void Awake()
    {
        rb = platform.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        platform.position = start.position;
    }

    // Update is called once per frame
    void Update()
    {
        T = Mathf.Clamp(T + Time.deltaTime * speed * (forward ? 1:-1), 0, 1);
        
        Vector3 newPostion = Vector3.Lerp(start.position, end.position, T);
        rb.MovePosition(newPostion);
        
        if (T == 1 || T == 0) forward = !forward;
    }

    public Vector3 velocity() => rb.velocity;

    private void OnDrawGizmos()
    {
        Debug.DrawLine(start.position, end.position, Color.green, 2, false);
    }
}
