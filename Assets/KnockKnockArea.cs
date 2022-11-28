using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockKnockArea : PurchaseArea
{
    public void KnockKnock()
    {
        FirstPersonCameraController.Instance.WakeUp();
    }
}
