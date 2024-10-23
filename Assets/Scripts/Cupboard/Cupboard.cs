using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Cupboard : InteractBase, Interacter
{
    public bool isHideable = true;
    public Transform hidePosition;
    public Transform exitPosition;
    public Animator anim;
    
    private MainPlayer _Player;
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
        if (isHideable)
        {
            HideInCupboard();            
        }
        else
        {
            OpenCloseCupboard();
        }
    }
    private void Update()
    {
        if (!_Heighlight)
            return;
        if (_Player.playerController.interactBase == null)
            _Heighlight.enabled = false;
    }
    public void HideInCupboard()
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
            LeanTween.moveLocal(_Player.gameObject, new Vector3(0, 0, 0), time).setOnComplete(() => {
                LeanTween.rotateLocal(_Player.gameObject, playerRotationDiretion, time / 2);
                // Play only if caretaker encounter is 1st time and you have choosen to hide
                LeanTween.delayedCall(.1f, () => unityEvent?.Invoke());
                _Player.isHided = true;
                _Player.torch.SetActive(false);
                foreach (Collider c in col)
                {
                    c.enabled = true;
                }
            });
            _Player.controller.enabled = false;
            _Player.gameObject.layer = 0;
        }
        else if (_Player.transform.parent == hidePosition)
        {
            _Player.transform.parent = null;
            foreach (Collider c in col)
            {
                c.enabled = false;
            }
            LeanTween.move(_Player.gameObject, exitPosition.position, time).setOnComplete(() =>
            {
                _Player.torch.SetActive(true);
                _Player.isHided = false;
                foreach (Collider c in col)
                {
                    c.enabled = true;
                }
            });
            _Player.gameObject.layer = 8;
            _Player.controller.enabled = true;
        }
    }
    public void OpenCloseCupboard()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Open")) anim.Play("Close");
        else anim.Play("Open");
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
