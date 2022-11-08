using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateCurrencyText : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    [SerializeField] Currency currency;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        textMesh.SetText(GameManager.Instance.GetCurrency(currency).ToString());
    }
}