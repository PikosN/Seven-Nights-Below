using System.Collections.Generic;

public class UpgradeData
{
    public string id;
    public string name;
    public string description;
    public int baseCost;
    public int amountOfUpgrades;

    public float costGrowthRate;
    public float effectRate;
    public bool showStats = false;
}

public class AllUpgrades
{
    public static List<UpgradeData> all = new List<UpgradeData>
    {
        // Growth upgrade
        new UpgradeData()
        {
            id = "growth",
            name = "[00] BETTER LAMPS",
            description = "Plants growth time -10%",
            baseCost = 20,
            amountOfUpgrades = 10,
            costGrowthRate = 1.7f,
            effectRate = 0.9f,
            showStats = true,
            
},
        // Money upgrade
        new UpgradeData()
        {
            id = "money",
            name = "[01] FERTILIZERS",
            description = "The value of plant +20%",
            baseCost = 25,
            amountOfUpgrades = 10,
            costGrowthRate = 1.8f,
            effectRate = 1.2f,
            showStats = true,
        },
        // Autoharvest upgrade
        new UpgradeData()
        {
            id = "autoharvest",
            name = "[02] AUTOHARVESTING",
            description = "Automatic plant harvesting!",
            baseCost = 100,
            amountOfUpgrades = 1,
            costGrowthRate = 1f
        },
        new UpgradeData()
        {
            id = "bonus_chance",
            name = "[03] BONUS CHANCE",
            description = "The chance of extra money +5%",
            baseCost = 40,
            amountOfUpgrades = 20,
            costGrowthRate = 2f,
            effectRate = 0.05f,
            showStats = true,
        },
        new UpgradeData()
        {
            id = "bonus_money",
            name = "[04] BETTER FERTILIZERS",
            description = "The value of extra money +1",
            baseCost = 100,
            amountOfUpgrades = 5,
            costGrowthRate = 2.3f,
            effectRate = 1f,
            showStats = true,
        },
        //new UpgradeData()
        //{
            //id = "",
            //name = "",
            //description = "",
            //baseCost = 0,
            //amountOfUpgrades = 0,
            //costGrowthRate = 0f,
            //effectRate = 0f
        //},
    };
    public static UpgradeData GetUpgrade(string id)
    {
        return all.Find(upgrade => upgrade.id == id);
    }
}