using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUI : MonoBehaviour
{
    public GameObject shopPanel;
    public UpgradeCard upgradeCardPrefab;
    public Transform basicUpgradesTransform;
    public Transform uniqueUpgradesTransform;
    public GameObject fpMoneyText;
    
    public GameObject uniqueUpgradesGameObject;

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
        UpgradeCard card;
        if (upgrade.isUnique)
        {
            card = Instantiate(upgradeCardPrefab, uniqueUpgradesTransform);
        }
        else
        {
            card = Instantiate(upgradeCardPrefab, basicUpgradesTransform);
        }
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
            CreateUpgradeCard("bonus_chance");
            CreateUpgradeCard("bonus_money");
            CreateUpgradeCard("loss_chance");
            CreateUpgradeCard("autoharvest");
            uniqueUpgradesGameObject.SetActive(true);
            CreateUpgradeCard("fast_farmer");
            CreateUpgradeCard("massive_farmer");
            CreateUpgradeCard("risky_farmer");
        }
    }
}
