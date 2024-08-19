using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour, Interacter
{
    public int Number;

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        Debug.Log("Work = "+ Number);
    }
}
