using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public GameObject finalScreen;
    public AudioSource doorAudioSource;
    public AudioClip creakyDoorOpenSound;

    void Awake()
    {
        finalScreen = GameObject.Find("FinalScreen");
        doorAudioSource = GetComponentInChildren<AudioSource>();
    }

    public string GetInteractText()
    {
        if (G.prestigeManager.prestigeLevel == 3)
        {
            return "Open?";
        }
        else
        {
            return "Closed. It won't open";
        }
    }

    IEnumerator Open()
    {
        G.player.SetMovementEnabled(false);
        doorAudioSource.PlayOneShot(creakyDoorOpenSound);
        yield return new WaitForSeconds(0.1f);
        finalScreen.SetActive(true); 
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }

    public void Interact()
    {
        if (G.prestigeManager.prestigeLevel == 3)
        {
            StartCoroutine(Open());
        }
    }
}
