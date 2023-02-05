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
    [SerializeField] private GameObject choiceGameObject;
    [SerializeField] private TMP_Text charaName;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button nextBtn;
    public bool isOpen = false;
    private int dialogueIndex = 0;
    public bool ObjectDestroyed = false;
    public bool DialogueEnd = false;
    public bool isChoice = false;
    [SerializeField] private Dialogue[] dialogue;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isOpen && !ObjectDestroyed && !isChoice)
        {
            NextDialogue();
        }
    }

    [ContextMenu("Test Diaolgue")]
    public void ExecuteInteractable()
    {
        if (!isOpen)
        {
            isOpen = true;

            dialogueModalUIPrefab.SetActive(true);
            SetDialogueUI(dialogue[0]);
            nextBtn.onClick.AddListener(NextDialogue);
            InGameTracker.instance.state = GameState.Dialogue;
        }
    }

    private void SetDialogueUI(Dialogue dialogue)
    {
        
        dialogueText.text = dialogue.dialogueText;
        
        charaName.text = dialogue.name;
    
    }

    public void NextDialogue()
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

    public void JumpDialogueIndex(int index)
    {
        dialogueIndex = index - 1;
        NextDialogue();

    }

    public void ShowChoice(bool _isChoice)
    {
        isChoice = _isChoice;
        choiceGameObject.SetActive(isChoice);
    }

    public void EndDialogue()
    {
        dialogueModalUIPrefab.SetActive(false);
        ObjectDestroyed = true;
        DialogueEnd = true;
        InGameTracker.instance.state = GameState.Dialogue;
        dialogueIndex = 0;
    }
}
