using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCosmetic : MonoBehaviour
{
    
    public void UnlockCosmeticCall(string ID)
    {
        FindObjectOfType<PlayersCosmeticsController>().ActivateCosmeticPur(ID);
    }
}
