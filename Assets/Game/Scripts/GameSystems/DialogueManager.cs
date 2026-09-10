using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public TMP_Text speakerText;
    public TMP_Text dialogueText;

    private bool isTyping;

    public float typingSpeed = 0.03f;

    void Start()
    {
        G.UIManager.SetCutsceneUIEnabled(false);
    }

    void Update()
    {
        if (isTyping && Keyboard.current.spaceKey.isPressed)
        {
            isTyping = false;
        }
    }

    public IEnumerator PlayLine(string speaker, string line)
    {
        dialogueUI.SetActive(true);

        speakerText.text = speaker;

        yield return StartCoroutine(TypeText(line));
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;

        dialogueText.text = "";
        
        yield return new WaitForSeconds(0.5f);
        foreach (var character in line)
        {
            if (!isTyping)
            {
                dialogueText.text = line;
                break;
            }

            dialogueText.text += character;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        yield return new WaitForSeconds(0.5f);
        G.UIManager.SetCutsceneUIEnabled(false);
        dialogueText.text = "";
    }
}
