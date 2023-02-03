using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    public string name;
    [TextArea(5,10)] public string dialogueText;
}

public class DialogueEntity : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialogue[] dialogue;
    [SerializeField] private GameObject dialogueModalUIPrefab;
    private GameObject dialogueObject;
    //private DialogueModalUI dialogueUI;
    public bool isOpen = false;
    private int dialogueIndex = 0;
    public bool ObjectDestroyed = false;
    public bool DialogueEnd = false;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return) && isOpen == true && ObjectDestroyed == false)
    //    {
    //        NextDialogue();
    //    }
    //}

    public void ExecuteInteractable()
    {
        if (!isOpen)
        {
            isOpen = true;

            //dialogueObject = Instantiate(dialogueModalUIPrefab);
            //dialogueUI = dialogueObject.GetComponent<DialogueModalUI>();
            //dialogueUI.SetDialogueUI(dialogues[0]);
            //dialogueUI.OnNextDialogueButtonPressed += NextDialogue;
            //InGameTracker.instance.gameState = GameplayState.Dialogue;
        }
    }

    //private void NextDialogue()
    //{
    //    dialogueIndex += 1;

    //    if (dialogueIndex <= dialogues.Length - 1)
    //        dialogueUI.SetDialogueUI(dialogues[dialogueIndex]);
    //    else
    //    {
    //        EndDialogue();
    //    }

    //    dialogues[dialogueIndex].onCurrentDialogueEvent?.Invoke();
    //}

    //private void EndDialogue()
    //{
    //    dialogueUI.OnNextDialogueButtonPressed -= NextDialogue;
    //    dialogueObject.SetActive(false);
    //    ObjectDestroyed = true;
    //    DialogueEnd = true;
    //    InGameTracker.instance.gameState = GameplayState.Playing;
    //    dialogueIndex = 0;
    //}
}
