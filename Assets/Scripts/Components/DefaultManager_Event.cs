using UnityEngine;
using UnityEngine.Events;

public enum DefaultManager_Command
{
    Load,
    Set,
    FirstSet
}

[System.Serializable]
public class DefaultManager_Event : UnityEvent<DefaultManager_Command>
{
    
}
