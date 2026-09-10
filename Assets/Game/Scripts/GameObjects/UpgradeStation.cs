using UnityEngine;

public class UpgradeStation : MonoBehaviour, IInteractable
{
    public string interactText = "[ E ] Open the shop";
    public void Interact()
    {
        G.shopUI.OpenShop();
    }
    public string GetInteractText() { return interactText; }

}
