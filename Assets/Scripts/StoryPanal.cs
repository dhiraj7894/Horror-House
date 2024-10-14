using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPanal : MonoBehaviour
{
    public GameObject Background;
    public Text story;

    public void SetData(bool isTrue, string data)
    {
        Background.SetActive(isTrue);
        story.text = data;  
    }
}
