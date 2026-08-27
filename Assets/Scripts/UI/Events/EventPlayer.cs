using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventPlayer : MonoBehaviour
{

    [Header("References:")]
    [SerializeField] private DialogueSystem dialogue;
    [SerializeField] private Animator eventAnim;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioSource audioEvent;

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
        {
            onEventEnd = false;
            current_event = 0;
            PlayEvent();
        }
    }

    private void Start()
    {
        if (playOnStart)
            PlayEvent();
    }

    public void PlayEvent()
    {
        if (event_coroutine != null)
        {
            StopCoroutine(event_coroutine);
            event_coroutine = null;
        }
        
        current_event = 0;
        event_coroutine = StartCoroutine(PlayEvents());
    }

    public void RestartEvent()
    {
        if (event_coroutine != null)
        {
            StopCoroutine(event_coroutine);
            event_coroutine = null;
        }

        current_event = 0;
        onEventEnd = false;

        event_coroutine = StartCoroutine(PlayEvents());
    }

    public IEnumerator PlayEvents()
    {
        while (current_event < events.Length)
        {
            if (events[current_event] != null)
            {
                Debug.Log(events[current_event]);
                yield return events[current_event].Execute(this);
            }
                

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

    public AudioSource GetSource()
    {
        return audioEvent;
    }
}
