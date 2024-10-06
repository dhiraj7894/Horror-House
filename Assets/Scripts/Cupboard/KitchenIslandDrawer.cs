using HorroHouse.Player;
using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenIslandDrawer : InteractBase, Interacter
{
    public float openPosition;
    public float closePosition;

    public float time = 1;
    public Collider[] col;
    private void Start()
    {
        _UIText = "Open";
    }
    public void Interact()
    {
        UITextUpdate();
        if (transform.localPosition.x == closePosition)
        {
            LeanTween.moveLocalX(transform.gameObject, openPosition, time);
        }
        else
        {
            LeanTween.moveLocalX(transform.gameObject, closePosition, time);
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
