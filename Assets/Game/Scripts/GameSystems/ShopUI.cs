using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUI : MonoBehaviour
{
    public GameObject shopPanel;
    public UpgradeCard upgradeCardPrefab;
    public Transform content;
    public GameObject fpMoneyText;

    public InputActionReference cancelAction;

    public List<UpgradeCard> activeUpgradeCards;

    private void OnEnable()
    {
        cancelAction.action.Enable();
    }
    private void OnDisable()
    {
        cancelAction.action.Disable();
    }

    void Update()
    {
        if (shopPanel.activeSelf && cancelAction.action.WasPressedThisFrame())
        {
            CloseShop();
        }
    }

    public void CreateUpgradeCard(string id)
    {
        UpgradeData upgrade = AllUpgrades.GetUpgrade(id);
        UpgradeCard card = Instantiate(upgradeCardPrefab, content);
        card.Setup(upgrade);

        activeUpgradeCards.Add(card);
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        fpMoneyText.SetActive(false);

        G.player.SetMovementEnabled(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        fpMoneyText.SetActive(true);

        G.player.SetMovementEnabled(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Reset()
    {
        while (activeUpgradeCards.Count > 0)
        {
            activeUpgradeCards[0].Delete();
        }


        CreateUpgradeCard("growth");
        CreateUpgradeCard("money");
        
        if (G.prestigeManager.prestigeLevel >= 1)
        {
            CreateUpgradeCard("autoharvest");
            CreateUpgradeCard("bonus_chance");
            CreateUpgradeCard("bonus_money");
        }
    }
}
