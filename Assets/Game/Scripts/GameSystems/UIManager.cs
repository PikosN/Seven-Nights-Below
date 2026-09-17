using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject ingameUI;
    public GameObject interactableUI;
    public GameObject defaultCrosshair;
    public TMP_Text interactableText;
    public GameObject fpMoneyText;
    public GameObject dialogueUI;
    public GameObject pauseMenuUI;
    GameObject pauseMenuPanel;
    Button resumeButton;
    Button settingsButton;
    Button mainMenuButton;

    void Awake()
    {
        pauseMenuPanel = pauseMenuUI.transform.Find("Panel").gameObject;
        resumeButton = pauseMenuUI.transform.Find("ResumeButton").GetComponent<Button>();
        settingsButton = pauseMenuUI.transform.Find("SettingsButton").GetComponent<Button>();
        mainMenuButton = pauseMenuUI.transform.Find("ExitButton").GetComponent<Button>();

        resumeButton.onClick.AddListener(() => TogglePauseMenu(false));
        settingsButton.onClick.AddListener(() => TogglePauseMenu(false));
    }


    public void SetInteractableUI(bool value, string text)
    {
        interactableUI.SetActive(value);
        interactableText.text = text;
        defaultCrosshair.SetActive(!value);
    }

    public void SetUIEnabled(bool enabled)
    {
        ingameUI.SetActive(enabled);
        fpMoneyText.SetActive(enabled);
    }

    public void SetCutsceneUIEnabled(bool enabled)
    {
        dialogueUI.SetActive(enabled);
    }

    public void TogglePauseMenu(bool enabled)
    {
        pauseMenuUI.SetActive(enabled);
    }
    void ToggleSettingsMenu(bool enabled)
    {
        resumeButton.gameObject.SetActive(!enabled);
        settingsButton.gameObject.SetActive(!enabled);
        mainMenuButton.gameObject.SetActive(!enabled);


    }
}

