using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObjectPlacer : InteractBase, Interacter
{
    private MainPlayer _player;

    public Item[] _requirements;
    public Transform[] _requirementPositions;

    public int placementCount = 0;
    public GameObject GarageKey;
    private void Start()
    {
        _player = GameManager.Instance._PlayerObject;
        GarageKey.SetActive(false);
    }

    public void Drop()
    {
        
    }

    public void Interact()
    {
        if (_player.GetComponent<ControllerPlayer>()._targetPlace.childCount > 0)
        {
            foreach(Item item in _requirements)
            {
                if(_player.GetComponent<ControllerPlayer>().itemData == item)
                {
                   Transform obj =  _player.GetComponent<ControllerPlayer>()._targetPlace.GetChild(0);
                    obj.parent = _requirementPositions[item.id];
                    obj.localPosition = Vector3.zero;
                    obj.localRotation = Quaternion.identity;
                    placementCount++;
                }
            }
        }

        if(placementCount >= 9)
        {
            isLocked = true;
            GetComponent<Collider>().enabled = false;
            FrameChecker();
        }
    }

    public void FrameChecker()
    {
        if (isLocked)
        {
            EventManager.Instance.PhotoFrameUnlocked();            
            GarageKey.SetActive(true);
        }
    }
}
