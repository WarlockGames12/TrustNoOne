using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayer : MonoBehaviour
{

    [Header("References:")]
    [SerializeField] private DialogueSystem dialogue;
    [SerializeField] private Animator eventAnim;
    [SerializeField] private Transform spawnPoint;

    [Header("Event Player Settings:")]
    [SerializeField] private GameEvent[] events;
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool playOnEnable;
    public bool onEventEnd;

    // private variables
    private int current_event;
    private Coroutine event_coroutine;
    private readonly Dictionary<string, EventTarget> event_objects = new();

    private void Awake()
    {
        var scene_targets = FindObjectsByType<EventTarget>(FindObjectsInactive.Include);
       
        foreach (var tar in scene_targets)
        {
            if (!event_objects.ContainsKey(tar.ID))
                event_objects.Add(tar.ID, tar);
        }

        dialogue = FindAnyObjectByType<DialogueSystem>(FindObjectsInactive.Include);
        onEventEnd = false;
    }

    private void OnEnable()
    {
        if (playOnEnable)
            PlayEvent();
    }

    private void Start()
    {
        if (playOnStart)
            PlayEvent();
    }

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

        onEventEnd = true;
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

    public EventTarget GetTarget(string id)
    {
        if (event_objects.TryGetValue(id, out var event_obj))
            return event_obj;
        
        Debug.LogWarning($"Event target: '{id}' could not be found.");
        return null;
    }

    public Transform GetTransform()
    {
        return spawnPoint;
    }
}
