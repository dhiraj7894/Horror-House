using System;
using System.Security.Cryptography;
using HorroHouse;
using UnityEngine.Events;


[Serializable]
public class EventForTaskComplete
{
    #region EventsForStory
    public UnityEvent Fuse1Connected;
    public UnityEvent Fuse2Connected;
    public UnityEvent FrameUnlocked;
    public UnityEvent RotationPuzzleUnlocked;
    public UnityEvent BasementDoorUnlocked;
    public UnityEvent GarageDoorUnlocked;
    public UnityEvent PasswordPuzzleUnlocked;
    public UnityEvent TempleDoorUnlocked;
    public UnityEvent CarStarted;
    #endregion
}

[Serializable]
public class EventForTask
{
    #region EventsForStory
    public UnityEvent CutSceneCompleted;


    public UnityEvent GotFuse1;
    public UnityEvent GotFuse2; 
    public UnityEvent GotBasementKey;
    public UnityEvent GotTarraceKey;
    public UnityEvent GotCarKey;
    public UnityEvent GotGarageKey;
    public UnityEvent GotHanumanChalisaBook;


    public UnityEvent ClickedGenrator;
    public UnityEvent ClickedGarageDoor;
    public UnityEvent ClickedBasementDoor;
    public UnityEvent ClickedTempleDoor;
    public UnityEvent ClickedTarraceDoor;
    public UnityEvent ClickedBedroomDoor;
    #endregion
}


public class EventManager : Singleton<EventManager>
{
    public EventForTaskComplete eventForTaskComplete;
    public EventForTask eventForTask;

    public event Action PressFButton;
    public event Action PressGButton;
    public static event Action isPlayerInteracting;

    
    public void PressedFButton()
    {
        PressFButton?.Invoke();
    }
    public void PressedGButton()
    {
        PressGButton?.Invoke();
    }

}
