using UnityEngine;

public class Main : MonoBehaviour
{
    public EconomyManager economyManager;
    public ShopUI shopUI;
    public UpgradeManager upgradeManager;
    public PlantManager plantManager;
    public PrestigeManager prestigeManager;
    public ProgressManager progressManager;
    public PlantSelectionUI plantSelectionUI;
    public Player player;
    public UIManager UImanager;
    public LightManager lightManager;
    public AudioManager audioManager;
    public DialogueManager dialogueManager;
    public SettingsManager settingsManager;
    public ElectricalPanel electricalPanel;


    void Awake()
    {
        G.economyManager = economyManager;
        G.shopUI = shopUI;
        G.upgradeManager = upgradeManager;
        G.plantManager = plantManager;
        G.prestigeManager = prestigeManager;
        G.progressManager = progressManager;
        G.plantSelectionUI = plantSelectionUI;
        G.player = player;
        G.UIManager = UImanager;
        G.lightManager = lightManager;
        G.audioManager = audioManager;
        G.dialogueManager = dialogueManager;
        G.settingsManager = settingsManager;
        G.electricalPanel = electricalPanel;

        StartGame();
    }

    void StartGame()
    {
        G.progressManager.StartDay();
    }
}
