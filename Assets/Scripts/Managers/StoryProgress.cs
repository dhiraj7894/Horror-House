using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class sVideoPlayer
{
    public VideoPlayer vPlayer;
    public GameObject vScreen;
}

public class StoryProgress : MonoBehaviour
{
    public sVideoPlayer videoPlayer;

    public void StartPlayingVideo(VideoClip clip)
    {
        videoPlayer.vPlayer.clip = clip;
        videoPlayer.vScreen.SetActive(true);
        videoPlayer.vPlayer.Play();
        GameManager.Instance.CutSceneStatus(true);
        videoPlayer.vPlayer.loopPointReached += VideoComplete;
    }

    public void VideoComplete(VideoPlayer source)
    {
        GameManager.Instance.CutSceneStatus(false);
        videoPlayer.vScreen.SetActive(false);
        videoPlayer.vPlayer.Stop();
    }
}
