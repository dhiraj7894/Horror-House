using HorroHouse;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SubtitleManager : Singleton<SubtitleManager>
{    
    public AudioSource audioSource;    
    public VoiceLineSO voiceLineSO;

    [Header("Dialogue UIs")]
    public GameObject dialogueUI;    
    public TextMeshProUGUI dialogueText;

    public float audioLengthIncreser = .35f;

    public bool isAudioPlaying = false;
    public UnityEvent executeOnSubtitleStarts;

    private void Start()
    {
        audioSource = GameObject.Find("Dialouge").GetComponent<AudioSource>();
    }

    public void GetSubtitleTextsData(SubtitleTexts data)
    {
        PlayAudioForCutScene(data);        
        //Debug.Log($"Data executed : {data.subtitleText}");
    }


    private void PlayAudioForCutScene(SubtitleTexts data)
    {
        GameManager.Instance.CutSceneStatus(data.isCutscene);
        if (data.isCutscene) LeanTween.rotate(GameManager.Instance._PlayerObject.gameObject, data.playerRotation, .3f).setEaseInCubic();
        executeOnSubtitleStarts = GameManager.Instance.onSubtitleStarts[data.eventStartNumber];
        if (data.triggerEvent)
        {
            Debug.Log("Subtitle End");
            executeOnSubtitleStarts?.Invoke();
        }
        dialogueUI.SetActive(true);
        dialogueText.text = data.subtitleText;

        audioSource.clip = data.audioClip;
        audioSource.Play();
        if (data.isAutoContinue)
        {
            LeanTween.delayedCall((audioSource.clip.length + .1f), () => { 
                PlayAudioForCutScene(data.nextSubtitle); 
            });
            return;
        }
        LeanTween.delayedCall((audioSource.clip.length + .1f), () => {
            StopPlaying();
        });
        isAudioPlaying = true;
    }

    private void StopPlaying()
    {
        GameManager.Instance.CutSceneStatus(false);
        dialogueUI.SetActive(false);
        dialogueText.text = "";
        audioSource.Stop();
        isAudioPlaying = false;
        Debug.Log("Subtitle Stopped");
    }

    private void Update()
    {        
       /* if (isAudioPlaying && audioSource.time >= audioSource.clip.length)
        {           
            StopPlaying();
        }*/
    }
}
