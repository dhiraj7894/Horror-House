using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    
    [SerializeField] UnityEvent toggleOn;
    [SerializeField] UnityEvent toggleOff;


    UnityEngine.UI.Toggle toggle;
    Vector2 handlePosition;


    private void Awake()
    {
        toggle = GetComponent<UnityEngine.UI.Toggle>();
        toggle.isOn = true;
        handlePosition = uiHandleRectTransform.anchoredPosition;
        toggle.onValueChanged.AddListener(OnSwitch);
        if (toggle.isOn)
            OnSwitch(true);
    }
    
    void OnSwitch(bool on)
    {
        if(on)
        {
            uiHandleRectTransform.anchoredPosition = handlePosition * -1;
            toggleOn?.Invoke();
        }
        else
        {
            uiHandleRectTransform.anchoredPosition = handlePosition;
            toggleOff?.Invoke();
        }
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
