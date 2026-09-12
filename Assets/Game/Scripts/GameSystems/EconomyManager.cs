using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public TMP_Text FPMoneyText;
    public TMP_Text shopMoneyText;
    public Transform moneyPopupContainer;
    public MoneyPopup moneyPopupPrefab;
    public int money = 15;

    public void AddMoney(int amount)
    {
        money += amount;
        StartCoroutine(G.progressManager.AddProgress(amount));
        UpdateMoneyUI();
        ShowPopup(amount);
    }
    
    public bool TrySpentMoney(int amount)
    {
        if (amount <= money)
        {
            money -= amount;
            UpdateMoneyUI();
            return true;
        }
        return false;
    }
    void UpdateMoneyUI()
    {
        FPMoneyText.text = "$" + money;
        shopMoneyText.text = "$" + money;
    }

    public void Reset()
    {
        money = 15;
        UpdateMoneyUI();
    }

    void ShowPopup(int amount)
    {
        MoneyPopup popup = Instantiate(moneyPopupPrefab, moneyPopupContainer);

        popup.Setup(amount);
    }
}
