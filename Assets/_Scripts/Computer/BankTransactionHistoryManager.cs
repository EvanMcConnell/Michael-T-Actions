using TMPro;
using UnityEngine;

public class BankTransactionHistoryManager : MonoBehaviour
{
    public static BankTransactionHistoryManager _Instance;
    
    [SerializeField] private GameObject ReceiptObject;
    
    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        UpdateTransactionHistory();
    }

    public void UpdateTransactionHistory()
    {
        foreach (Transform obj in transform) Destroy(obj.gameObject);

        int receiptCount = 0;
        foreach (GameManager.Receipt receipt in GameManager.Instance.receipts)
        {
            if (receipt.Oil == Currency.realMoney)
            {
                GameObject newReceipt = Instantiate(ReceiptObject, transform);
                newReceipt.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = receipt.Title;
                newReceipt.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = receipt.Value;
                newReceipt.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = receipt.Balance;
            }
        }
    }
}
