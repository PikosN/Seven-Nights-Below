using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject ingameUI;
    public GameObject interactableUI;
    public GameObject defaultCrosshair;
    public TMP_Text interactableText;
    public GameObject fpMoneyText;
    public GameObject dialogueUI;

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
}

