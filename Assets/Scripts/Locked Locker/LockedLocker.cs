using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedLocker : InteractBase, Interacter
{
    [Space(10)]
    private MainPlayer _player;

    public GameObject door;

    public float doorOpeningTime = .5f;
    public float doorOpeningAngle = -70;

    private void Start()
    {
        _player = GameManager.Instance._PlayerObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (isLocked)
            return;
        OpenTheDoor();
    }
    public void Drop()
    {

    }

    public void OpenTheDoor()
    {
        LeanTween.rotateLocal(door, new Vector3(doorOpeningAngle,0,0), doorOpeningTime).setOnComplete(() => {
        // Spwan item if required 
        });
    }
}
