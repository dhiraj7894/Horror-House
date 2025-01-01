using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioClip[] objectFall;
}

[System.Serializable]
public class AudioSources
{
    [Range(0, 1)] public float sfxVolume;
    public AudioSource sfxSource;

    [Range(0, 1)] public float dialougeVolume;
    public AudioSource dialougeSource;

    [Range(0, 1)] public float musicVolume;
    public AudioSource musicSource;
}
namespace HorroHouse
{
    public class AudioManager : Singleton<AudioManager>
    {
        public enum AudioType { 
            sfx = 0,
            dialouge = 1, 
            music = 2
        }


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
            DontDestroyOnLoad(this);
            AmbAudioPlaying(true);
        }


        public void AmbAudioPlaying(bool isTrue)
        {
            ambAudioPlaying = isTrue;
        }

        private void Update()
        {
            if(ambAudioPlaying) PlayAmbientAudio(audioSource.musicSource);
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
            audioSource.musicSource.volume = audioSource.musicVolume;
            audioSource.dialougeSource.volume = audioSource.dialougeVolume;
            audioSource.sfxSource.volume = audioSource.sfxVolume;

        }

        public void PlayPlayerAudio(AudioSource source,AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }
        AudioType val;

        public void SetAudioType(int type)
        {
            val = (AudioType)type;
        }
        public void SetVolume(Slider slider)
        {
            switch (val) {
                case AudioType.sfx:
                    audioSource.sfxVolume = slider.value;
                    return;
                case AudioType.dialouge:
                    audioSource.dialougeVolume = slider.value;
                    return;
                case AudioType.music:
                    audioSource.musicVolume = slider.value;
                    return;
            
            }

        }

        public void SaveVoulmeMeter()
        {
            audioSource.sfxSource.volume = audioSource.sfxVolume;
            audioSource.dialougeSource.volume = audioSource.dialougeVolume;
            audioSource.musicSource.volume = audioSource.musicVolume;
        }
    }
}
