using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject ConfirmPopup;

    void Update()
    {
        if (!Keyboard.current.escapeKey.wasPressedThisFrame) return;

        if (G.electricalPanel.electricalPanelUI.activeSelf)
        {
            G.electricalPanel.CloseUI();
        }
        else if (G.shopUI.shopPanel.activeSelf)
        {
            G.shopUI.CloseShop();
        }
        else if (pauseMenuUI.activeSelf)
        {
            TogglePauseMenu(false);
        }
        else
        {
            TogglePauseMenu(true);
        }
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
        if (enabled)
        {
            pauseMenuUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            G.player.SetMovementEnabled(false);
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            G.player.SetMovementEnabled(true);
        }
        
    }
    
    public void ToggleConfirmPopup()
    {
        ConfirmPopup.SetActive(!ConfirmPopup.activeSelf);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

