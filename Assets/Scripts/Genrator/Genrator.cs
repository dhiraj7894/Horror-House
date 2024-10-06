using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Genrator : InteractBase, Interacter
{
    [Space(10)]
    private MainPlayer _player;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color colour;
    public Item[] _requirements;
    public Transform[] _requirementPositions;
    public MeshRenderer[] _indicatorLight;

    public Collider[] col;
    public int placementCount = 0;
    [SerializeField]private List<Transform> FuseCounts = new List<Transform>();
    private void Start()
    {
        _player = GameManager.Instance._PlayerObject;

    }
    public void Interact()
    {
        if (_player.GetComponent<ControllerPlayer>()._targetPlace.childCount > 0)
        {
            foreach (Item item in _requirements)
            {
                if (_player.GetComponent<ControllerPlayer>().itemData == item)
                {
                    Transform obj = _player.GetComponent<ControllerPlayer>()._targetPlace.GetChild(0);
                    obj.parent = _requirementPositions[item.id];
                    obj.localPosition = Vector3.zero;
                    obj.localRotation = Quaternion.identity;
                    _indicatorLight[item.id].material.SetColor("_EmissionColor", colour);
                    FuseCounts.Add(obj);
                    placementCount++;
                    // Cutscene for Genrator Power Text
                    CheckFuseConnection();
                }
            }
        }
        if (placementCount >= _requirementPositions.Length)
        {
            isLocked = true;
            GetComponent<Collider>().enabled = false;
        }
    }
    public void Drop()
    {

    }

    public void CheckFuseConnection()
    {
        if(FuseCounts.Count == 1)
        {
            EventManager.Instance.FuseOneConnected();
        }
        if(FuseCounts.Count == 2)
        {
            EventManager.Instance.FuseTwoConnected();
        }
    }
}
