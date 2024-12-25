using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivity : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mouseValue;
    public void SetText(Slider slider)
    {
        mouseValue.text = slider.value.ToString("F1");
    }
}
