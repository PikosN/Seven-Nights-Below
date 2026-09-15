using System.Collections;
using UnityEngine;

public class SleepAnimation : MonoBehaviour
{
    public Transform playerTransform;
    
    public Transform lyingPosition;
    public Transform sittingPosition;
    public Transform standingPosition;

    public AudioSource doorAudioSource;
    public AudioClip knockSound;

    private bool isAnimating = false;

    public IEnumerator GoToSleep()
    {
        if (isAnimating)
        {
            yield break;
        }
        isAnimating = true;
        
        G.lightManager.EnableFlashlight(false);
        
        G.UIManager.SetUIEnabled(false);
        G.player.SetMovementEnabled(false);

        standingPosition.position = playerTransform.position;
        standingPosition.rotation = playerTransform.rotation;

        yield return MoveCamera(standingPosition, sittingPosition, 0.5f);
        yield return new WaitForSeconds(0.1f);
        yield return MoveCamera(sittingPosition, lyingPosition, 0.5f);
        yield return new WaitForSeconds(5f);

        isAnimating = false;
    }
    public IEnumerator Wakeup()
    {
        if (isAnimating)
        {
            yield break;
        }
        isAnimating = true;

        G.player.SetMovementEnabled(false);

        playerTransform.position = lyingPosition.position;
        playerTransform.rotation = lyingPosition.rotation;

        standingPosition.rotation = Quaternion.Euler(0f, 0f, 0f);
        
        doorAudioSource.PlayOneShot(knockSound);
        yield return new WaitForSeconds(1f);
        yield return MoveCamera(lyingPosition, sittingPosition, 0.5f);
        
        if (G.prestigeManager.prestigeLevel == 0)
        {
            yield return new WaitForSeconds(1.2f);
            G.UIManager.SetCutsceneUIEnabled(true);
            yield return G.dialogueManager.PlayLine("A voice behind the door", "Did you wake up?");
            G.lightManager.EnableFlashlight(true);
            yield return G.dialogueManager.PlayLine("A voice behind the door", "...");
            yield return G.dialogueManager.PlayLine("A voice behind the door", "Get up. Turn on the light on the right.");
            yield return MoveCamera(sittingPosition, standingPosition, 0.5f);
            G.player.ResetCamera(1f);
            G.player.SetMovementEnabled(true);
            G.UIManager.SetUIEnabled(true);
            isAnimating = false;
            yield return G.dialogueManager.PlayLine("A voice behind the door", "Plant and harvest plants.");
            yield return G.dialogueManager.PlayLine("A voice behind the door", "You can buy upgrades at the computer.");
            yield return G.dialogueManager.PlayLine("A voice behind the door", "Don't try to open the door.");
            yield return G.dialogueManager.PlayLine("A voice behind the door", "Go to sleep after meeting the quota.");
            yield return G.dialogueManager.PlayLine("A voice behind the door", "Good luck.");
        }
        else
        {
            yield return MoveCamera(sittingPosition, standingPosition, 0.5f);
            G.player.ResetCamera(1f);

            G.player.SetMovementEnabled(true);
            G.UIManager.SetUIEnabled(true);
            isAnimating = false;
        }
    }

    private IEnumerator MoveCamera(Transform from, Transform to, float duration)
    {
        float time = 0f;
        
        Vector3 startPosition = from.position;
        Quaternion startRotation = from.rotation;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / duration);

            t = Mathf.SmoothStep(0f, 1f, t);

            playerTransform.position = Vector3.Lerp(startPosition, to.position, t);
            playerTransform.rotation = Quaternion.Slerp(startRotation, to.rotation, t);

            G.player.cameraTransform.rotation = Quaternion.Slerp(startRotation, to.rotation, t);

            yield return null;
        }

        playerTransform.position = to.position;
        playerTransform.rotation = to.rotation;
    }
}
