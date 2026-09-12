using System;
using System.Collections;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light flashlight;
    public Light emergencyLight;

    public Lamp[] Lamps;

    void Awake()
    {
        Lamps = FindObjectsByType<Lamp>();
    }

    public bool isLightOn = false;
    public event Action OnLightsTurnedOff;
    public event Action OnLightsTurnedOn;

    private Coroutine flashlightCoroutine;

    public void TurnOffLights()
    {
        isLightOn = false;

        foreach (Lamp lamp in Lamps)
        {
            lamp.SetEnabled(false);
        }
        
        EnableFlashlight(true);
        
        StartCoroutine(FadeLight(emergencyLight, 5f, 0.5f));
        
        G.progressManager.quotaSign.SignOff();

        OnLightsTurnedOff?.Invoke();
    }
    public IEnumerator TurnOnLights()
    {
        isLightOn = true;

        yield return new WaitForSeconds(1f);

        foreach (Lamp lamp in Lamps)
        {
            lamp.SetEnabled(true);
        }

        EnableFlashlight(false);

        StartCoroutine(FadeLight(emergencyLight, 0f, 0.2f));
        
        G.progressManager.quotaSign.SignOn();

        OnLightsTurnedOn?.Invoke();
    }

    public void EnableFlashlight(bool on)
    {
        if (on)
        {
            if (flashlightCoroutine != null) StopCoroutine(flashlightCoroutine);
            flashlightCoroutine = StartCoroutine(FlashlightFlickering());
        }
        else
        {
            if (flashlightCoroutine != null) StopCoroutine(flashlightCoroutine);
            flashlight.enabled = false;
        }
    }

    IEnumerator FlashlightFlickering()
    {
        yield return new WaitForSeconds(1f);

        flashlight.enabled = true;

        yield return new WaitForSeconds(0.3f);

        flashlight.enabled = false;

        yield return new WaitForSeconds(0.08f);

        flashlight.enabled = true;

        yield return new WaitForSeconds(0.1f);

        flashlight.enabled = false;

        yield return new WaitForSeconds(0.06f);

        flashlight.enabled = true;
    }

    public IEnumerator FadeLight(Light light, float finalIntensity, float fadeTime)
    {
        float startIntensity = light.intensity;
        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            float t = timer / fadeTime;
            t = Mathf.SmoothStep(0f, 1f, t);

            light.intensity = Mathf.Lerp(startIntensity, finalIntensity, t);

            yield return null;
        }
        light.intensity = finalIntensity;
    }
}
