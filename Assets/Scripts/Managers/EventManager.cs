using System;
using System.Security.Cryptography;
using HorroHouse;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public event Action PressFButton;
    public event Action PressGButton;
    public static event Action SpecialAttackEnd;
    public static event Action OrbCollected;
    public static event Action CutSceneChange;

    public static event Action isPlayerInteracting;

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
    public void PressedFButton()
    {
        PressFButton?.Invoke();
    }
    public void PressedGButton()
    {
        PressGButton?.Invoke();
    }

    public static void OnSpecialAttackEnd()
    {
        SpecialAttackEnd?.Invoke();
    }

    public static void OnOrbCollected()
    {
        OrbCollected?.Invoke();
    }

    public static void OnCutSceneChange()
    {
        CutSceneChange?.Invoke();
    }

    public void FuseOneConnected()
    {
        Fuse1Connected?.Invoke();     
    }
    public void FuseTwoConnected()
    {        
        Fuse2Connected?.Invoke();
    }
    public void PhotoFrameUnlocked()
    {
        FrameUnlocked?.Invoke();
    }

    public static void isPlayerInteracted()
    {
        isPlayerInteracting?.Invoke();
    }
}
