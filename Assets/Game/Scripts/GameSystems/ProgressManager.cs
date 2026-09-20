using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public SleepAnimation sleepAnimation;

    public float dayProgress;
    public float progress;
    public int dayGoal;
    public bool isDayCompleted;
    public QuotaSign quotaSign;
    
    public ElectricalPanel electricalPanel;

    public void StartDay()
    {
        StartCoroutine(sleepAnimation.Wakeup());

        G.shopUI.Reset();

        dayProgress = 0f;
        progress = 0f;
        isDayCompleted = false;

        dayGoal = GameMath.GetDayGoal(500, G.prestigeManager.currentDay);
        
        quotaSign.Setup();

    }

    public IEnumerator AddProgress(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            dayProgress++;
            
            quotaSign.quotaText.text = $"{dayProgress}/{dayGoal}";;
            
            progress = Mathf.Clamp01(dayProgress / dayGoal);
            if (progress >= 1)
            {
                isDayCompleted = true;
                electricalPanel.AddEnergy(-electricalPanel.maxEnergy);
                quotaSign.Setup();
                quotaSign.SignOn();
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
