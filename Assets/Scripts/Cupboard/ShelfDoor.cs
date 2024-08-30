using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfDoor : InteractBase, Interacter
{
    //[SerializeField]private float GCurrentRotation;
    [SerializeField]private float LCurrentRotation;

    [Space(10)]
    public Vector3 openRotation;
    public Vector3 closeRotation;

    public float time = 1;
    public Collider[] col;
    private void Start()
    {
        _UIText = "Open";
    }
    public void Interact()
    {
        UITextUpdate();
        if (_UIText.Contains("Close"))
        {
            LeanTween.rotateLocal(gameObject, closeRotation, time).setEaseInCirc();
        }
        else if (_UIText.Contains("Open"))
        {
            LeanTween.rotateLocal(gameObject, openRotation, time).setEaseInCirc();
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

    [ContextMenu("Update Rotation")]
    void checkRotation()
    {
        //GCurrentRotation = transform.eulerAngles.y;
        LCurrentRotation = transform.localEulerAngles.y;
    }

    private void Update()
    {
        checkRotation();
    }

}
