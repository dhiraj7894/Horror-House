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


    private void Start()
    {       
    }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        lift.SetHeight(_floor);
        OffOtherCollider();
        lift.col = col;
        if (isInsidesButton)
        {
            lift.player.controller.enabled = false;
            lift.player.gameObject.layer = 0;

        }
    }

    public void OffOtherCollider()
    {
        foreach (Collider col in otherCol)
        {
            col.isTrigger = false;
        }
    }
    
}
