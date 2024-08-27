using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfDoor : InteractBase, Interacter
{
    public float openRotation;
    public float closeRotation;

    public float time = 1;
    public Collider[] col;
    private void Start()
    {
        _UIText = "Open";
    }
    public void Interact()
    {
        UITextUpdate();
        if (transform.localEulerAngles.y == closeRotation)
        {
            LeanTween.rotateY(transform.gameObject, openRotation, time);
        }
        else
        {
            LeanTween.rotateY(transform.gameObject, closeRotation, time);
        }
    }

    public void Drop()
    {

    }

    void UITextUpdate()
    {
        if (_UIText.Contains("Open"))
            _UIText = "Close";
        else
            _UIText = "Open";
    }
}
