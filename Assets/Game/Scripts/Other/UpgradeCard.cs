using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public AudioSource uiAudioSource;

    public AudioClip buySound;
    public AudioClip failedBuySound;

    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public TMP_Text statsText;
    public Button buyButton;
    public TMP_Text buyText;

    private UpgradeData upgrade;

    void Awake()
    {
        uiAudioSource = GameObject.Find("UIAudioSource").GetComponent<AudioSource>();
        buyText = buyButton.GetComponentInChildren<TMP_Text>();
    }

    public void Setup(UpgradeData cardData)
    {
        upgrade = cardData;

        descriptionText.text = cardData.description;
        
        Refresh();

        buyButton.onClick.AddListener(BuyAndApply);
    }
    void BuyAndApply()
    {
        int cost = GameMath.GetUpgradeCost(
            upgrade.baseCost,
            upgrade.costGrowthRate,
            G.upgradeManager.GetUpgradeLevel(upgrade.id)
            );
        if (G.economyManager.TrySpentMoney(cost))
        {
            uiAudioSource.PlayOneShot(buySound, 0.4f);

            G.upgradeManager.ApplyUpgrade(upgrade);
            if (G.upgradeManager.GetUpgradeLevel(upgrade.id) < upgrade.amountOfUpgrades)
            {
                Refresh();
            }
            else
            {
                SetPurchased();
            }
        }
        else
        {
            uiAudioSource.PlayOneShot(failedBuySound, 0.4f);
        }
    }
    public void Refresh()
    {
        int cost = GameMath.GetUpgradeCost(
            upgrade.baseCost,
            upgrade.costGrowthRate,
            G.upgradeManager.GetUpgradeLevel(upgrade.id)
            );
        int level = G.upgradeManager.GetUpgradeLevel(upgrade.id);

        if (upgrade.amountOfUpgrades == 1)
        {
            nameText.text = upgrade.name;
        }
        else
        {
            nameText.text = upgrade.name + " " + level;
        }

        // 100% -> 110%
        //if (upgrade.showStats == true)
        //{
        //    statsText.enabled = true;
        //    statsText.text = $"{Mathf.Pow(upgrade.effectRate, level) * 100:0.}% -> {Mathf.Pow(upgrade.effectRate, level + 1) * 100:0.}%";
        //}

        costText.text = "$" + cost;
    }

    public void Delete()
    {
        G.shopUI.activeUpgradeCards.Remove(this);
        Destroy(gameObject);
    }

    void SetPurchased()
    {
        buyButton.interactable = false;
        costText.text = "";
        //statsText.text = $"{Mathf.Pow(upgrade.effectRate, G.upgradeManager.GetUpgradeLevel(upgrade.id) * 100):0.}%";
        if (upgrade.showStats == true)
        {
            buyText.text = "MAXED";
        }
        else
        {
            buyText.text = "PURCHASED";
        }
    }
}
