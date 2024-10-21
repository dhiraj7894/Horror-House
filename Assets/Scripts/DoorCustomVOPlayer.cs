using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCustomVOPlayer : MonoBehaviour
{
    public SubtitleTexts subtitleTexts;
    private bool isSoundPlayed = false;

    public void PlaySound()
    {
        if(!isSoundPlayed) SubtitleManager.Instance.GetSubtitleTextsData(subtitleTexts);
        isSoundPlayed = true;
    }


}
