using System.Collections;
using UnityEngine;

public abstract class GameEvent : ScriptableObject
{
    public abstract IEnumerator Execute(EventPlayer eventPlayer);
}

