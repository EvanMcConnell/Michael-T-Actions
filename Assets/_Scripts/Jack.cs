using System;
using UnityEngine;

public class Jack : MonoBehaviour
{
    public static Jack corporealForm;

    private void Awake()
    {
        corporealForm = this;
    }
}
