using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InteractableObject : InteractBase, Interacter
{
    public Item itemData;
    private MainPlayer _player;
    public Rigidbody _body;
    public Collider _collider;

    public Vector3 itemPosition;
    public Quaternion itemRotation;

    private void Start()
    {
        _player = GameManager.Instance._PlayerObject;
    }

    private void Update()
    {
        if (!_Heighlight)
            return;
        if(_player.playerController.interactBase == null)
            _Heighlight.enabled = false;
    }
    public void Interact()
    {
        if(_player.GetComponent<ControllerPlayer>()._targetPlace.childCount<= 0)
        {
            this.transform.parent = _player.GetComponent<ControllerPlayer>()._targetPlace;
            _player.GetComponent<ControllerPlayer>().itemData = itemData;
            this.transform.localPosition = itemPosition;
            this.transform.localRotation = itemRotation;
            _body.isKinematic = true;
            _collider.enabled = false;
            UIManager.Instance.SetInHandItemString(itemData.Name);
        }

        
    }

    public void Drop()
    {
        this.transform.parent = null;
        _body.isKinematic = false;
        _collider.enabled = true;
        UIManager.Instance.SetInHandItemString();
    }
}
