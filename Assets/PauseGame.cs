using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public void PauseGameCall()
    {
        CanhaBucksInstaller.Instance.installCanha = true;
        WorldWideWeb.Instance.pauseGame();
    }

}
