using HorroHouse;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;

public enum CanCTInteract
{
    no,
    yes

}
public class InteractBase : MonoBehaviour
{
    //public Outline _Heighlight;   
    public Animator _Animator;
    public DoorName _DoorName;
    public CanCTInteract _CanCTInteract;
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

    public void ClosedDoorAudio()
    {        
        AudioManager.Instance.PlayPlayerAudio(AudioManager.Instance.audioSource.playerAudioSource, 
            AudioManager.Instance.GetAudio(AudioManager.Instance.audioBank.doorWontBudge
            )); 
    }

    public void OpenTheDoorAudio()
    {        
        AudioManager.Instance.PlayPlayerAudio(AudioManager.Instance.audioSource.playerAudioSource, 
            AudioManager.Instance.GetAudio(AudioManager.Instance.audioBank.doorOpen
            ));
    }

    public void CloseTheDoorAudio()
    {
        AudioManager.Instance.PlayPlayerAudio(AudioManager.Instance.audioSource.playerAudioSource, 
            AudioManager.Instance.GetAudio(AudioManager.Instance.audioBank.doorClose
            ));
    }

    public void OpenTheShelfAudio()
    {
        AudioManager.Instance.PlayPlayerAudio(AudioManager.Instance.audioSource.playerAudioSource,
            AudioManager.Instance.audioBank.shelf[0]);
    }

    public void CloseTheShelfAudio()
    {
        AudioManager.Instance.PlayPlayerAudio(AudioManager.Instance.audioSource.playerAudioSource, AudioManager.Instance.audioBank.shelf[1]);
    }
    public void OpenTheDrawerAudio()
    {
        AudioManager.Instance.PlayPlayerAudio(AudioManager.Instance.audioSource.playerAudioSource,
            AudioManager.Instance.audioBank.drawer[0]);
    }

    public void CloseTheDrawerAudio()
    {
        AudioManager.Instance.PlayPlayerAudio(AudioManager.Instance.audioSource.playerAudioSource, AudioManager.Instance.audioBank.drawer[1]);
    }
}
