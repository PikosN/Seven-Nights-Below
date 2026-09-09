using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public TMP_Text FPMoneyText;
    public TMP_Text shopMoneyText;
    public int money = 15;

    public void AddMoney(int amount)
    {
        money += amount;
        G.progressManager.AddProgress(amount);
        UpdateMoneyUI();
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
}
