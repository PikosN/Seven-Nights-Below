using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuotaSign : MonoBehaviour
{
    public TMP_Text quotaText;
    public Image signImage;
    public Light signLight;
    
    static Color redColor = new Color32(166, 61, 54, 255);
    static Color redGlowColor = new Color32(201, 75, 63, 255);
    static Color greenColor = new Color32(2, 129, 6, 255);
    static Color greenGlowColor = new Color32(61, 217, 57, 255);
    static Color zeroColor = new Color32(0, 0, 0, 0);
    static Color quotaTextColor = new Color32(181, 183, 154, 255);

    Color currentColor = redColor;
    Color currentGlowColor = redGlowColor;

    void Awake()
    {
        quotaText =  GetComponentInChildren<TMP_Text>();
        signImage = GetComponentInChildren<Image>();
        signLight = GetComponentInChildren<Light>();
    }
    
    public void SignOff()
    {
        Setup();
        StartCoroutine(G.lightManager.FadeLight(signLight, 0f, 3f));
        StartCoroutine(FadeColor(signImage, zeroColor, 3f));
        StartCoroutine(FadeColor(quotaText, zeroColor, 2f));
    }
    public void SignOn()
    {
        Setup();
        StartCoroutine(G.lightManager.FadeLight(signLight, 0.5f, 3f));
        StartCoroutine(FadeColor(signImage, currentColor, 3f));
        StartCoroutine(FadeColor(quotaText, quotaTextColor, 0.1f));
    }
    
    public void Setup()
    {
        UpdateColor();
        quotaText.text = $"{G.progressManager.dayProgress}/{G.progressManager.dayGoal}";
    }

    public void UpdateColor()
    {
        if (G.progressManager.isDayCompleted)
        {
            currentColor = greenColor;
            signLight.color = greenGlowColor;
            StartCoroutine(FadeColor(signImage, greenColor, 3f));
        }
        else
        {
            currentColor = redColor;
            signLight.color = redGlowColor;
            StartCoroutine(FadeColor(signImage, redColor, 3f));
        }
    }

    IEnumerator FadeColor(Image image, Color endColor,  float duration)
    {
        Color startColor = image.color;
        float timer = 0f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            image.color = Color.Lerp(startColor, endColor, timer/duration);
            yield return null;
        }

        image.color = endColor;
    }
    IEnumerator FadeColor(TMP_Text text, Color endColor,  float duration)
    {
        Color startColor = text.color;
        float timer = 0f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            text.color = Color.Lerp(startColor, endColor, timer/duration);
            yield return null;
        }

        text.color = endColor;
    }
}
