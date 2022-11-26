using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCosmeticsController : MonoBehaviour
{
    public List<CosmeticSub> cosmeticsSub;
    public List<CosmeticPur> cosmeticsPur;


    private void Start()
    {
        ResetAllCosmetics();
    }

    public void ResetAllCosmetics()
    {
        foreach (var cosmetic in cosmeticsSub)
        {
            cosmetic.itemGameobject.SetActive(cosmetic.unlocked);
        }
    }

    public void ActivateCosmeticPur(string ID, bool unlocked = true)
    {
        try
        {
            CosmeticPur cos = cosmeticsPur.Find(cos => cos.ID == ID);

            cos.unlocked = unlocked;
            cos.itemGameobject.SetActive(cos.unlocked);

        }
        catch (System.Exception e)
        {
            Debug.LogError($"no cosmetic with that ID or somethin IDK, heres the error: {e}");
        }

        ResetAllCosmetics();
    }


    public void ActivateCosmetic(SubscriptionID ID, bool unlocked = true) {
        try
        {
            CosmeticSub cos = cosmeticsSub.Find(cos => cos.ID == ID);

            cos.unlocked = unlocked;
            cos.itemGameobject.SetActive(cos.unlocked);

        } catch (System.Exception e)
        {
            Debug.LogError($"no cosmetic with that ID or somethin IDK, heres the error: {e}");
        }

        ResetAllCosmetics();
    }
}

[System.Serializable]
public class CosmeticSub
{
    [SerializeField] internal SubscriptionID ID;
    [SerializeField] internal GameObject itemGameobject;
    [SerializeField] internal bool unlocked;
}

[System.Serializable]
public class CosmeticPur
{
    [SerializeField] internal string ID;
    [SerializeField] internal GameObject itemGameobject;
    [SerializeField] internal bool unlocked;
}