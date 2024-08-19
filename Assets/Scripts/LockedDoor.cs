using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LockedDoor : InteractBase, Interacter
{
    [Space(10)]
    public bool isLocked = true;
    public MainPlayer _player;

    public Item _requirements;
    public Transform _requirementPositions;

    public Collider[] col;
    public float doorOpeningTime = .5f;

    private void Start()
    {
        _player = GameManager.Instance._PlayerObject;
    }
    public void Interact()
    {
        if (_player.GetComponent<ControllerPlayer>()._targetPlace.childCount > 0)
        {
            if (_player.GetComponent<ControllerPlayer>().itemData == _requirements)
            {
                Transform obj = _player.GetComponent<ControllerPlayer>()._targetPlace.GetChild(0);
                obj.parent = _requirementPositions;
                obj.localPosition = Vector3.zero;
                obj.localRotation = Quaternion.identity;
                LeanTween.delayedCall(1, () => { OpenTheDoor(); });
                


            }
            else
            {
                _UIText = "Require the key";
            }
        }
    }
    public void Drop()
    {

    }

    public void OpenTheDoor()
    {
        LeanTween.rotateY(gameObject, -70, doorOpeningTime).setEaseInCirc().setOnComplete(() => {
            gameObject.layer = 0;
        });
    }
}
