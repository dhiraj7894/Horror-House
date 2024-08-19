using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : InteractBase, Interacter
{
    public int ElementType;
    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        GameManager.Instance.CollectElement(ElementType);
        Destroy(this.gameObject);
    }
}
