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
    public bool isUnique;
    public bool isMany;
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
            isMany =  true,
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
            isMany = true,
        },
        
        new UpgradeData()
        {
            id = "bonus_chance",
            name = "[02] BONUS CHANCE",
            description = "The chance of extra money +5%",
            baseCost = 40,
            amountOfUpgrades = 20,
            costGrowthRate = 2f,
            effectRate = 0.05f,
            isMany =  true,
        },
        
        new UpgradeData()
        {
            id = "bonus_money",
            name = "[03] BETTER FERTILIZERS",
            description = "The value of extra money +1",
            baseCost = 100,
            amountOfUpgrades = 5,
            costGrowthRate = 2.3f,
            effectRate = 1f,
            isMany =  true,
        },
        
        new UpgradeData()
        {
            id = "loss_chance",
            name = "[04] LUCKY PLANTS",
            description = "The chance of plant loss -2% (Value at start: 25%)",
            baseCost = 50,
            amountOfUpgrades = 5,
            costGrowthRate = 1.8f,
            effectRate = 0.02f,
            isMany = true,
        },
        
        // Autoharvest upgrade
        new UpgradeData()
        {
            id = "autoharvest",
            name = "[05] AUTOHARVESTING",
            description = "Automatic plant harvesting!",
            baseCost = 100,
            amountOfUpgrades = 1,
            costGrowthRate = 1f,
        },
        new UpgradeData()
        {
            id = "fast_farmer",
            name = "[06] FAST FARMER",
            description = "Plants growth time -50%, plant loss chance -20%",
            baseCost = 250,
            amountOfUpgrades = 1,
            costGrowthRate = 1f,
            isUnique =  true,
        },
        new UpgradeData()
        {
            id = "massive_farmer",
            name = "[07] MASSIVE FARMER",
            description = "The value of plants +100%, plant loss chance -20%",
            baseCost = 250,
            amountOfUpgrades = 1,
            costGrowthRate = 1f,
            isUnique =  true,
        },
        new UpgradeData()
        {
            id = "risky_farmer",
            name = "[08] RISKY FARMER",
            description = "The value of extra money 7x, plant loss chance +25%",
            baseCost = 250,
            amountOfUpgrades = 1,
            costGrowthRate = 1f,
            isUnique =  true,
        }
        
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