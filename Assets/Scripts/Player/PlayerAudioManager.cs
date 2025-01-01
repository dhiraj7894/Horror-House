using HorroHouse;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public void PlayOnWalk()
    {
        AudioManager.Instance.audioSource.sfxSource.PlayOneShot(AudioManager.Instance.GetAudio(AudioManager.Instance.audioBank.walkOnRubber));
    }
}
