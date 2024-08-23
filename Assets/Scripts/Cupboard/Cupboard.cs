using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboard : InteractBase, Interacter
{
    public Transform hidePosition;
    public Transform exitPosition;
    public Animator anim;
    
    public MainPlayer _Player;
    public Vector3 playerRotationDiretion;

    public float time = 1;
    public Collider[] col;
    private void Start()
    {
        _Player = GameManager.Instance._PlayerObject;
        _UIText = "Hide";
    }
    public void Interact()
    {
        UITextUpdate();
        anim.Play("Open");
        if (_Player.transform.parent != hidePosition)
        {
            foreach (Collider c in col)
            {
                c.enabled = false;
            }
            _Player.transform.parent = hidePosition;
            LeanTween.moveLocal(_Player.gameObject, new Vector3(0, 0, 0), time).setOnComplete(() =>{
                LeanTween.rotateLocal(_Player.gameObject, playerRotationDiretion, time/2);
                foreach(Collider c in col)
                {
                    c.enabled = true;
                }
            });
            _Player.controller.enabled = false;
            _Player.gameObject.layer = 0;
        }
        else if(_Player.transform.parent == hidePosition)
        {
            _Player.transform.parent = null;
            foreach (Collider c in col)
            {
                c.enabled = false;
            }
            LeanTween.move(_Player.gameObject, exitPosition.position, time).setOnComplete(() =>
            {
                
                foreach (Collider c in col)
                {
                    c.enabled = true;
                }
            });
            _Player.gameObject.layer = 8;
            _Player.controller.enabled = true;
        }
    }

    public void Drop()
    {
        
    }

    void UITextUpdate()
    {
        if (_UIText.Contains("Hide"))
            _UIText = "UnHide";
        else
            _UIText = "Hide";
    }
}
