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

    public PlatformerInputHandler inputHandler;

    private void Awake()
    {
        if(_instance == null) _instance = this;
        else Destroy(gameObject);

        inputHandler = GetComponent<PlatformerInputHandler>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    [SerializeField] private int canhaBucks;
    [SerializeField] private int kubaKoin;
    [SerializeField] private int _realMoney;

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

    public bool PurchaseWithCurrency(Currency currency, int value)
    {
        if (Currency.kubaKoin == currency && kubaKoin >= value)
        {
            kubaKoin -= value;
            return true;
        }
        else if (Currency.canhaBucks == currency && canhaBucks >= value)
        {
            canhaBucks -= value;
            return true;
        }
        else if (Currency.realMoney == currency && _realMoney >= value)
        {
            _realMoney -= value;
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

    [Header("UNLOCK PAYWALLS, GIRLBOSS")]
    [SerializeField] internal bool sprintUnlocked;
    [SerializeField] internal bool doubleJumpUnlocked;
    [SerializeField] internal bool zAxisUnlocked;

}

/// Classes
public enum Currency { realMoney, kubaKoin, canhaBucks }

