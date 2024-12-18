using HorroHouse;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Introduction : QuestStep
{
    public TextAsset inkJSON;
    public float playbackSpeed = 1;
    public VideoPlayer videoPlayer;
    public GameObject cutSceneScreen;
    private void Start()
    {
        videoPlayer.playbackSpeed = playbackSpeed;
        GameManager.Instance.CutSceneStatus(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += StartDialouge;
    }

    private void StartDialouge(VideoPlayer source)
    {
        videoPlayer.Stop();
        cutSceneScreen.SetActive(false);
        DialogueManager.Instance.EnterFullScreenDialogueMode(inkJSON, this);
    }

}
