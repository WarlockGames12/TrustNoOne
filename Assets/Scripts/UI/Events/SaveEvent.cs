using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Save or Remove Event")]
public class SaveEvent : GameEvent
{
    public enum SavingOrRemovingPrefs
    {
        Save,
        Remove
    }

    [Header("Saving Settings:")]
    [SerializeField] private string playerPrefsString;
    [SerializeField] private SavingOrRemovingPrefs savingOrRemove;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        switch (savingOrRemove)
        {
            case SavingOrRemovingPrefs.Save:
                PlayerPrefs.SetInt(playerPrefsString, 1);
                break;
            case SavingOrRemovingPrefs.Remove:
                PlayerPrefs.DeleteKey(playerPrefsString);
                break;
        }

        yield return null;
    }
}
