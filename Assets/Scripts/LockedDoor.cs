using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LockedDoor : InteractBase, Interacter
{
    [Space(10)]
    private MainPlayer _player;

    public Item _requirements;
    public Transform _requirementPositions;

    public Collider[] col;
    public float doorOpeningTime = .5f;
    public float doorOpeningAngle = -70;
    public float doorClosingAngle = 0;

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
                foreach (Collider coll in col)
                {
                    coll.enabled = false;
                }
                LeanTween.delayedCall(1, () => { OpenTheDoor(); });

            }
        }
    }
    private void Update()
    {
        if (!_Heighlight)
            return;
        if (_player.playerController.interactBase == null)
            _Heighlight.enabled = false;
    }
    public void Drop()
    {

    }

    public void OpenTheDoor()
    {
        Debug.Log("Temple Lock Unlocked");
        LeanTween.rotateY(gameObject, doorOpeningAngle, doorOpeningTime).setEaseInCirc().setOnComplete(() => {
            gameObject.layer = 0;
            isLocked = false;   
        });
    }
}
