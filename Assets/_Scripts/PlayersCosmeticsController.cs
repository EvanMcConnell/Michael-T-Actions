using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCosmeticsController : MonoBehaviour
{
    public List<Cosmetic> cosmetics;

    private void Start()
    {
        ResetAllCosmetics();
    }

    public void ResetAllCosmetics()
    {
        foreach (var cosmetic in cosmetics)
        {
            cosmetic.itemGameobject.SetActive(cosmetic.unlocked);
        }
    }


    public void ActivateCosmetic(string ID, bool unlocked = true) {
        try
        {
            Cosmetic cos = cosmetics.Find(cos => cos.ID == ID);

            cos.unlocked = unlocked;
            cos.itemGameobject.SetActive(cos.unlocked);

        } catch (System.Exception e)
        {
            Debug.LogError($"no cosmetic with that ID or somethin IDK, heres the error: {e}");
        }
    }
}

[System.Serializable]
public class Cosmetic
{
    [SerializeField] internal string ID;
    [SerializeField] internal GameObject itemGameobject;
    [SerializeField] internal bool unlocked;
}