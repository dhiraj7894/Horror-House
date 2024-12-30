using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioBank
{
    public AudioClip[] walkOnRubber;
    public AudioClip[] ambient;
    public AudioClip[] doorWontBudge;
    public AudioClip[] doorOpen;
    public AudioClip[] doorClose;
    public AudioClip[] lift;
    public AudioClip[] cupboard;
    public AudioClip[] taskSelected;
    public AudioClip[] shelf;
    public AudioClip[] drawer;
}

[System.Serializable]
public class AudioSources
{
    [Range(0, 1)] public float baseAudioVol;
    public AudioSource baseAudioSource;

    [Range(0, 1)] public float playerAudioVol;
    public AudioSource playerAudioSource;

    [Range(0, 1)] public float ambAudioVol;
    public AudioSource ambAudioSource;
}
namespace HorroHouse
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSources audioSource;
        public AudioBank audioBank;

        public bool ambAudioPlaying = false;

        public AudioClip GetAudio(AudioClip[] clipList)
        {
            int randomNumber = Random.Range(0, clipList.Length);
            return clipList[randomNumber];
        }

        private void Start()
        {
            SetAudioLevels();
        }


        public void AmbAudioPlaying(bool isTrue)
        {
            ambAudioPlaying = isTrue;
        }

        private void Update()
        {
            if(ambAudioPlaying) PlayAmbientAudio(audioSource.ambAudioSource);
        }
        public void PlayAmbientAudio(AudioSource source)
        {
            if (source.isPlaying)
                return;
            source.PlayOneShot(GetAudio(audioBank.ambient));
        }
        public void StopAmbientAudio(AudioSource source)
        {            
            source.Stop();
        }

        public void SetAudioLevels()
        {
            audioSource.ambAudioSource.volume = audioSource.ambAudioVol;
            audioSource.playerAudioSource.volume = audioSource.playerAudioVol;
            audioSource.baseAudioSource.volume = audioSource.baseAudioVol;

        }

        public void PlayPlayerAudio(AudioSource source,AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }
    }
}
