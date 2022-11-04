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

    private void Awake()
    {
        if(_instance == null) _instance = this;
        else Destroy(gameObject);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    /// 
    /// WOW this could just be 3 lines of code but instead I did all this bs, 
    /// suppose it makes it cleaner in other scripts
    /// We can also do checks for getters and setters 
    /// 

    // Useless Money for the player to collect and do nothing with 
    private float _usefulIngameMoney;
    public float UsefulInGameMoney => _usefulIngameMoney;
    
    /// <summary>
    /// Add/Remove money from pool
    /// </summary>
    /// <param name="value">amount to change</param>
    public void ChangeUsefulsInGameMoney(float value)
    {
        _usefulIngameMoney += value;

        //TODO: add checks for negative values
    }
    public void SetUsefulInGameMoney(float value) => _usefulIngameMoney = value;


    // Useful Money that is hard to optain 
    private float _uselessIngameMoney;
    public float UselessInGameMoney => _uselessIngameMoney;
    public void ChangeUselessInGameMoney(float value)
    {
        _uselessIngameMoney += value;
        //TODO: add checks for negative values

    }
    public void SetUselessInGameMoney(float value) => _uselessIngameMoney = value;


    // Jacks actual money and IBAN
    private float _realMoney;
    public float RealMoney => _usefulIngameMoney;
    public void ChangerealMoney(float value)
    {
        _usefulIngameMoney += value;
        //TODO: add checks for negative values

    }
    public void SetRealMoney(float value) => _usefulIngameMoney = value;


    /// <summary>
    /// Yummy Doner Meat
    /// </summary>
    public bool yummyDonerMeat;
}
