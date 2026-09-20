using System.Collections;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public Renderer lightsRenderer;
    public Light spotLightSource;
    public Light pointLightSource;
    public AudioSource lampAudioSource;
    public AudioClip offSound;

    private Material materialOfLampBase;
    private Material materialOfLamp;

    private Color emissionColor = new Color32(94, 0, 95, 255);
    private float emissionIntensity = 1f;

    void Awake()
    {
        materialOfLampBase = lightsRenderer.materials[0];
        materialOfLamp = lightsRenderer.materials[1];
    }

    public void SetEnabled(bool enabled)
    {
        spotLightSource.enabled = enabled;
        pointLightSource.enabled = enabled;

        if (enabled)
        {
            StartCoroutine(SmoothVolumeTransition(lampAudioSource, 0.75f, 0.5f));

            materialOfLamp.SetColor("_BaseColor", new Color32(255, 0, 217, 255));
            materialOfLamp.SetColor("_EmissionColor", emissionColor * emissionIntensity);
            materialOfLampBase.SetColor("_BaseColor", new Color32(231, 231, 231, 255));
            StartCoroutine(G.lightManager.FadeLight(spotLightSource, 10f, 1f));
            StartCoroutine(G.lightManager.FadeLight(pointLightSource, 4f, 1f));
        }
        else
        {
            lampAudioSource.PlayOneShot(offSound, 1f);
            StartCoroutine(SmoothVolumeTransition(lampAudioSource, 0f, 0.75f));

            materialOfLamp.SetColor("_BaseColor", new Color32(127, 127, 127, 255));
            materialOfLamp.SetColor("_EmissionColor", Color.black * 0.1f);
            materialOfLampBase.SetColor("_BaseColor", new Color32(82, 82, 82, 255));
            StartCoroutine(G.lightManager.FadeLight(spotLightSource, 0f, 1f));
            StartCoroutine(G.lightManager.FadeLight(pointLightSource, 0f, 1f));
        }
    }

    IEnumerator SmoothVolumeTransition(AudioSource audioSource, float endVolume, float transitionTime)
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer <= transitionTime)
        {
            timer += Time.deltaTime;

            float t = timer / transitionTime;

            audioSource.volume = Mathf.Lerp(startVolume, endVolume, t);

            yield return null;
        }
    }
}
