using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : InteractBase, Interacter
{
    public Transform playerPosition;

    public MainPlayer _Player;
    public Vector3 currentPosition;

    public float time = 1;
    public Collider[] col;

    private void Start()
    {
        _Player = GameManager.Instance._PlayerObject;
    }

    public void Drop()
    {
        if (_Player.transform.parent == playerPosition)
        {
            foreach (Collider c in col)
            {
                c.enabled = false;
            }
            LeanTween.move(_Player.gameObject, currentPosition, time).setOnComplete(() =>
            {
                _Player.transform.parent = null;
                foreach (Collider c in col)
                {
                    c.enabled = true;
                }
            });
            _Player.controller.enabled = true;
            _Player.gameObject.layer = 8;
        }

    }

    public void Interact()
    {
        currentPosition = transform.position;
        if (_Player.transform.parent != playerPosition)
        {
            foreach (Collider c in col)
            {
                c.enabled = false;
            }
            _Player.transform.parent = playerPosition;
            LeanTween.moveLocal(_Player.gameObject, new Vector3(0, 0, 0), time).setOnComplete(() => {
                LeanTween.rotateLocal(_Player.gameObject, new Vector3(0, 90, 0), time);
                foreach (Collider c in col)
                {
                    c.enabled = true;
                }
            });
            _Player.controller.enabled = false;
            _Player.gameObject.layer = 0;
        }
        else if (_Player.transform.parent == playerPosition)
        {
            foreach (Collider c in col)
            {
                c.enabled = false;
            }
            LeanTween.move(_Player.gameObject, currentPosition, time).setOnComplete(() =>
            {
                _Player.transform.parent = null;
                foreach (Collider c in col)
                {
                    c.enabled = true;
                }
            });
            _Player.gameObject.layer = 8;
            _Player.controller.enabled = true;
        }
    }

}
