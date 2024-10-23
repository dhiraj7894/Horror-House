using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InteractBase : MonoBehaviour
{
    public Outline _Heighlight;   
    public Animator _Animator;
    public DoorName _DoorName;
    public bool isLocked = false;
    public string _UIText;
    public string _LockedText;
    public UnityEvent unityEvent;

    public void DisableLock()
    {
        isLocked = false;
    }
    public void IsLocked(bool isTrue)
    {
        isLocked = isTrue;
    }
    public void CheckTheDoorStatus()
    {
        Debug.Log("Interacted");
        switch (_DoorName)
        {
            case DoorName.None: break;
            case DoorName.GarageDoor: EventManager.Instance.eventForTask.ClickedGarageDoor?.Invoke(); break;
            case DoorName.BasementDoor: EventManager.Instance.eventForTask.ClickedBasementDoor?.Invoke(); break;
            case DoorName.TempleDoor: EventManager.Instance.eventForTask.ClickedTempleDoor?.Invoke(); break;
            case DoorName.TarraceDoor: EventManager.Instance.eventForTask.ClickedTarraceDoor?.Invoke(); break;
            case DoorName.BedroomDoor: EventManager.Instance.eventForTask.ClickedBedroomDoor?.Invoke(); break;
        }
    }
}
