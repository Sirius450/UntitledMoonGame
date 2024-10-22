using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogBoxText;
    [SerializeField] Image actorPortrait;

    [SerializeField] GameObject dialogueBoxGameObject;

    private Dialogue currentDialogue;
    private int dialogueTextBlockIndex = 0;
    public Button nextDialogueButton;

    public static DialogueManager Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueTextBlockIndex = 0;
        nameText.text = dialogue.actorName;
        //dialogBoxText.text = dialogue.textBlocks[dialogueTextBlockIndex];

        StartCoroutine(DisplayTextOverTimer(dialogue.textBlocks[dialogueTextBlockIndex]));

        actorPortrait.sprite = dialogue.actorPortrait;

        dialogueTextBlockIndex++;

        currentDialogue = dialogue;


        dialogueBoxGameObject.SetActive(true);
    }

    public void NextTextBlock()
    {
        if (dialogueTextBlockIndex >= currentDialogue.textBlocks.Length)
        {
            dialogueBoxGameObject.SetActive(false);
            return;
        }

        StartCoroutine(DisplayTextOverTimer(currentDialogue.textBlocks[dialogueTextBlockIndex]));
        dialogueTextBlockIndex++;
    }

    IEnumerator DisplayTextOverTimer(string textBlock)
    {
        dialogBoxText.text = "";

        foreach (char character in textBlock)
        {
            dialogBoxText.text += character;
            yield return new WaitForSeconds(0.01f);
        }
    }

    
}
