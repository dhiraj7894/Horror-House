using HorroHouse.Player;
using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KitchenIslandDrawer : InteractBase, Interacter
{
    private MainPlayer _player;
    public float openPosition;
    public float closePosition;

    public float time = 1;
    public Collider[] col;
    private void Start()
    {
        _player = GameManager.Instance._PlayerObject;
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

    private void Update()
    {
        /*if (!_Heighlight)
            return;
        if (_player.playerController.interactBase == null)
            _Heighlight.enabled = false;*/
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
