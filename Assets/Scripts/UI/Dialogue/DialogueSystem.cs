using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{

    [Header("Dialogue Settings:")]
    [SerializeField] private Text speakerName;
    [SerializeField] private Text speakerDescription;
    [SerializeField, Range(0, 5)] private float typingSpeed; 
    [SerializeField] private GameObject dialogueUI;

    [Header("Audio Settings:")]
    [SerializeField] private AudioSource dialogueConfirm;
    [SerializeField] private AudioSource typingSound;

    [Header("Dialogue Options Settings:")]
    [SerializeField] private Transform[] buttonPlaces;
    [SerializeField] private Button dialogueButtons;

    [Header("Events")]
    private readonly Action onDialogueComplete;

    // private variables
    public DialogueBox current_dialogue;
    private int current_line_index;
    private bool is_typing;
    private Coroutine type_routine;
    private readonly List<Button> active_dialogue_buttons = new();

    private void Awake()
    {
        StartDialogue(current_dialogue);
    }

    // Update is called once per frame
    private void Update()
    {
        if (current_dialogue == null) return;

        // Use this to skip or advance to next dialogue
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (is_typing)
                CompleteCurrentLine();
            else
            {
                dialogueConfirm.Play();
                AdvanceLine();
            }
        }
    }

    public void StartDialogue(DialogueBox dialogue)
    {
        // Starting dialogue
        current_dialogue = dialogue;
        current_line_index = 0;

        dialogueUI.SetActive(true);
        DisplayCurrentLine();
    }

    private void DisplayCurrentLine()
    {
        // If there's nothing more afterwards, end the dialogue!
        if (current_line_index >= current_dialogue.dialogueEntries.Length)
        {
            EndDialogue();
            return;
        }

        // show speaker name before advancing to typing
        var entry = current_dialogue.dialogueEntries[current_line_index];
        speakerName.text = entry.speakerName;

        // Stop the coroutine if there's nothing to display
        if (type_routine != null)
        {
            StopCoroutine(type_routine);
            type_routine = null;
        }

        // Let the typing commence
        type_routine = StartCoroutine(TypeLine(entry.speakerDescription, entry.speakerVoice));
    }

    private void CompleteCurrentLine()
    {
        if (type_routine != null)
        {
            StopCoroutine(type_routine);
            type_routine = null;
        }

        var entry = current_dialogue.dialogueEntries[current_line_index];
        speakerDescription.text = entry.speakerDescription;
        is_typing = false;
    }

    private void AdvanceLine()
    {
        current_line_index++;
        DisplayCurrentLine();
    }

    private void EndDialogue()
    {
        // If during the ending of the dialogue has a dialogue options in there. Show them to the player
        if (current_dialogue != null && current_dialogue.dialogueOptions.Length > 0)
        {
            ShowDialogueOptions();
            return;
        }

        // End the normal dialogue right here.
        speakerDescription.text = "";
        speakerName.text = "";

        dialogueUI.SetActive(false);
        onDialogueComplete?.Invoke();
        current_dialogue = null;
    }

    private void ShowDialogueOptions()
    {
        // Get the options in scriptable object
        var options = current_dialogue.dialogueOptions;
        
        for (var i = 0; i < options.Length; i++)
        {
            if (i >= buttonPlaces.Length)
                break;
            
            // 
            var button = Instantiate(dialogueButtons, buttonPlaces[i]);
            button.transform.localPosition = Vector3.zero;

            button.GetComponentInChildren<Text>().text = options[i].optionText;
            int option_index = i;
            
            button.onClick.AddListener(() =>
            {
               SelectDialogueOption(option_index); 
            });
            active_dialogue_buttons.Add(button);
        }
    }

    private void SelectDialogueOption(int option_index)
    {
        var option = current_dialogue.dialogueOptions[option_index];
        dialogueConfirm.Play();

        HideDialogueOptions();

        if (option.nextDialogue != null)
            StartDialogue(option.nextDialogue);
        else
            EndDialogue();
    }

    private void HideDialogueOptions()
    {
        foreach (var button in active_dialogue_buttons)
            Destroy(button.gameObject);
        
        active_dialogue_buttons.Clear();
    }

    private IEnumerator TypeLine(string dialogueLine, AudioClip voiceClip)
    {
        is_typing = true;
        speakerDescription.text = "";

        if (typingSound && voiceClip)
        {
            typingSound.Stop();
            typingSound.clip = voiceClip;
            typingSound.loop = true;
            typingSound.Play();
        }

        foreach(var character in dialogueLine)
        {
            speakerDescription.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (typingSound)
            typingSound.Stop();
        
        is_typing = false;
        type_routine = null;
    }
}
