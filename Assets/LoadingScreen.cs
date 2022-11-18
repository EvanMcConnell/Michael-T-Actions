using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public void loadingDone()
    {
        WorldWideWeb.Instance.StartGame();
    }
}
