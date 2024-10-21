using HorroHouse;
using System.Data;
using TMPro;
using UnityEngine;

public class SubtitleManager : Singleton<SubtitleManager>
{    
    public AudioSource audioSource;    
    public VoiceLineSO voiceLineSO;

    [Header("Dialogue UIs")]
    public GameObject dialogueUI;    
    public TextMeshProUGUI dialogueText;

    public float audioLengthIncreser = .35f;

    public bool isAudioPlaying = false;

    public void GetSubtitleTextsData(SubtitleTexts data)
    {
        PlayAudioForCutScene(data);        
    }


    private void PlayAudioForCutScene(SubtitleTexts data)
    {
        GameManager.Instance.CutSceneStatus(data.isCutscene);
        dialogueUI.SetActive(true);
        dialogueText.text = data.text;

        audioSource.clip = data.audioClip;
        audioSource.Play();
        if (data.isContinue)
        {
            LeanTween.delayedCall((audioSource.clip.length + .1f), () => { PlayAudioForCutScene(data.nextStep); });
            return;
        }
        isAudioPlaying = true;
    }

    private void StopPlaying()
    {
        dialogueUI.SetActive(false);
        dialogueText.text = "";
        audioSource.Stop();
        isAudioPlaying = false;
       
    }

    private void Update()
    {
        
        if (isAudioPlaying && audioSource.time >= audioSource.clip.length)
        {            
            StopPlaying();
        }
    }
}
