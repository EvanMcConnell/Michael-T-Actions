using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Gamemanager finna gonna manage shit innit
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("no game manager here Kiddo");

            return _instance;
        }
    }

    public Dictionary<SubscriptionID, SubscriptionClass> SubscriptionClasses { get => subscriptionClasses; set => subscriptionClasses = value; }

    private Dictionary<SubscriptionID, SubscriptionClass> subscriptionClasses = new Dictionary<SubscriptionID, SubscriptionClass>();

    public PlatformerInputHandler inputHandler;

    private void Awake()
    {
        if(_instance == null) _instance = this;
        else Destroy(gameObject);

        inputHandler = GetComponent<PlatformerInputHandler>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        subscriptionClasses.Add(SubscriptionID.sprint, new SubscriptionClass(false, false, 120, 0));
        subscriptionClasses.Add(SubscriptionID.doubleJump, new SubscriptionClass(false, false, 120, 0));
        subscriptionClasses.Add(SubscriptionID.zAxis, new SubscriptionClass(true, false, 120, 0));
        subscriptionClasses.Add(SubscriptionID.bed, new SubscriptionClass(false, false, 120, 0));
    }

    [SerializeField] private int canhaBucks;
    [SerializeField] private int kubaKoin;
    [SerializeField] private int _realMoney;

    public List<Receipt> receipts;

    [System.Serializable]
    public struct Receipt
    {
        public Currency Oil;
        public string Title;
        public string Value;
        public string Balance;
    }

    public int GetCurrency(Currency currency)
    {
        switch (currency)
        {
            case Currency.kubaKoin:
                return kubaKoin;

            case Currency.canhaBucks:
                return canhaBucks;

            case Currency.realMoney:
                return _realMoney;

            default:
                return 0;
        }
    }

    public void SetCurrency(Currency currency, int value)
    {
        switch (currency)
        {
            case Currency.kubaKoin:
                kubaKoin = value;
                break;

            case Currency.canhaBucks:
                 canhaBucks = value;
                break;

            case Currency.realMoney:
                 _realMoney = value;
                break;
        }
    }

    public void IncrementCurrency(Currency currency, int value)
    {
        switch (currency)
        {
            case Currency.kubaKoin:
                kubaKoin += value;
                break;

            case Currency.canhaBucks:
                canhaBucks += value;
                break;

            case Currency.realMoney:
                _realMoney += value;
                break;
        }
    }

    public bool PurchaseWithCurrency(Currency currency, int value, string name)
    {
        if (Currency.kubaKoin == currency && kubaKoin >= value)
        {
            kubaKoin -= value;
            
            Receipt newReceipt = new Receipt();
            newReceipt.Oil = Currency.kubaKoin;
            newReceipt.Title = name;
            newReceipt.Value = (-value).ToString();
            newReceipt.Balance = kubaKoin.ToString();
            receipts.Add(newReceipt);
            
            return true;
        }
        else if (Currency.canhaBucks == currency && canhaBucks >= value)
        {
            canhaBucks -= value;
            
            Receipt newReceipt = new Receipt();
            newReceipt.Oil = Currency.canhaBucks;
            newReceipt.Title = name;
            newReceipt.Value = (-value).ToString();
            newReceipt.Balance = canhaBucks.ToString();
            receipts.Add(newReceipt);
            
            return true;
        }
        else if (Currency.realMoney == currency && _realMoney >= value)
        {
            _realMoney -= value;
            
            Receipt newReceipt = new Receipt();
            newReceipt.Oil = Currency.realMoney;
            newReceipt.Title = name;
            newReceipt.Value = (-value).ToString();
            newReceipt.Balance = _realMoney.ToString();
            receipts.Add(newReceipt);
            
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Yummy Doner Meat
    /// </summary>
    public bool yummyDonerMeat;


    public bool IsSubActive(SubscriptionID ID)
    {
        return subscriptionClasses[ID].isActive;
    }

    public bool IsSubUnlocked(SubscriptionID ID)
    {
        return subscriptionClasses[ID].isUnlocked;
    }

 
    public void SubScribe(SubscriptionID ID)
    {
        // is unlocked 
        StartCoroutine(SubscribeToSprintCor(ID));
        ///REEE
        FindObjectOfType<PlayersCosmeticsController>().ActivateCosmetic(ID);

    }

    public int GetSubTimeLeft(SubscriptionID ID)
    {
        if (subscriptionClasses.TryGetValue(ID, out SubscriptionClass sub))
        {
            return sub.timeLeft;
        }
        return 0;
    }

    IEnumerator SubscribeToSprintCor(SubscriptionID ID)
    {
        subscriptionClasses[ID].timeLeft = subscriptionClasses[ID].duration;
        subscriptionClasses[ID].isActive = true;

        while (subscriptionClasses[ID].timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            subscriptionClasses[ID].timeLeft--;
        }

        subscriptionClasses[ID].isActive = false;
    }

}

/// Classes
public enum Currency { realMoney, kubaKoin, canhaBucks }
public enum SubscriptionID { sprint, doubleJump, zAxis, bed }

[System.Serializable]
public class SubscriptionClass
{
    [SerializeField] internal bool isActive;
    [SerializeField] internal bool isUnlocked;
    [SerializeField] internal int duration = 120;
    [SerializeField] internal int timeLeft;

    public SubscriptionClass(bool isActive, bool isUnlocked, int duration, int timeLeft)
    {
        this.isActive = isActive;
        this.isUnlocked = isUnlocked;
        this.duration = duration;
        this.timeLeft = timeLeft;
    }
}