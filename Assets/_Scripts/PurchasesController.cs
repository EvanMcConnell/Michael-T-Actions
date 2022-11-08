using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PurchasesController : MonoBehaviour
{
    [SerializeField] internal List<PurchasableItem> PurchasableItems;
    

    public void PurchaseItem(string ID, bool unlocked = true)
    {
        try
        {
            PurchasableItem purchasableItem = PurchasableItems.Find(p => p.ID == ID);

            //TODO Money Check 
        }
        catch (System.Exception e)
        {
            Debug.LogError($"no cosmetic with that ID or somethin IDK, heres the error: {e}");
        }
    }
}

[System.Serializable]
public class PurchasableItem {
    [SerializeField] internal string ID;
    [SerializeField] internal Currency currency;
    [SerializeField] internal int cost;
    [SerializeField] internal bool unlocked;
    [SerializeField] internal bool purchased;
}
