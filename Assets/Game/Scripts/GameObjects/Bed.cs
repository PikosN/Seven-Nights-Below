using System.Collections;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    public SleepAnimation sleepAnimation;
    public void Interact()
    {
        if (G.progressManager.isDayCompleted)
        {
            StartCoroutine(EndDay());
        }
    }

    private IEnumerator EndDay()
    {
        yield return StartCoroutine(sleepAnimation.GoToSleep());
        G.prestigeManager.Prestige();
    }

    public string GetInteractText()
    {
        if (G.progressManager.isDayCompleted)
        {
            return "[ E ] Sleep";
        }
        else
        {
            return "You don't want to sleep. You need to work.";
        }
        
    }
}
