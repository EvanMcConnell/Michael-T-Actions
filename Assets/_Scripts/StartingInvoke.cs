using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingInvoke : MonoBehaviour
{
    public void StartingInvokeSubRemoving() {
        GameManager.Instance.removeAllSubs();
    }
}
