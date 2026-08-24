using System.Collections;
using UnityEngine;

public class EventPlayer : MonoBehaviour
{

    [Header("References:")]
    [SerializeField] private DialogueSystem dialogue;
    [SerializeField] private Animator eventAnim;

    [Header("Event Player Settings:")]
    [SerializeField] private GameEvent[] events;

    // private variables
    private int current_event;
    private Coroutine event_coroutine;

    public void PlayEvent()
    {
        if (event_coroutine != null)
            return;
        
        current_event = 0;
        event_coroutine = StartCoroutine(PlayEvents());
    }

    private IEnumerator PlayEvents()
    {
        while (current_event < events.Length)
        {
            if (events[current_event] != null)
                yield return events[current_event].Execute(this);

            current_event++;
        }

        event_coroutine = null;
    }

    public DialogueSystem GetDialogueSystem()
    {
        return dialogue;
    }

    public Animator GetAnimator()
    {
        return eventAnim;
    }
}
