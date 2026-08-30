using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{

    [Header("Player Interaction Settings:")]
    [SerializeField] private GameObject pressE;
    [SerializeField] private UnityEvent events;
    [SerializeField] private bool cantInteractAfterShift;
    [SerializeField] private bool canInteractAfterShift;
    [SerializeField] private UnityEvent afterInteractEvent;
 
    [Header("Sound Effects if Needed:")]
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip[] audioClips;

    [Header("If Needed, Animation Settings:")]
    [SerializeField] private Animator anim;
    [SerializeField] private string animString;

    [Header("Different Interact")]
    [SerializeField] private bool different_interaction;
    [SerializeField] private Animator fade;
    [SerializeField] private EventPlayer[] eventPlayer;

    private bool can_activate;
    private bool anim_bool = true;
    private bool can_activate_event = true;

    private int shift_day;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Has_Save") && cantInteractAfterShift)
            can_activate_event = false;
        if (PlayerPrefs.HasKey("Has_Save") && canInteractAfterShift)
            can_activate_event = true;
        else if (!PlayerPrefs.HasKey("Has_Save") && canInteractAfterShift)
            can_activate_event = false;
    }

    // Update is called once per frame
    private void Update()
    {

        if (PlayerPrefs.HasKey("Shift_Day"))
            shift_day = PlayerPrefs.GetInt("Shift_Day");
        
        if (anim != null)
            anim_bool = anim.GetBool(animString);
        if (Input.GetKeyDown(KeyCode.E) && can_activate)
            events?.Invoke();

        if (Input.GetKeyDown(KeyCode.E) && can_activate && different_interaction && PlayerPrefs.HasKey("Has_Save") && !PlayerPrefs.HasKey("Has_Shot") && PlayerPrefs.HasKey("Alive_Robots"))
        {
            fade.SetBool("Fade", true);
            eventPlayer[0].PlayEvent();
        }
        if (Input.GetKeyDown(KeyCode.E) && can_activate && different_interaction && PlayerPrefs.HasKey("Has_Save") && PlayerPrefs.HasKey("Has_Shot") && PlayerPrefs.HasKey("Alive_Robots"))
        {
            fade.SetBool("Fade", true);
            eventPlayer[1].PlayEvent();
        }
        if (Input.GetKeyDown(KeyCode.E) && can_activate && different_interaction && PlayerPrefs.HasKey("Has_Save") && !PlayerPrefs.HasKey("Has_Shot") && PlayerPrefs.HasKey("Alive_Robots") && shift_day >= 3)
        {
            fade.SetBool("Fade", true);
            eventPlayer[2].PlayEvent();
        } 
        if (shift_day > 3 && canInteractAfterShift)
        {
            afterInteractEvent?.Invoke();
        }           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && can_activate_event)
        {
            can_activate = true;
            if (pressE != null)
                pressE.SetActive(can_activate);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && can_activate_event)
        {
            can_activate = false;
            if (pressE != null)
                pressE.SetActive(can_activate);
        }
    }

    public void SetBoolAnim()
    {
        anim_bool = !anim_bool;
        anim.SetBool(animString, anim_bool);
    }

    public void DoorSound()
    {
        if (!anim_bool)
            soundSource.clip = audioClips[0];
        else
            soundSource.clip = audioClips[1];

        soundSource.Play();
    }
}