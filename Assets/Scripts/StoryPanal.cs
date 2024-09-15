using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryPanal : MonoBehaviour
{
    public GameObject Background;
    public TextMeshProUGUI story;

    public void SetData(bool isTrue, string data)
    {
        Background.SetActive(isTrue);
        story.text = data;  
    }
}
