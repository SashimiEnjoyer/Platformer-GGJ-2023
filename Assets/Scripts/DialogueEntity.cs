using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct Dialogue
{
    public string name;
    [TextArea(5,10)] public string dialogueText;
    public UnityEvent onCurrentDialogueEvent;
}

public class DialogueEntity : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject dialogueModalUIPrefab;
    [SerializeField] private TMP_Text charaName;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button nextBtn;
    private GameObject dialogueObject;
    public bool isOpen = false;
    private int dialogueIndex = 0;
    public bool ObjectDestroyed = false;
    public bool DialogueEnd = false;
    [SerializeField] private Dialogue[] dialogue;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isOpen && !ObjectDestroyed)
        {
            NextDialogue();
        }
    }

    public void ExecuteInteractable()
    {
        if (!isOpen)
        {
            isOpen = true;

            dialogueObject.SetActive(true);
            SetDialogueUI(dialogue[0]);
            nextBtn.onClick.AddListener(NextDialogue);
            InGameTracker.instance.state = GameState.Dialogue;
        }
    }

    private void SetDialogueUI(Dialogue dialogue)
    {
        charaName.text = name;
        dialogueText.text = dialogue.dialogueText;
    }

    private void NextDialogue()
    {
        dialogueIndex += 1;

        if (dialogueIndex <= dialogue.Length - 1)
            SetDialogueUI(dialogue[dialogueIndex]);
        else
        {
            EndDialogue();
            return;
        }

        dialogue[dialogueIndex].onCurrentDialogueEvent?.Invoke();
    }

    private void EndDialogue()
    {
        dialogueObject.SetActive(false);
        ObjectDestroyed = true;
        DialogueEnd = true;
        InGameTracker.instance.state = GameState.Dialogue;
        dialogueIndex = 0;
    }
}
