using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    /// <summary>
    /// the smaller the wider floating
    /// </summary>
    [SerializeField]
    float floatVariation = 2;

    /// <summary>
    /// speen speed 
    /// </summary>
    [SerializeField]
    float rotationSpeed = 10;

    float zeroY;
    float posX, posZ;
    float graphX = 0;
    bool up = true;

    void Start()
    {
        
    }

    void Update()
    {
        upDown();

        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
    }
    void upDown()
    {
        if (graphX > Mathf.PI)
        {
            graphX -= Mathf.PI;
            up = !up;
        }
        graphX += Time.deltaTime;
        transform.position = new Vector3(posX, (up ? 1 : -1) * (Mathf.Sin(graphX) / floatVariation) + zeroY, posZ);
    }
}
