using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int growthLevel = 0;
    public int moneyLevel = 0;
    public int hasAutoharvest = 0;
    public int bonusChanceLevel = 0;
    public int bonusMoneyLevel = 0;
    public int lossChanceLevel = 0;
    public int hasFastFarmer = 0;
    public int hasMassiveFarmer = 0;
    public int hasRiskyFarmer = 0;

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.id)
        {
            case "growth":
                growthLevel++;
                break;
            case "money":
                moneyLevel++;
                break;
            case "bonus_chance":
                bonusChanceLevel++;
                break;
            case "bonus_money":
                bonusMoneyLevel++;
                break;
            case "loss_chance":
                lossChanceLevel++;
                break;
            case "autoharvest":
                hasAutoharvest = 1;
                break;
            case "fast_farmer":
                hasFastFarmer = 1;
                break;
            case "massive_farmer":
                hasMassiveFarmer++;
                break;
            case "risky_farmer":
                hasRiskyFarmer++;
                break;
        }
    }

    public int GetUpgradeLevel(string id)
    {
        switch (id)
        {
            case "growth":
                return growthLevel;

            case "money":
                return moneyLevel;

            case "autoharvest":
                return hasAutoharvest;

            case "bonus_chance":
                return bonusChanceLevel;
                
            case "bonus_money":
                return bonusMoneyLevel;

            case "loss_chance":
                return lossChanceLevel;

            case "fast_farmer":
                return hasFastFarmer;

            case "massive_farmer":
                return hasMassiveFarmer;

            case "risky_farmer":
                return hasRiskyFarmer;
            default: 
                return 0;
        }
    }

    public void Reset()
    {
        growthLevel = 0;
        moneyLevel = 0;
        hasAutoharvest= 0;
        bonusChanceLevel = 0;
        bonusMoneyLevel = 0;
        hasFastFarmer = 0;
        hasMassiveFarmer = 0;
        hasRiskyFarmer = 0;
    }
}
