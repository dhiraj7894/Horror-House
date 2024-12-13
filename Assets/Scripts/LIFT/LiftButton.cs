using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LiftButton : InteractBase, Interacter
{
    public bool isInsidesButton = true;
    public Lift lift;
    public int _floor;
    public Collider col;
    public Collider[] otherCol;
    public GameObject LiftHighlight;

    public int clickCount = 0;
    private void Start()
    {
    }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        if(clickCount < 1)
        {
            unityEvent?.Invoke();
            clickCount++;
        }
        if (!isLocked)
        {
            lift.SetHeight(_floor);
            OffOtherCollider();
            lift.col = col;
            if (isInsidesButton)
            {
                lift.player.controller.enabled = false;
                lift.player.gameObject.layer = 0;
            }

            /// Heighlight Code
            /*LiftHighlight.SetActive(true);
            LeanTween.delayedCall(.1f, () => {
                LiftHighlight.SetActive(false);
            });*/
        }
        
    }

    public void OffOtherCollider()
    {
       /* foreach (Collider col in otherCol)
        {
            col.isTrigger = false;
        }*/
    }

    

}
